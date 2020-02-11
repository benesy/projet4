using Projet4DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Projet4Metier;

namespace Projet4ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDb();

            projet4Entities ctx = new projet4Entities();
            MCaptor mc = new MCaptor();
            mc.Serial_number = "459";
            mc.Localisation = "ici";
            mc.Description = "plop";
            captor cpt = mc.ConvertToDao();
            ctx.captor.Add(cpt);        
            ctx.SaveChanges();
            Console.WriteLine("capteur : id {0} ajouté !", cpt.captorId);
            Console.ReadLine();
        }

        public static void ClearDb()
        {
            projet4Entities ctx = new projet4Entities();
            List<captor> captorlist = ctx.captor.ToList();
            List<statement> statementlist = ctx.statement.ToList();
            foreach (captor c in captorlist)
            {
                ctx.captor.Remove(c);
            }

            foreach (statement s in statementlist)
            {
                ctx.statement.Remove(s);
            }
            ctx.SaveChanges();
        }
    }
}
