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
            throw new NotImplementedException();
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
            var query = CrossCloudFirestore.Current
                .Instance
                .Collection("lists")
                .WhereEqualsTo(field, value)
                .WhereEqualsTo("userId", userId);
            return query;
        }

        public IQuery GetAllContains(string userId, string field1, object value1, string field2, object value2)
        {
            var query = CrossCloudFirestore.Current
                .Instance
                .Collection("lists")
                .WhereEqualsTo(field1, value1)
                .WhereEqualsTo(field2, value2)
                .WhereEqualsTo("userId", userId);
            return query;
        }

        public IQuery GetAllContains(string userId, string field1, object value1, string field2, object value2, string field3, object value3)
        {
            var query = CrossCloudFirestore.Current
                .Instance
                .Collection("lists")
                .WhereEqualsTo(field1, value1)
                .WhereEqualsTo(field2, value2)
                .WhereEqualsTo(field3, value3)
                .WhereEqualsTo("userId", userId);
            return query;
        }

        public async Task<bool> Update(ListModel model)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("lists")
                        .Document(model.Id)
                        .UpdateAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
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

        public async Task<bool> Delete(ListModel model)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("lists")
                        .Document(model.Id)
                        .DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
