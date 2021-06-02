using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public interface IFirestoreRepository<T>
    {
        T Get();
        Task<IEnumerable<T>> GetAll(string userId);
        Task<IEnumerable<T>> GetAllContains(string userId, string field, object value);
    }
}
