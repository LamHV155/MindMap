using MindMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindMap.Controllers
{
    public class MENUcontroller
    {
        public static List<MENU> getListMenu()
        {
            using (var _context = new MINDMAPEntities())
            {
                var menus = (from m in _context.MENUs.AsEnumerable()
                             select m)
                             .Select(x => new MENU
                             {
                                 ID = x.ID,
                                 SHAPE_CHILD = x.SHAPE_CHILD,
                                 SHAPE_PARENT = x.SHAPE_PARENT,
                                 COLOR_BOARD = x.COLOR_BOARD,
                                 COLOR_CHILD = x.COLOR_CHILD,
                                 COLOR_PARENT = x.COLOR_PARENT,
                                 COLOR_PATH = x.COLOR_PATH,
                                 STYLE_PATH = x.STYLE_PATH

                             }).ToList();
                return menus;
            }
        }
    }
}
