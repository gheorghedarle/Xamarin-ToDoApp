using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public interface IFirestoreRepository<T>
    {
        T Get();
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllContains(string field, object value);
    }
}
