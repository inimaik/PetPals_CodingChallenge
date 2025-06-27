using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dao
{
    public class AdoptionEventContext : DbContext
    {
        public AdoptionEventContext() : base(DbPropertyUtil.GetConnectionString())
        {
        }
        public DbSet<AdoptionEvent> AdoptionEvents { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}