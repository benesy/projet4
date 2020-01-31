using Projet4DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet4ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ShowCaptors();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur ! : {0}", e);
            }

        }

        static captor AddCaptor(string serial, string location, string description)
        {
            projet4DAO ctx = new projet4DAO();
            captor c = new captor();

            c.serial_number = serial;
            c.localisation = location;
            c.description = description;
            ctx.captor.Add(c);
            ctx.SaveChanges();
            return c;
        }

        static void ShowCaptors()
        {
            projet4DAO ctx = new projet4DAO();
            List<captor> captorList;

            captorList = ctx.captor.ToList();
            captorList.ForEach(delegate (captor c)
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", c.captorId, c.serial_number, c.localisation, c.description);
            });
        }
    }
}
