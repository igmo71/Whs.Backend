using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whs.Domain;
using Microsoft.EntityFrameworkCore;

namespace Whs.Application.Interfaces
{
    public interface IWhsDbContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
