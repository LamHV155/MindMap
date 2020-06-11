using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindMap.Controllers.Objects
{
    public class Relationship : Button
    {
        public Node node1;
        public Node node2;
        public string label;
        public Relationship(): base()
        {
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ForeColor = Color.Orange;
            this.Size = new Size(100, 60);

            this.DoubleClick += Relationship_DoubleClick;
        }

        private void Relationship_DoubleClick(object sender, EventArgs e)
        {
            this.Controls.Add(cTextBox());
        }

        private void drawPath(Point node1Loc, Point node2Loc, Color color, int size)
        {
            Point point1 = new Point(node1Loc.X + this.node1.Width / 2, node1Loc.Y + this.node1.Height / 2);
            Point point2 = new Point(node2Loc.X + this.node2.Width / 2, node2Loc.Y + this.node2.Height / 2);
        }

        public Point getLabelLocation(Node n1, Node n2)
        {
            if (isLeftParent(n1) && isLeftParent(n2))
                return new Point((n1.Location.X > n2.Location.X) ? (n1.Location.X) : (n2.Location.X), (int)((n1.Location.Y + n2.Location.Y) / 2));
            return new Point(0,0);
        }

        private bool isLeftParent(Node n)
        {
            if (n.Location.X < n.parent.Location.X) return true;
            return false;
        }

        #region TextBox
        private TextBox cTextBox()
        {
            TextBox tb = new TextBox();
            tb.Size = this.Size;
            tb.Multiline = true;
            tb.Location = new Point(0, 0);
            tb.BackColor = this.BackColor;
            tb.ForeColor = this.ForeColor;
            tb.Font = this.Font;
            tb.Text = this.Text;

            tb.KeyPress += Tb_KeyPress;

            return tb;
        }

        private void Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //Enter
            {
                TextBox tb = (TextBox)sender;
                this.Text = tb.Text;
                tb.Dispose();
            }
        }

        #endregion


    }
}
