using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<TaskModel>> GetAll(string userId)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .WhereEqualsTo("userId", userId)
                .GetAsync();

            return document.ToObjects<TaskModel>();
        }

        public async Task<IEnumerable<TaskModel>> GetAllContains(string userId, string field, object value)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .WhereEqualsTo("date", value)
                .WhereEqualsTo("userId", userId)
                .GetAsync();

            return document.ToObjects<TaskModel>();
        }
    }
}
