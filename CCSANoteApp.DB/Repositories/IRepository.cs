using CCSANoteApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.DB.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T obj);
        T? GetById(Guid id);
        List<T> GetAll();
        void Update(T obj);
        void Delete(T obj);
        void DeleteById(Guid id);
        
    }
}
