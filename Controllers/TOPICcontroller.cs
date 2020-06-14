using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindMap.Models;

namespace MindMap.Controllers
{
    public class TOPICcontroller
    {
        public static int getID()
        {
            using (var _context = new MINDMAPEntities())
            {
                var id = (from t in _context.TOPICs
                          select t.ID).ToList();
                if (id.Count <= 0)
                {
                    return 1;
                }
                else
                {
                    return id.Max() + 1;
                }
            }
        }
    }
}
