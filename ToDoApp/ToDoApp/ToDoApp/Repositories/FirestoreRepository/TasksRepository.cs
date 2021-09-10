using Plugin.CloudFirestore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public class TasksRepository : IFirestoreRepository<TaskModel>
    {
        public TaskModel Get()
        {
            throw new NotImplementedException();
        }

        public IQuery GetAll(string userId)
        {
            var query = CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .WhereEqualsTo("userId", userId);

            return query;
        }

        public IQuery GetAllContains(string userId, string field, object value)
        {
            var query = CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .WhereEqualsTo(field, value)
                .WhereEqualsTo("userId", userId);
            return query;
        }

        public IQuery GetAllContains(string userId, string field1, object value1, string field2, object value2)
        {
            var query = CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .WhereEqualsTo(field1, value1)
                .WhereEqualsTo(field2, value2)
                .WhereEqualsTo("userId", userId);
            return query;
        }

        public async Task<bool> Update(TaskModel model)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("tasks")
                        .Document(model.id)
                        .UpdateAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Add(TaskModel model)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("tasks")
                        .AddAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(TaskModel model)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("tasks")
                        .Document(model.id)
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
