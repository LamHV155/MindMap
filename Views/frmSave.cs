using MindMap.Controllers;
using MindMap.Controllers.Objects;
using MindMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindMap.Views
{
    public partial class frmSave : Form
    {
        public MindMap mindmap { get; set; }
        List<Node> listNode = new List<Node>();
     
        public frmSave()
        {
            InitializeComponent();
            showStorageOnListView();
        }
        
        private void showStorageOnListView()
        {
            ltwSave.Columns.Add("Name", 200);
            ltwSave.Columns.Add("Date Modified", 250);
            ltwSave.View = View.Details;
            foreach(STORAGE s in STORAGEcontroller.getListStorage())
            {
                ListViewItem name = new ListViewItem(s.NAME_S);
                ListViewItem.ListViewSubItem  date = new ListViewItem.ListViewSubItem(name, s.DATE_MODIFIED.ToString());
                name.SubItems.Add(date);
                ltwSave.Items.Add(name);
            }
        }

        public void update()
        {
            //storage
            STORAGE storage = new STORAGE();
            storage.ID_BOARD = mindmap.board.id;
            storage.NAME_S = STORAGEcontroller.getNameStorage(storage.ID_BOARD);
            storage.DATE_MODIFIED = DateTime.Now;

            //board
            BOARD board = new BOARD();
            board.ID = storage.ID_BOARD;
            board.WIDTH = mindmap.board.picbox.Width;
            board.HEIGHT = mindmap.board.picbox.Height;
            board.COLOR = HexConverter(mindmap.board.picbox.BackColor);

            //node
            TOPICcontroller.removeNode(convertNodeToTopic(board, mindmap.listDelete));
            mindmap.listDelete.Clear();
            List<TOPIC> existTopics = new List<TOPIC>() { };
            existTopics = TOPICcontroller.getListTopic(board.ID);
            List<TOPIC> updateTopics = new List<TOPIC>();
            foreach (TOPIC topic in convertNodeToTopic(board, this.listNode))
            {
                foreach (TOPIC etopic in existTopics)
                {
                    if (topic.ID == etopic.ID)
                    {
                        updateTopics.Add(topic);
                    }
                }
            }
            TOPICcontroller.updateListTopic(updateTopics);

            BOARDcontroller.updateBoard(board);

            if (STORAGEcontroller.updateStorage(storage))
            {
                MessageBox.Show("saved successfully",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }


        private void save()
        {
            STORAGE storage = new STORAGE();
           
               
            if (STORAGEcontroller.checkName(tbxSave.Text))
            {
                storage.NAME_S = tbxSave.Text;
                storage.ID_BOARD = BOARDcontroller.getID();
                storage.DATE_MODIFIED = DateTime.Now;
                saveBoard(storage.ID_BOARD);//, false);
                if (STORAGEcontroller.addStorage(storage))
                {
                    MessageBox.Show("saved successfully",
                                    "Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }

            }
            else
            {
                DialogResult dr =  MessageBox.Show("This name already exists !",
                                                    "Warning",
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Warning);
            }
           
        }

        private void saveBoard(int id)
        {
            BOARD board = new BOARD();
            board.ID = id;
            board.WIDTH = mindmap.board.picbox.Width;
            board.HEIGHT = mindmap.board.picbox.Height;
            board.COLOR = HexConverter(mindmap.board.picbox.BackColor);

            saveListNode(board);
          
            BOARDcontroller.AddBoard(board);

            mindmap.board.id = id;
        }
        public List<TOPIC> convertNodeToTopic(BOARD board, List<Node> listNode)
        {
            List<TOPIC> listTopic = new List<TOPIC>();
            foreach (Node node in listNode)
            {
                TOPIC topic = new TOPIC();
                topic.ID = node.id;
                topic.ID_BOARD = board.ID;
                topic.ID_PARENT = node.parent.id;
                topic.LABEL_TP = node.Text;
                topic.POS_X = node.Location.X;
                topic.POS_Y = node.Location.Y;
                topic.SHAPE = node.shape;
                topic.SIZE = node.size;
                topic.SIZE_PATH = node.path.size;
                topic.TEXT_SIZE = (int)node.Font.Size;
                topic.STYLE_PATH = node.path.type;
                topic.COLOR_PATH = HexConverter(node.path.color);
                topic.BACKCOLOR = HexConverter(node.BackColor);
                topic.FORECOLOR = HexConverter(node.ForeColor);
                topic.WIDTH = node.Width;
                topic.HEIGHT = node.Height;
                topic.FONT = node.Font.FontFamily.Name;
                topic.BOARD = board;

                listTopic.Add(topic);
            }
            return listTopic;
        }

        public void saveListNode(BOARD board)
        {
            TOPICcontroller.addListTopic(convertNodeToTopic(board, this.listNode));          
        }

       
        public void getListNode(List<Node> list)
        {
            listNode.AddRange(list);
        }


        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
            mindmap.isExisted = true;
            this.Dispose();
        }
    }
}
