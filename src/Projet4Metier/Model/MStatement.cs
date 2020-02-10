using Projet4DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet4Metier
{
    public class MStatement
    {
        public int StatementId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public System.DateTime DateTime { get; set; }
        public int CaptorId { get; set; }



        public statement ConvertToDao()
        {
            statement s = new statement();
            s.statementId = StatementId;
            s.temperature = Temperature;
            s.humidity = Humidity;
            s.dateTime = DateTime;
            return s;
        }

        public void LoadFromDao(statement s)
        {
            StatementId = s.statementId;
            Temperature = s.temperature;
            Humidity = s.humidity;
            DateTime = s.dateTime;
        }

     

        public void Add(projet4Entities ctx)
        {
            ctx.statement.Add(ConvertToDao());
        }

        public void Remove(projet4Entities ctx)
        {
            ctx.statement.Remove(ConvertToDao());
        }
        public static List<MStatement> GetAll(projet4Entities ctx)
        {
            List<MStatement> ls = new List<MStatement>();
            List<statement> l = ctx.statement.ToList();
            foreach(statement s in l)
            {
                MStatement ms = new MStatement();
                ms.LoadFromDao(s);
                ls.Add(ms);
            }

            return ls;
        }
        public static List<MStatement> GetAllByCaptorId(projet4Entities ctx, int captorId)
        {
            List<MStatement> ls = new List<MStatement>();
            List<statement> l = ctx.statement.Where(st=> st.captorId==captorId).ToList();
            foreach (statement s in l)
            {
                MStatement ms = new MStatement();
                ms.LoadFromDao(s);
                ls.Add(ms);
            }

            return ls;
        }

        // get by date //rajouter by id
        public static List<MStatement> GetByDate(projet4Entities ctx, DateTime debut, DateTime fin)
        {
            List<MStatement> ls = new List<MStatement>();
            List<statement> l = ctx.statement.Where(s => s.dateTime >= debut && s.dateTime <= fin).ToList();
            foreach (statement s in l)
            {
                MStatement ms = new MStatement();
                ms.LoadFromDao(s);
                ls.Add(ms);
            }

            return ls;
        }
    }
}
