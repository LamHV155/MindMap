using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MindMap.Models;

namespace MindMap.Controllers.Objects
{
    class FormatTable : Panel
    {
        public bool isOn = false;
    
        public string shapeNode="";
        public Color colorNode=Color.White;
        public string fontText="";
        public int sizeText=0;
        public Color colorText=Color.White;
        public string stylePath="";
        public int sizePath=0;
        public Color colorPath=Color.White;
     
        public FormatTable(Point location, Size size, Color bcolor, Color fcolor) : base()
        {
            this.Location = location;
            this.Size = size;
            this.BackColor = bcolor;
            this.ForeColor = fcolor;
            splitLayout();
        }

       
        public void splitLayout()
        {
            List<string> listShape = new List<string>() {"Rectangle", "Ellipse", "Rhombus"};
            List<string> listSize = new List<string>() { };
            for(int i = 1; i <=20; i++)
            {
                listSize.Add(i.ToString());
            }
            List<string> listFont = new List<string>() {"Tahoma", "Times New Roman", "Calibri", "Helvetica", "Georgia" };
            List<string> listStypePath = new List<string>() { "Curve", "Line" };
            
            cLabel(new Point(10, 50), "Shape", 15, FontStyle.Bold);

            cCombobox(listShape, new Point(100, 97), new Size(120, 20), "cbbShape");
            cLabel(new Point(25, 100), "Shape : ", 12, FontStyle.Regular);

            cBtnColor(new Point(100, 150), new Size(80, 20), "colorShape");
            cLabel(new Point(25, 150), "Color : ", 12, FontStyle.Regular);
            ///////////////////////////////////////////////////////////////////////////////////////////

            cLabel(new Point(10, 250), "Text", 15, FontStyle.Bold);

            cCombobox(listFont, new Point(100, 300), new Size(150, 20), "cbbFont");
            cLabel(new Point(25, 300), "Font : ", 12, FontStyle.Regular);

            cCombobox(listSize, new Point(100, 350), new Size(50, 20), "cbbTextSize");
            cLabel(new Point(25, 350), "Size : ", 12, FontStyle.Regular);

            cBtnColor(new Point(100, 400), new Size(80, 20), "colorText");
            cLabel(new Point(25, 400), "Color : ", 12, FontStyle.Regular);

            ///////////////////////////////////////////////////////////////////////////////////
            
            cLabel(new Point(10, 450), "Path", 15, FontStyle.Bold);

            cCombobox(listStypePath, new Point(100, 500), new Size(100, 20), "cbbStylePath");
            cLabel(new Point(25, 500), "Style : ", 12, FontStyle.Regular);

            cCombobox(new List<string>() { "1", "2", "3", "4" }, new Point(100, 550), new Size(50, 20), "cbbSizePath");
            cLabel(new Point(25, 550), "Size : ", 12, FontStyle.Regular);

            cBtnColor(new Point(100, 600), new Size(80, 20), "colorPath");
            cLabel(new Point(25, 600), "Color : ", 12, FontStyle.Regular);

        }

        public void updateFormatTable(string shape, Color colorNode, string font, int textsize, Color colorText, mPath path)
        {
            this.shapeNode = shape;
            this.colorNode = colorNode;
            this.fontText = font;
            this.sizeText = textsize;
            this.colorText = colorText;
            this.stylePath = path.type;
            this.sizePath = path.size;
            this.colorPath = path.color;

            foreach(Control control in this.Controls)
            {
                switch (control.Tag)
                {
                    case "cbbShape":
                        control.Text = this.shapeNode;
                        break;
                    case "cbbFont":
                        control.Text = this.fontText;
                        break;
                    case "cbbTextSize":
                        control.Text = this.sizeText.ToString();
                        break;
                    case "cbbStylePath":
                        control.Text = this.stylePath;
                        break;
                    case "cbbSizePath":
                        control.Text = this.sizePath.ToString();
                        break;
                    case "colorShape":
                        control.BackColor = this.colorNode;
                        break;
                    case "colorText":
                        control.BackColor = this.colorText;
                        break;
                    case "colorPath":
                        control.BackColor = this.colorPath;
                        break;
                }        
            }
            
        }

        private Label cLabel(Point location, string text, int textsize, FontStyle fontstype)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = location;
            label.FlatStyle = FlatStyle.Flat;
            label.Font = new Font("tahoma", textsize, fontstype);
            this.Controls.Add(label);
            return label;
        }

        private ComboBox cCombobox(List<string> listItem, Point location, Size size, string idtag)
        {
            ComboBox cbb = new ComboBox();

            cbb.Tag = idtag;
            cbb.Location = location;
            cbb.Size = size;
            cbb.DataSource = listItem;
            cbb.FlatStyle = FlatStyle.Flat;
            cbb.BackColor = Color.FromArgb(235, 235, 235);

            this.Controls.Add(cbb);

            switch (idtag)
            {
                case "cbbShape":
                    cbb.Text = this.shapeNode;
                    break;
                case "cbbFont":
                    cbb.Text = this.fontText;
                    break;
                case "cbbTextSize":
                    cbb.Text = this.sizeText.ToString();
                    break;
                case "cbbStylePath":
                    cbb.Text = this.stylePath;
                    break;
                case "cbbSizePath":
                    cbb.Text = this.sizePath.ToString();
                    break;
            }
            return cbb;
            
        }

        private Button cBtnColor(Point location, Size size, string idtag)
        {
            ColorDialog cd = new ColorDialog();
            Button btn = new Button();
            btn.Location = location;
            btn.Size = size;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Tag = idtag;
            btn.Click += BtnColor_Click;
            this.Controls.Add(btn);
            switch (idtag)
            {
                case "colorShape":
                    btn.BackColor = this.colorNode;
                    break;
                case "colorText":
                    btn.BackColor = this.colorText;
                    break;
                case "colorPath":
                    btn.BackColor = this.colorPath;
                    break;
            }
            return btn;
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
        }
    }
}

