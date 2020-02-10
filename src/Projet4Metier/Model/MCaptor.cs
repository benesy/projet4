using Projet4DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet4Metier
{
    public class MCaptor
    {
        public int CaptorId { get; set; }
        public string Serial_number { get; set; }
        public string Description { get; set; }
        public string Localisation { get; set; }

        public captor ConvertToDao()
        {
            captor c = new captor();
            c.captorId = CaptorId;
            c.serial_number = Serial_number;
            c.description = Description;
            c.localisation = Localisation;

            return c;
         }

        public void LoadFromDao(captor c)
        {
            CaptorId = c.captorId;
            Serial_number = c.serial_number;
            Description = c.description;
            Localisation = c.localisation;
        }
        public void Add(projet4Entities ctx)
        {
            ctx.captor.Add(ConvertToDao());
        }

        public void Remove(projet4Entities ctx)
        {
            ctx.captor.Remove(ConvertToDao());
        }
        public static List<MCaptor> GetAll(projet4Entities ctx)
        {
            List<MCaptor> lc = new List<MCaptor>();
            List<captor> l = ctx.captor.ToList();
            foreach (captor c in l)
            {
                MCaptor mc = new MCaptor();
                mc.LoadFromDao(c);
                lc.Add(mc);
            }

            return lc;
        }
    }
}
