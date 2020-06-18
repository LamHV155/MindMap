using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MindMap.Models;
using MindMap.Controllers;

namespace MindMap.Views
{
    public partial class frmDelete : Form
    {
        private string name = "";
        public MindMap mindmap;
        public frmDelete()
        {
            InitializeComponent();
            showStorageOnListView();
        }
        private void showStorageOnListView()
        {
            ltwDelete.Columns.Add("Name", 200);
            ltwDelete.Columns.Add("Date Modified", 250);
            ltwDelete.View = View.Details;
            ltwDelete.Items.Clear();
            foreach (STORAGE s in STORAGEcontroller.getListStorage())
            {
                ListViewItem name = new ListViewItem(s.NAME_S);
                ListViewItem.ListViewSubItem date = new ListViewItem.ListViewSubItem(name, s.DATE_MODIFIED.ToString());
                name.SubItems.Add(date);
                ltwDelete.Items.Add(name);
            }
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {  
            if (name == "")
            {
                MessageBox.Show("Choose a name, please !",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                int idboard = STORAGEcontroller.getIDBoard(name);
                if(TOPICcontroller.deleteNode(idboard) && BOARDcontroller.deleteBoardAndStorage(idboard))
                {
                    showStorageOnListView();
                    mindmap.board.Dispose();
                    mindmap.Controls.Add(mindmap.createBoardAndMainNode());
                    MessageBox.Show("Remove successfully!",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Remove failure!",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                }
            }
        }

        private void ltwDelete_Click(object sender, EventArgs e)
        {
            name = ltwDelete.SelectedItems[0].SubItems[0].Text.Trim();
        }
    }
}
