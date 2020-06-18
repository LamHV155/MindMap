using MindMap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
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
         
        public static BOARD getBOARD(int idboard)
        {
            using(var _context = new MINDMAPEntities())
            {
                var board = (from b in _context.BOARDs.AsEnumerable()
                             where b.ID == idboard
                             select b).
                             Select(x => new BOARD
                             {
                                 ID = x.ID,
                                 COLOR = x.COLOR,
                                 WIDTH = x.WIDTH,
                                 HEIGHT = x.HEIGHT
                             }).ToList();
                return board[0];
                             
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

        public static bool updateBoard(BOARD board)
        {
            try
            {
               using(var _context = new MINDMAPEntities())
                {
                    _context.BOARDs.AddOrUpdate(board);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool deleteBoardAndStorage(int id)
        {
            try
            {
                using(var _context = new MINDMAPEntities())
                {
                    var board = _context.BOARDs.Where(x => x.ID == id).First();
                    board.STORAGEs.Clear();                   
                   _context.BOARDs.Remove(board);
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
