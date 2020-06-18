using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.Xml;
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

        public static bool addStorage(STORAGE storage)
        {
            try
            {
                using(var _context = new MINDMAPEntities())
                {
                    _context.STORAGEs.Add(storage);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<STORAGE> getListStorage()
        {
            using( var _context = new MINDMAPEntities())
            {
                var storage = (from s in _context.STORAGEs.AsEnumerable()
                               select s)
                               .Select(x => new STORAGE
                               {
                                   NAME_S = x.NAME_S,
                                   ID_BOARD = x.ID_BOARD,
                                   DATE_MODIFIED = x.DATE_MODIFIED
                               }).ToList();
                return storage;
            }
        }
        public static int getIDBoard(string name)
        {
            using( var _context = new MINDMAPEntities())
            {
                var storage = (from s in _context.STORAGEs.AsEnumerable()
                             where s.NAME_S == name
                             select s)
                             .Select(x => new STORAGE 
                             {
                                 NAME_S = x.NAME_S,
                                 ID_BOARD = x.ID_BOARD,
                                 DATE_MODIFIED = x.DATE_MODIFIED
                             }).ToList();
                return storage[0].ID_BOARD;
            }
        }
        
        public static bool updateStorage(STORAGE storage)
        {
            try
            {
                using(var _context = new MINDMAPEntities())
                {
                    _context.STORAGEs.AddOrUpdate(storage);
                    _context.SaveChanges();
                    return true;
                }            
            }
            catch
            {
                return false;
            }
        }


        public static string getNameStorage(int idboard)
        {
            using(var _context = new MINDMAPEntities())
            {
                var s = _context.STORAGEs.Where(x => x.ID_BOARD == idboard).First();
                return s.NAME_S;
            }
        }
    }
}
