using Projet4DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet4Metier.Service
{
   public class Manager
    {
        projet4Entities ctx = new projet4Entities();

        public void saveChanges()
        {
            ctx.SaveChanges();

            var changedEntriesCopy = ctx.ChangeTracker.Entries()
                                        .Where(e => e.State != EntityState.Detached)
                                        .ToList();
            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
        
        public List<MCaptor> GetCaptorList()
        {
           return MCaptor.GetAll(ctx);
        }

        public List<MStatement> GetAllStatement(MCaptor c)
        {
            return MStatement.GetAllByCaptorId(ctx, c.CaptorId);
        }
        public List<MStatement> GetAllStatementByDate(MCaptor c, DateTime start, DateTime end)
        {
            return MStatement.GetByDate(ctx, c.CaptorId, start, end);
        }
    }
}
