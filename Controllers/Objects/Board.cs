using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MindMap.Controllers.Objects
{
    public class Board : Panel
    {
        public PictureBox picbox;
        public int id = -1;
        public Board(int id, Color bcolor, Point location, Size size) : base()
        {
            this.id = id;
            this.BackColor = bcolor;
            this.Location = location;
            this.Size = size;
            this.AutoScroll = true;

            this.picbox = new PictureBox();
            this.picbox.Size = this.Size;
            this.picbox.Image = new Bitmap(this.Width, this.Height);
            this.picbox.BackColor = this.BackColor;
            this.picbox.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(this.picbox);
        }
    }
}
