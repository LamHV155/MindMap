using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
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
       
         
        
       
        public static bool addListTopic(List<TOPIC> listTopic)
        {
            try
            {
                using (var _context = new MINDMAPEntities())
                {
                    foreach(TOPIC topic in listTopic)
                    {
                        _context.TOPICs.Add(topic);
                    }
                    _context.SaveChanges(); 
                    return true;
                }
            }
            catch
            {
                return false;
            }    
        }

        public static List<TOPIC> getListTopic(int idboard)
        {
            using(var _context = new MINDMAPEntities())
            {
                var topics = (from t in _context.TOPICs.AsEnumerable()
                              where t.ID_BOARD == idboard
                              select t)
                              .Select(x => new TOPIC
                              {
                                  ID = x.ID,
                                  ID_BOARD = x.ID_BOARD,
                                  BOARD = x.BOARD,
                                  ID_PARENT = x.ID_PARENT,
                                  POS_X = x.POS_X,
                                  POS_Y = x.POS_Y,
                                  WIDTH = x.WIDTH,
                                  HEIGHT = x.HEIGHT,
                                  LABEL_TP = x.LABEL_TP,
                                  BACKCOLOR = x.BACKCOLOR,
                                  FORECOLOR = x.FORECOLOR,
                                  COLOR_PATH = x.COLOR_PATH,
                                  SIZE_PATH = x.SIZE_PATH,
                                  STYLE_PATH = x.STYLE_PATH,
                                  SHAPE = x.SHAPE,
                                  SIZE = x.SIZE,
                                  TEXT_SIZE = x.TEXT_SIZE,
                                  FONT = x.FONT
                              }).ToList();
                return topics;              
                    
            }
        }
        public static bool updateListTopic(List<TOPIC> listTopic)
        {
            try
            {
                using (var _context = new MINDMAPEntities())
                {
                    foreach (TOPIC topic in listTopic)
                    {
                       
                         _context.TOPICs.AddOrUpdate(topic);
                        
                    }
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

   

        public static bool removeNode(List<TOPIC> listTopic)
        {
            try
            {
                using (var _context = new MINDMAPEntities())
                {
                    foreach (TOPIC topic in listTopic)
                    {
                        var t = _context.TOPICs.Where(x => x.ID == topic.ID).First();
                        _context.TOPICs.Remove(t);
                    }
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
