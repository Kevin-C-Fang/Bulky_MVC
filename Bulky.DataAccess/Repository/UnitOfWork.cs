using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    // Gives access to all repositories that you want, but also instantiates all repos even if not needed.
    // Cleaner approach, but more monolithic or tiered.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        // Since save isn't relevant to the repository or the model just to the db context, we separate that functionality out 
        // so it can be applicable to multiple future models
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
