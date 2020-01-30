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
           try {
                f1();
           }
            catch (Exception e)
           {
                Console.WriteLine("Tu sais pas coder ! : {0}", e);
            }
            

        }

        static void f1()
        {
            projet4DAO ctx = new projet4DAO();

            captor c = new captor();
           
            c.serial_number = "123454";
            c.localisation = "hangar a";
            c.description = "salade";

            ctx.captor.Add(c);
            ctx.SaveChanges();

           


        }

    }
}
