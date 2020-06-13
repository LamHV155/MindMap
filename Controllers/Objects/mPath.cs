using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Controllers.Objects
{ 
    public class mPath
    {
        public int size;
        public Color color;
        public string type;

        public mPath(int size, Color color, string type)
        {
            this.size = size;
            this.color = color;
            this.type = type;
        }
    }
}
