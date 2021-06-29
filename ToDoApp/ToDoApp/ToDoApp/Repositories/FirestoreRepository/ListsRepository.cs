using Plugin.CloudFirestore;
using System;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public class ListsRepository : IFirestoreRepository<ListModel>
    {
        public ListModel Get()
        {
            throw new System.NotImplementedException();
        }

        public IQuery GetAll(string userId)
        {
            var query = CrossCloudFirestore.Current
                    .Instance
                    .Collection("lists")
                    .WhereEqualsTo("userId", userId);

            return query;
        }

        public IQuery GetAllContains(string userId, string field, object value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ListModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Add(ListModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(ListModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
