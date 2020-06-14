using MindMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Controllers
{
    public class BOARDcontroller
    {
        public static int getID()
        {
            using(var _context = new MINDMAPEntities())
            {
                var id = (from b in _context.BOARDs
                          select b.ID).ToList();
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

        public static bool AddBoard(BOARD board)
        {
            try
            {
                using (var _context = new MINDMAPEntities())
                {
                    _context.BOARDs.Add(board);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
