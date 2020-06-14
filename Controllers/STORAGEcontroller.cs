using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindMap.Models;

namespace MindMap.Controllers
{
    public class STORAGEcontroller
    {
        public static bool checkName(string txt)
        {
            using (var _context = new MINDMAPEntities())
            {
                var name = (from s in _context.STORAGEs
                            where s.NAME_S == txt
                            select s).ToList();
                if (name.Count >= 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
