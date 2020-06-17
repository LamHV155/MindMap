using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using MindMap.Views;

namespace MindMap.Controllers.Objects
{
    
    public class MenuButton : Button
    {
        public int id;
        public Color colorBoard, colorParent, colorChild, colorPath;
        public string shapeParent, shapeChild, stylePath;
        Graphics path = null;
        public frmMenu formMenu;
        public Panel pnl;

        public MenuButton(int id, Color colorB, Color colorParent, Color colorC, Color colorPath, string shapeP, string shapeC, string style) : base()
        {
            this.id = id;
            this.colorBoard = colorB;
            this.colorParent = colorParent;
            this.colorPath = colorPath;
            this.colorChild = colorC;
            this.shapeChild = shapeC;
            this.shapeParent = shapeP;
            this.stylePath = style;
            this.Size = new Size(300, 200);

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Silver;
            this.FlatAppearance.BorderSize = 2;
            this.BackColor = Color.DarkCyan;



            pnl = new Panel();
            pnl.DoubleClick += Pnl_DoubleClick;
            pnl.BackColor = this.colorBoard;
            pnl.Paint += Pnl_Paint;
            pnl.Click += Pnl_Click;
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.Location = new Point(5, 5);
            pnl.Size = new Size(290, 190);
            pnl.Dock = DockStyle.Fill;
            this.Controls.Add(pnl);
        }

        private void Pnl_Click(object sender, EventArgs e)
        {
            formMenu.displayBorderNode(this);
        }

        private void Pnl_DoubleClick(object sender, EventArgs e)
        {
            MindMap mm = new MindMap(this.colorBoard, this.colorParent, this.colorChild, this.colorPath, this.shapeParent, this.shapeChild, this.stylePath);
            mm.Show();
        }

        private void Pnl_Paint(object sender, PaintEventArgs e)
        {
            using (path = ((Panel)sender).CreateGraphics())
            {
                DrawPath(this.stylePath, new Point(110, 75), new Point(10, 10), new Size(80, 50), new Size(50, 30), this.colorPath);
                DrawPath(this.stylePath, new Point(110, 75), new Point(10, 150), new Size(80, 50), new Size(50, 30), this.colorPath);
                DrawPath(this.stylePath, new Point(110, 75), new Point(230, 30), new Size(80, 50), new Size(50, 30), this.colorPath);
                DrawPath(this.stylePath, new Point(110, 75), new Point(230, 130), new Size(80, 50), new Size(50, 30), this.colorPath);



                DrawShape(this.shapeParent, new Point(110, 75), new Size(80, 50), new Pen(Color.DarkGray, 2), new SolidBrush(this.colorParent));
                DrawShape(this.shapeChild, new Point(10, 10), new Size(50, 30), new Pen(Color.DarkGray, 2), new SolidBrush(this.colorChild));
                DrawShape(this.shapeChild, new Point(10, 150), new Size(50, 30), new Pen(Color.DarkGray, 2), new SolidBrush(this.colorChild));
                DrawShape(this.shapeChild, new Point(230, 130), new Size(50, 30), new Pen(Color.DarkGray, 2), new SolidBrush(this.colorChild));
                DrawShape(this.shapeChild, new Point(230, 30), new Size(50, 30), new Pen(Color.DarkGray, 2), new SolidBrush(this.colorChild));
            }
        }
        public void DrawShape(string shape, Point location, Size size, Pen color, Brush backcolor)
        {
            switch (shape)
            {
                case "Rectangle":
                    {
                        path.SmoothingMode = SmoothingMode.AntiAlias;
                        GraphicsPath g = new GraphicsPath(FillMode.Winding);                     
                        g.AddRectangle(new Rectangle(location, size));                       
                        path.FillPath(backcolor, g);
                        path.DrawPath(color, g);
                        g.Dispose();
                        break;
                    }
                case "Rhombus":
                    {
                        path.SmoothingMode = SmoothingMode.AntiAlias;
                        GraphicsPath g = new GraphicsPath(FillMode.Winding);

                        size = new Size((int)(size.Width * 1.5), (int)(size.Height*1.5));
                        Point p1 = new Point(location.X + size.Width / 2, location.Y);
                        Point p2 = new Point(location.X + size.Width, location.Y + size.Height / 2);
                        Point p3 = new Point(location.X + size.Width / 2, location.Y + size.Height);
                        Point p4 = new Point(location.X, location.Y + size.Height / 2);
                        Point[] arrPoint = new Point[4] { p1, p2, p3, p4 };

                        g.AddPolygon(arrPoint);
                        path.FillPath(backcolor, g);

                        path.DrawPath(color, g);
                        g.Dispose();
                        break;
                    }
                case "Ellipse":
                    {
                        path.SmoothingMode = SmoothingMode.AntiAlias;
                        GraphicsPath g = new GraphicsPath(FillMode.Winding);
                        size = new Size((int)(size.Width * 1.5), (int)(size.Height * 1.5));
                        g.AddEllipse(new Rectangle(location, size));
                        path.FillPath(backcolor, g);
                        path.DrawPath(color, g);
                        g.Dispose();
                        break;
                    }
            }
        }
        public void DrawPath(string name, Point p1, Point p2, Size z1, Size z2, Color color)
        {
            switch (name)
            {
                case "Curve":
                    {
                        path.SmoothingMode = SmoothingMode.AntiAlias;
                        Point point0 = new Point(p1.X + z1.Width / 2, p1.Y + z1.Height / 2);
                        Point point2 = new Point(p2.X + z2.Width / 2, p2.Y + z2.Height / 2);
                        int x1 = (point0.X > point2.X) ? (point2.X + (int)((point0.X - point2.X) / 2)) : (point0.X + (int)((point2.X - point0.X) / 2));
                        int y1 = (point0.Y > point2.Y) ? (point0.Y - (int)((point0.Y - point2.Y) / 5)) : (point0.Y + (int)((point2.Y - point0.Y) / 5));
                        Point point1 = new Point(x1, y1);
                        Point[] arrP = new Point[3] { point0, point1, point2 };

                        path.DrawCurve(new Pen(color, 2), arrP);

                        break;
                    }
                case "Line":
                    {
                        path.SmoothingMode = SmoothingMode.AntiAlias;
                        Point point0 = new Point(p1.X + z1.Width / 2, p1.Y + z1.Height / 2);
                        Point point2 = new Point(p2.X + z2.Width / 2, p2.Y + z2.Height / 2);
                        path.DrawLine(new Pen(color, 2), point0, point2);
                        break;
                    }
            }
        }
    }

}

