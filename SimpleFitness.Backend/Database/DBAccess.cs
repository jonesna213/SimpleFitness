using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Database {
    public class DBAccess<T> where T : BaseModel {
        internal DBContext context;
        internal DbSet<T> dbSet;

        public DBAccess(DBContext context) {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection() {
            return dbSet;
        }

        public void Commit() {
            context.SaveChanges();
        }

        public void Delete(string Id) {
            var item = Find(Id);
            if (context.Entry(item).State == EntityState.Detached) {
                dbSet.Attach(item);
                dbSet.Remove(item);
            }
        }

        public T Find(string Id) {
            return dbSet.Find(Id);
        }

        public void Insert(T item) {
            dbSet.Add(item);
        }

        public void Update(T item) {
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
