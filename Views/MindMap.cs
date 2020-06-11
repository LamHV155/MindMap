using MindMap.Controllers.Objects;
using MindMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindMap
{
    public partial class MindMap : Form
    {
        private Node node=null;
        private int idnode = 0;
        private Board board;
        private mPath path;
        private FormatTable formatTable;
    
        private delegate Point getChilLocation(int width);
        private delegate void Draw(Point selfLoc, Point parentLoc, Color color, int size);
        private delegate void Expand();
        private delegate void updateInfoToFormat(string shape, Color colorNode, string font, int textsize, Color colorText, mPath path);
        public MindMap()
        {
            InitializeComponent();
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = width;
            this.Height = height;

            this.Controls.Add(createToolPanel());
            this.Controls.Add(createBoard());
            formatTable = new FormatTable(new Point(this.Width - 300, 100), new Size(290, this.Height - 100), Color.FromArgb(225, 225, 225), Color.Black);

            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 2500;
            timer.Tick += Timer_Tick;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw draw;
           foreach(Node node in this.board.picbox.Controls)
            {
                draw = new Draw(node.drawPath);
                draw(node.Location, node.parent.Location, board.picbox.BackColor, node.path.size + 2);
                draw(node.Location, node.parent.Location, node.path.color, node.path.size);
            }
        }

        private Board createBoard()
        {
            Board board = new Board(Color.Ivory, new Point(0, 100), new Size(this.Width - 20, this.Height - 140));
            this.board = board;
            Size nSize = new Size(120, 80);
            Point nLocation = new Point(board.Width/2 - nSize.Width/2, board.Height/2 - nSize.Height/2);
            path = new mPath(0, 2, Color.Black, "curve");
            
            board.picbox.Controls.Add(createNode(idnode, "Main Topic", nLocation, nSize, Color.DarkRed, Color.White, path, null, 14));
            idnode++;
            return board;
        }
  

        private Panel createToolPanel()
        {
            Panel pnl = new Panel();
            pnl.Location = new Point(0, 30);
            pnl.Width = this.Width;
            pnl.Height = 70;
            pnl.BackColor = Color.FromArgb(230, 230, 230);
            pnl.Font = new Font("tahoma", 9);

            pnl.Controls.Add(createButton(new Point(450, 10), new Size(90, 35), Color.FromArgb(235,235,235), Color.Gray, node, "Add"));
            pnl.Controls.Add(createButton(new Point(560, 10), new Size(90, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Relationship"));
            pnl.Controls.Add(createButton(new Point(680, 10), new Size(90, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Summary"));
            pnl.Controls.Add(createButton(new Point(800, 10), new Size(90, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Delete"));
            pnl.Controls.Add(createButton(new Point(pnl.Width - 150, 10), new Size(70, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Format"));
            return pnl;
        }

        private Button createButton(Point location, Size size, Color backcolor, Color forecolor, Node n, string label)
        {
            fButton fb = new fButton(location, size, n, label, backcolor, forecolor);
            fb.Tag = label;
            fb.Click += Fb_Click;
            return fb;
        }

        private void Fb_Click(object sender, EventArgs e)
        {

            fButton fb = (fButton)sender;

            if (this.board.picbox.Controls.Count == 0)
            {
                if((string)fb.Tag == "Add")
                {
                    Size nSize = new Size(120, 80);
                    Point nLocation = new Point(board.Width / 2 - nSize.Width / 2, board.Height / 2 - nSize.Height / 2);
                    path = new mPath(0, 2, Color.Black, "curve");
                    this.board.picbox.Controls.Add(createNode(idnode, "Main Topic", nLocation, nSize, Color.DarkRed, Color.White, path, null, 14));
                    idnode++;
                }
                else{
                    MessageBox.Show("Vui lòng thêm node mới !");
                }
            }
            else if(node is null)
            {
                MessageBox.Show("Chưa chọn node ! ");
            }
            else
            {
                if ((string)fb.Tag == "Add")
                {
                    getChilLocation cLoc = new getChilLocation(this.node.getChildLocation);
                    this.board.picbox.Controls.Add(createNode(idnode, "subtopic " + idnode, cLoc(100), new Size(100, 60), Color.IndianRed, Color.White, this.path, this.node));
                    idnode++;
                }
                else if((string)fb.Tag == "Delete")
                {
                    Draw draw = new Draw(node.drawPath);
                    draw(this.node.Location, this.node.parent.Location, this.board.picbox.BackColor, this.node.path.size + 2);
                    node.Dispose();
                    this.node = null;
                    
                }
                else if ((string)fb.Tag == "Format")
                {
                    if(this.formatTable.isOn == false)
                    {
                      
                        this.board.Size = new Size(this.Width - 300, this.Height - 140);
                        this.Controls.Add(this.formatTable);
                        this.formatTable.isOn = true;
                        updateInfoToFormat format = new updateInfoToFormat(this.formatTable.updateFormatTable);
                        format(this.node.shape, this.node.BackColor, this.node.Font.FontFamily.Name, (int)this.node.Font.Size, this.node.ForeColor, this.node.path);
                    }
                    else
                    {
                        this.board.Size = new Size(this.Width - 20, this.Height - 140);
                        this.Controls.Remove(this.formatTable);
                        this.formatTable.isOn = false;
                    }
                   
                }
              
                

                
            }

        }

        private Node createNode(int id, string label, Point location, Size size, Color bcolor, Color fcolor, mPath path, Node node = null, int textsize=12, string shape = "Rectangle", string font="tahoma")
        {
            Node n = new Node(id, label, location, size, this.board, bcolor, fcolor, path, font, textsize, shape, node);
            n.Click += Node_Click;

            

            Expand expand = new Expand(n.expandBoard);
            expand();
            return n;
        }

        private void Node_Click(object sender, EventArgs e)
        {
            Node no = (Node)sender;
            
            foreach(Node node in this.board.picbox.Controls)
            {
                node.FlatAppearance.BorderSize = 0;
            }
            no.FlatAppearance.BorderColor = Color.DarkCyan;
            no.FlatAppearance.BorderSize = 3;
            this.node = no;

            updateInfoToFormat format = new updateInfoToFormat(this.formatTable.updateFormatTable);
            format(this.node.shape, this.node.BackColor, this.node.Font.FontFamily.Name, (int)this.node.Font.Size, this.node.ForeColor, this.node.path);

        }
    }
}
