using Plugin.CloudFirestore;
using System;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public class ProjesctsRepository : IFirestoreRepository<ListModel>
    {
        public ListModel Get()
        {
            throw new System.NotImplementedException();
        }

        public IQuery GetAll(string userId)
        {
            var query = CrossCloudFirestore.Current
                    .Instance
                    .Collection("projects")
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
    }
}
