using Plugin.CloudFirestore;
using System.Threading.Tasks;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public interface IFirestoreRepository<T>
    {
        T Get();
        IQuery GetAll(string userId);
        IQuery GetAllContains(string userId, string field, object value);
        Task<bool> Update(T model);

    }
}
