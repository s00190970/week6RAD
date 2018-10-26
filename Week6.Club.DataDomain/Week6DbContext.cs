using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.Club.DataDomain
{
    class Week6DbContext: DbContext
    {
        public DbSet<Clubs> Clubs { get; set; }
        public DbSet<Members> ClubMembers { get; set; }
        public DbSet<Students> Students { get; set; }
        public Week6DbContext() : base("Week6Connection")
        {

        }

        public static Week6DbContext Create()
        {
            return new Week6DbContext();
        }
    }
}