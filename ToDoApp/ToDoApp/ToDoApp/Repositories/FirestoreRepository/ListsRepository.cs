using Plugin.CloudFirestore;
using System;
using System.Diagnostics;
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

        public IQuery GetAllContains(string userId, string field1, object value1, string field2, object value2)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ListModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Add(ListModel model)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("lists")
                        .AddAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public Task<bool> Delete(ListModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
