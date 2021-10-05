using Plugin.CloudFirestore;
using System.Threading.Tasks;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public interface IFirestoreRepository<T>
    {
        T Get();
        IQuery GetAll(string userId);
        IQuery GetAllContains(string userId, string field, object value);
        IQuery GetAllContains(string userId, string field1, object value1, string field2, object value2);
        IQuery GetAllContains(string userId, string field1, object value1, string field2, object value2, string field3, object value3);
        Task<bool> Update(T model);
        Task<bool> Add(T model);
        Task<bool> Delete(T model);
    }
}
