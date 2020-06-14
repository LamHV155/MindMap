
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
using MindMap.Controllers;
using MindMap.Controllers.Objects;
using MindMap.Views;

namespace MindMap
{
    public partial class MindMap : Form
    {
        public Node node=null;
        private int idnode = 0;
        public Board board;
        private mPath path;
        private FormatTable formatTable;

        //data from menu
        private Color M_colorBoard = Color.LightYellow;
        private Color M_colorParentNode = Color.DarkRed;
        private Color M_colorChildNode = Color.IndianRed;
        private string M_shapeParentNode = "Rectangle";
        private string M_shapeChildNode = "Ellipse";
        private string M_stylePath = "Curve";
        private Color M_colorPath = Color.DarkGray;


        //List
        List<Node> listNode = new List<Node>();
       
    
        public MindMap()
        {
            InitializeComponent();
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = width;
            this.Height = height;
            
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;


            idnode = TOPICcontroller.getID();
           
            formatTable = new FormatTable(new Point(this.Width - 300, 100), new Size(290, this.Height - 100), Color.FromArgb(225, 225, 225), Color.Black, this);

            this.Controls.Add(createToolPanel());
            this.Controls.Add(createBoard());
        }

        //Event
        #region Event
        private void Timer_Tick(object sender, EventArgs e)
        {
            
           foreach(Node node in this.board.picbox.Controls)
            {
                node.drawPath(node.Location, node.parent.Location, board.picbox.BackColor, node.path.size + 2);
                node.drawPath(node.Location, node.parent.Location, node.path.color, node.path.size);
            }
        }

        #endregion



        //Tools Panel & Board
        #region Panel & Board
        private Board createBoard()
        {
            Board board = new Board(M_colorBoard, new Point(0, 100), new Size(this.Width - 20, this.Height - 140));
            this.board = board;
            Size nSize = new Size(180, 100);
            Point nLocation = new Point(board.Width/2 - nSize.Width/2, board.Height/2 - nSize.Height/2);
            path = new mPath(4, M_colorPath, M_stylePath);

            Node n = createNode(idnode, "Main Topic", nLocation, nSize, M_colorParentNode, Color.White, path, null, 14, M_shapeParentNode);
            board.picbox.Controls.Add(n);
            this.node = n;
            idnode++;


            //Add List 
            listNode.Add(n);
            
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

            pnl.Controls.Add(createButton(new Point(50, 10), new Size(90, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Add"));
            pnl.Controls.Add(createButton(new Point(160, 10), new Size(90, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Delete"));
            pnl.Controls.Add(createButton(new Point(pnl.Width - 150, 10), new Size(70, 35), Color.FromArgb(235, 235, 235), Color.Gray, node, "Format"));
            return pnl;
        }
        #endregion



        //Function Button
        #region Function Button
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
                    Size nSize = new Size(180, 100);
                    Point nLocation = new Point(board.Width / 2 - nSize.Width / 2, board.Height / 2 - nSize.Height / 2);
                    path = new mPath(3, M_colorPath, M_stylePath);

                    Node n = createNode(idnode, "Main Topic", nLocation, nSize, M_colorParentNode, Color.White, path, null, 14, M_shapeParentNode);
                    this.board.picbox.Controls.Add(n);
                    idnode++;

                    listNode.Add(n);

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
                    Point cLoc = this.node.getChildLocation(this.node.Width);
                    Node n = createNode(idnode, "subtopic " + idnode, cLoc, new Size(150, 80), M_colorChildNode, Color.White, this.path, this.node, 12, M_shapeChildNode);
                    this.board.picbox.Controls.Add(n);
                    idnode++;

                    //Add list
                    listNode.Add(n);

                }
                else if((string)fb.Tag == "Delete")
                {
                   
                    this.node.drawPath(this.node.Location, this.node.parent.Location, this.board.picbox.BackColor, this.node.path.size + 2);
                    
                    //get out of list
                    foreach(Node n in listNode)
                    {
                        if(this.node.id == n.id)
                        {
                            if(listNode.Count != 1)
                                removeChildOfNode(n);
                            listNode.Remove(n);
                            break;
                        }
                    }

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
                        this.formatTable.updateFormatTable(this.node.shape, this.node.BackColor, this.node.size, this.node.Font.FontFamily.Name, (int)this.node.Font.Size, this.node.ForeColor, this.node.path);
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
        private void removeChildOfNode(Node parent)
        {
  
            foreach(Node n in this.listNode.ToList())
            {
                if (parent.id == n.parent.id)
                {
                    removeChildOfNode(n);
                    this.listNode.Remove(n);                   
                }
            }
        }

        #endregion


        //Node
        #region Node
        private Node createNode(int id, string label, Point location, Size size, Color bcolor, Color fcolor, mPath path, Node node = null, int textsize=12, string shape = "Rectangle", string font="tahoma")
        {
            Node n = new Node(id, label, location, size, this.board, bcolor, fcolor, path, font, textsize, shape, node);
            n.Click += Node_Click;
           
            n.expandBoard();
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

            this.formatTable.updateFormatTable(this.node.shape, this.node.BackColor, this.node.size, this.node.Font.FontFamily.Name, (int)this.node.Font.Size, this.node.ForeColor, this.node.path);

        }

        #endregion


        public void updateNode(string shape, Color colorNode, float sizeShape, string font, int textSize, Color colorText, string stylePath, int sizePath, Color colorPath)
        {
          
            this.node.shape = shape;
            this.node.BackColor = colorNode;
            if(this.node.size != sizeShape)
            {
                this.node.Width = (int)(this.node.Width * (sizeShape / this.node.size));
                this.node.Height = (int)(this.node.Height * (sizeShape / this.node.size));
                this.node.size = sizeShape;
            }          
            this.node.Font = new Font(font, textSize);
            this.node.ForeColor = colorText;
            this.node.path = new mPath(sizePath, colorPath, stylePath);
        }



        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSave formSave = new frmSave();
            formSave.ShowDialog();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmOpen formOpen = new frmOpen();
            formOpen.ShowDialog();
        }
    }
}
