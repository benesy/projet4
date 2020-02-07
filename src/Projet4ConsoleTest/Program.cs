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
           // ClearDb();

            projet4Entities ctx = new projet4Entities();
            MCaptor mc = new MCaptor();
            mc.Serial_number = "459";
            mc.Localisation = "ici";
            mc.Description = "plop";
            captor cpt = mc.ConvertToDao();
            ctx.captor.Add(cpt);
          
            //ctx.SaveChanges();
            



            List<MStatement> mStatementslist = Import.ConvertFile(Import.ReadFile("C:\\Users\\charl_000\\Desktop\\PROJET 4 C#\\import\\Sourcedonnees2.txt"));

            foreach (MStatement m in mStatementslist)
            {
                m.StatementId = cpt.captorId;
                ctx.statement.Add(m.ConvertToDao());
            }
            ctx.SaveChanges();
            Console.WriteLine("capteur : id {0} ajouté !", cpt.captorId);

            List<statement> ls = ctx.statement.Where(c => c.captorId == cpt.captorId).ToList();
            foreach (statement s in ls)
            {
                Console.WriteLine("Releve : {0} - {1} - {2} - {3} - {4}", s.captorId, s.statementId, s.humidity, s.temperature,s.dateTime);
            }

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
