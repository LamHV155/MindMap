using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MindMap.Controllers.Objects
{
    public class Node : Button 
    {
        public int id;
        public Node parent;
        public Board board;
        public mPath path;
        public string shape;
        public Color backcolor;
        public float size;

        public Point curLocation;
        public Point curParentLocation;
        
       
        public Node(int id, string label, Point location, Size size, Board board, Color bcolor, Color fcolor, mPath path, float size2, string font, int textsise, string shape, Node parent) : base()
        {
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);

            this.id = id;
            if(parent is null)
            {
                this.parent = this;
            }
            else
            {
                this.parent = parent;
            }
            this.Location = location;
            this.curLocation = location;
            this.curParentLocation = this.parent.Location;
            this.Size = size;
            this.board = board;
            this.BackColor = bcolor;
          
            this.ForeColor = fcolor;
            this.path = path;
            this.Font = new Font(font, textsise);
            this.Text = label;
            this.FlatStyle = FlatStyle.Flat;
            this.shape = shape;
            this.size = size2;
           
            this.LocationChanged += Node_LocationChanged;
            this.parent.LocationChanged += Parent_LocationChanged;
            this.parent.Disposed += Parent_Disposed;
            this.DoubleClick += Node_DoubleClick;
            this.SizeChanged += Node_SizeChanged;
            cShape();

            drawPath(this.Location, this.parent.Location, Color.Black, this.path.size);
            ControlExtension.Draggable(this, true);
        }

      

        public void cShape()
        {
            using (GraphicsPath path = new GraphicsPath(FillMode.Winding))
            {
                if (this.shape == "Ellipse")
                {
                    path.AddEllipse(new Rectangle(0, 0, this.Width, this.Height));
                    this.Region = new Region(path);
                    path.Dispose();
                }
                else if(this.shape == "Rhombus")
                {
                    Point p1 = new Point(this.Width / 2, 0);
                    Point p2 = new Point(this.Width, this.Height / 2);
                    Point p3 = new Point(this.Width / 2, this.Height);
                    Point p4 = new Point(0, this.Height / 2);
                    Point[] arrPoint = new Point[4] { p1, p2, p3, p4 };

                    path.AddPolygon(arrPoint);
                    this.Region = new Region(path);
                    path.Dispose();
                }
                else if(this.shape == "Rectangle")
                {
                    path.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
                    this.Region = new Region(path);
                    path.Dispose();
                }
                 
            }
               
            
        }


        //Event
        #region event

        private void Node_SizeChanged(object sender, EventArgs e)
        {

            cShape();
            this.board.picbox.Image = new Bitmap(this.board.picbox.Image.Width, this.board.picbox.Image.Height);
        }

      


        private void Node_DoubleClick(object sender, EventArgs e)
        {
            this.Controls.Add(cTextBox());
        }


        public void Parent_LocationChanged(object sender, EventArgs e)
        {

            if (this.IsDisposed == false)
            {
                drawPath(curLocation, curParentLocation, this.board.picbox.BackColor, this.path.size + 2);
                drawPath(curLocation, this.parent.Location, this.path.color, this.path.size);
                curParentLocation = this.parent.Location;
            } 
        }

        private void Node_LocationChanged(object sender, EventArgs e)
        {
            expandBoard();

            drawPath(curLocation, this.parent.Location, this.board.picbox.BackColor, this.path.size + 2);
            drawPath(this.Location, this.parent.Location, this.path.color, this.path.size);
            curLocation = this.Location;
        }

        private void Parent_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
            drawPath(this.Location, this.parent.Location, this.board.picbox.BackColor, this.path.size + 2);
        }
        #endregion


        //TextBox
        #region textbox

        private TextBox cTextBox()
        {
            TextBox tb = new TextBox();
            tb.Size = this.Size;
            tb.Multiline = true;
            tb.BorderStyle = BorderStyle.None;
            tb.Location = new Point(0, this.Height/3);
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


        //Draw path

        #region Draw Path


        public void drawPath(Point selfLocation, Point parentLocation, Color color, int size)
        {
            using (var g = Graphics.FromImage(board.picbox.Image))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Point point0 = new Point(selfLocation.X + this.Width / 2, selfLocation.Y + this.Height / 2);
                Point point2 = new Point(parentLocation.X + this.parent.Width / 2, parentLocation.Y + this.parent.Height / 2);
                int x1 = (point0.X > point2.X) ? (point2.X + (int)((point0.X - point2.X) / 2)) : (point0.X + (int)((point2.X - point0.X) / 2));
                int y1 = (point0.Y > point2.Y) ? (point0.Y - (int)((point0.Y - point2.Y) / 5)) : (point0.Y + (int)((point2.Y - point0.Y) / 5));
                Point point1 = new Point(x1, y1);

                Point[] arrP = new Point[3] { point0, point1, point2 };
                if (this.path.type == "Curve")
                {
                    g.DrawCurve(new Pen(color, size), arrP);
                }
                else if(this.path.type == "Line")
                {
                    g.DrawLine(new Pen(color, size), point0, point2);
                }
                board.picbox.Refresh();
            }
            
            
        }



        #endregion

        private int step = 100;

        public void expandBoard()
        {
            if (this.Location.X + this.Width > this.board.picbox.Width - 20)
            {
                this.board.picbox.Size = new Size(this.board.picbox.Width + step, this.board.picbox.Height);
                this.board.picbox.Image = new Bitmap(this.board.picbox.Width + step, this.board.picbox.Height);
                foreach (Node node in this.board.picbox.Controls)
                {
                    if (node.id != this.id)
                        drawPath(node.Location, node.parent.Location, node.path.color, node.path.size);
                }
            }
            else if (this.Location.X < 20)
            {
                this.board.picbox.Size = new Size(this.board.picbox.Width + step, this.board.picbox.Height);
                this.board.picbox.Image = new Bitmap(this.board.picbox.Width + step, this.board.picbox.Height);

                foreach (Node node in this.board.picbox.Controls)
                {
                    node.Location = new Point(node.Location.X + step, node.Location.Y);
                }
                this.Location = curLocation;
            }
            else if (this.Location.Y + this.Height > this.board.picbox.Height - 50)
            {
                this.board.picbox.Size = new Size(this.board.picbox.Width, this.board.picbox.Height + step);
                this.board.picbox.Image = new Bitmap(this.board.picbox.Width, this.board.picbox.Height + step);
                foreach (Node node in this.board.picbox.Controls)
                {
                    if (node.id != this.id)
                        drawPath(node.Location, node.parent.Location, node.path.color, node.path.size);
                }
            }
            else if (this.Location.Y < 20)
            {
                this.board.picbox.Size = new Size(this.board.picbox.Width, this.board.picbox.Height + step);
                this.board.picbox.Image = new Bitmap(this.board.picbox.Width, this.board.picbox.Height + step);

                foreach (Node node in this.board.picbox.Controls)
                {
                    node.Location = new Point(node.Location.X, node.Location.Y + step);
                }
                this.Location = curLocation;
            }
        }

        public Point getChildLocation(int width)
        {
            return (isLeftParent()) ? (new Point(this.Location.X - width*2, this.Location.Y)) : (new Point(this.Location.X + width*2, this.Location.Y));            
        }

        private bool isLeftParent()
        {
            if (this.Location.X < this.parent.Location.X)
                return true;
            return false;
        }

    }
}
