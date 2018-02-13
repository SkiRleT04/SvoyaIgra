using Core.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Db
{
    class ApplicationContext:DbContext
    {
        public ApplicationContext() : base("SvoyaIgra") { this.Configuration.AutoDetectChangesEnabled = false; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
