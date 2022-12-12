using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whs.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(WhsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
