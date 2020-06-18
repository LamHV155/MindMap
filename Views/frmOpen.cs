using MindMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MindMap.Controllers;
using MindMap.Controllers.Objects;
using System.Security.Cryptography.Xml;

namespace MindMap.Views
{
    public partial class frmOpen : Form
    {
        public MindMap mindmap { get; set; }
        public frmMenu fMenu { get; set; }
        public string name = "";
        public frmOpen()
        {
            InitializeComponent();
            showStorageOnListView();
        }

        private void showStorageOnListView()
        {
            ltvOpen.Columns.Add("Name", 200);
            ltvOpen.Columns.Add("Date Modified", 250);
            ltvOpen.View = View.Details;
            foreach (STORAGE s in STORAGEcontroller.getListStorage())
            {
                ListViewItem name = new ListViewItem(s.NAME_S);
                ListViewItem.ListViewSubItem date = new ListViewItem.ListViewSubItem(name, s.DATE_MODIFIED.ToString());
                name.SubItems.Add(date);
                ltvOpen.Items.Add(name);
            }
        }

        
        private void ltvOpen_Click(object sender, EventArgs e)
        {
            name = ltvOpen.SelectedItems[0].SubItems[0].Text.Trim();
        }



        private List<Node> addNodeToBoard(BOARD board)
        {
       
            mindmap.createBoard(board);
            List<Node> listNode = new List<Node>();
            List<int> idParents = new List<int>();
            foreach (TOPIC topic in TOPICcontroller.getListTopic(board.ID))
            {
                Color bcolor = ColorTranslator.FromHtml(topic.BACKCOLOR);
                Color fcolor = ColorTranslator.FromHtml(topic.FORECOLOR);
                Color pcolor = ColorTranslator.FromHtml(topic.COLOR_PATH);
                mPath path = new mPath(topic.SIZE_PATH, pcolor, topic.STYLE_PATH);

                bool flag = false;
                foreach (Node n in listNode)
                {
                    if (topic.ID_PARENT == n.id)
                    {
                        Node node = mindmap.createNode(topic.ID, topic.LABEL_TP, new Point(topic.POS_X, topic.POS_Y), new Size(topic.WIDTH, topic.HEIGHT), bcolor, fcolor, path, (float)topic.SIZE, n, topic.TEXT_SIZE, topic.SHAPE, topic.FONT);
                        mindmap.board.picbox.Controls.Add(node);
                        listNode.Add(node);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    Node node = mindmap.createNode(topic.ID, topic.LABEL_TP, new Point(topic.POS_X, topic.POS_Y), new Size(topic.WIDTH, topic.HEIGHT), bcolor, fcolor, path, (float)topic.SIZE, null, topic.TEXT_SIZE, topic.SHAPE, topic.FONT);
                    mindmap.board.picbox.Controls.Add(node);
                    listNode.Add(node);
                }
            }
            return listNode;
        }



        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(name == "")
            {
                MessageBox.Show("Choose a name, please !",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                if(mindmap != null)
                {
                    mindmap.board.Dispose();
                    int idboard = STORAGEcontroller.getIDBoard(name);
                    BOARD board = BOARDcontroller.getBOARD(idboard);
                    mindmap.reNewListNode(addNodeToBoard(board), board);
                
                    name = "";
                    this.Dispose();
                }
                else if(mindmap == null)
                {
                    int idboard = STORAGEcontroller.getIDBoard(name);
                    BOARD board = BOARDcontroller.getBOARD(idboard);
                    
                    MindMap mm = new MindMap();
                    this.mindmap = mm;
                    mm.board.Dispose();
                    mm.reNewListNode(addNodeToBoard(board), board);
               
                    mm.Show();
                    this.Dispose();
                }
            }
           
        }

      
    }
}
