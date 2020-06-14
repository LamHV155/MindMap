using MindMap.Controllers;
using MindMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        MindMap mindmap = new MindMap();
        public frmSave()
        {
            InitializeComponent();
        }
        
        private void save()
        {
            STORAGE storage = new STORAGE();
            

            if (STORAGEcontroller.checkName(tbxSave.Text))
            {
                storage.NAME_S = tbxSave.Text;
                storage.ID_BOARD = BOARDcontroller.getID();
            }
            else
            {
                MessageBox.Show("This name already exists !",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

        }

        private bool saveBoard(int id)
        {
            BOARD board = new BOARD();
            board.ID = id;
            board.WIDTH = mindmap.board.picbox.Width;
            board.HEIGHT = mindmap.board.picbox.Height;
            board.COLOR = HexConverter(mindmap.board.picbox.BackColor);

            if(BOARDcontroller.AddBoard(board))
                return true;
            return false;
        }

        private bool saveListNode()
        {
            return false;
        }


        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
