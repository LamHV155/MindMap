using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindMap.Controllers.Objects
{
    public class fButton : Button 
    {
        public Node node;
        public fButton(Point location, Size size, Node node, string label, Color bcolor, Color fcolor) : base()
        {
            this.Location = location;
            this.Size = size;
            this.node = node;
            this.Text = label;
            this.BackColor = bcolor;
            this.ForeColor = fcolor;
            this.FlatStyle = FlatStyle.Flat;
            
        }
    }
}
