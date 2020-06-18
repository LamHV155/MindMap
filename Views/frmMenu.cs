using MindMap.Controllers;
using MindMap.Controllers.Objects;
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

namespace MindMap.Views
{
    public partial class frmMenu : Form
    {
        public MenuButton menuBtn;
        public frmMenu()
        {
            InitializeComponent();
            addMenuBtnToFLPanel();
        }

        private void addMenuBtnToFLPanel()
        {
            foreach(MENU menu in MENUcontroller.getListMenu())
            {
                Color colorBoard = System.Drawing.ColorTranslator.FromHtml(menu.COLOR_BOARD);
                Color colorParent = System.Drawing.ColorTranslator.FromHtml(menu.COLOR_PARENT);
                Color colorChild = System.Drawing.ColorTranslator.FromHtml(menu.COLOR_CHILD);
                Color colorPath = System.Drawing.ColorTranslator.FromHtml(menu.COLOR_PATH);
                MenuButton Mbtn = new MenuButton(menu.ID, colorBoard, colorParent, colorChild, colorPath, menu.SHAPE_PARENT, menu.SHAPE_CHILD, menu.STYLE_PATH);
                Mbtn.formMenu = this;
                flpMenu.Controls.Add(Mbtn);

            }
        }

        public void displayBorderNode(MenuButton mb)
        {
            foreach (MenuButton mBtn in this.flpMenu.Controls)
            {
                mBtn.pnl.Dock = DockStyle.Fill;
                mBtn.FlatAppearance.BorderSize = 3;
            }
            mb.pnl.Dock = DockStyle.None;
            mb.FlatAppearance.BorderSize = 0;
            this.menuBtn = mb;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            frmOpen formOpen = new frmOpen();
            formOpen.Show();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(this.menuBtn != null)
            {
                MindMap mm = new MindMap(this.menuBtn.colorBoard, this.menuBtn.colorParent, this.menuBtn.colorChild, this.menuBtn.colorPath, this.menuBtn.shapeParent, this.menuBtn.shapeChild, this.menuBtn.stylePath);
           
                mm.Show();
            }
            else
            {
                MessageBox.Show("Choose an element in a panel, please !",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
              
        }
    }
}
