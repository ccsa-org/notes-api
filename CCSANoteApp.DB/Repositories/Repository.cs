using CCSANoteApp.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.DB.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public Repository(SessionFactory sessionFactory)
        {
            _session = sessionFactory.GetSession();
        }

        public void Add(T obj)
        {
            _session.Save(obj);
            Commit();
        }

        public T? GetById(Guid id)
        {
            var model = _session.Query<T>().FirstOrDefault(x => x.Id.Equals(id));
            return model;
        }

        public List<T> GetAll()
        {
            var collection = _session.Query<T>().ToList();
            return collection;
        }

        public void Delete(T obj)
        {
            _session.Delete(obj);
            Commit();
        }

        public void DeleteById(Guid id)
        {
            var model = GetById(id);

            if (model != null)
            {
                _session.Delete(model);
                Commit();
            }
        }

        public void Update(T obj)
        {
            _session.Update(obj);
            Commit();
        }

        protected bool Commit()
        {
            using var transction = _session.BeginTransaction();
            try
            {
                if (transction.IsActive)
                {
                    _session.Flush();
                    transction.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                transction.Rollback();
                return false;
            }
        }


        protected readonly ISession _session;
    }
}
