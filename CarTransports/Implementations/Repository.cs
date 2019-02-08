using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarTransports.Interfaces;

namespace CarTransports.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly Data.AppContext context;

        public Repository(Data.AppContext context)
        {
            this.context = context;
        }

        public void Create(T entity)
        {
            context.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
            Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<SelectListItem> PopulateDropDown(string id, string description, string tableName, string order)
        {
            var selectList = new List<SelectListItem>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select " + id + ", " + description + " from " + tableName + " order by " + order;
                context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    foreach (var item in result)
                    {
                        selectList.Add(new SelectListItem() { Value = result[0].ToString(), Text = result[1].ToString() });
                    }
                }
            }

            return selectList;
        }
    }
}
