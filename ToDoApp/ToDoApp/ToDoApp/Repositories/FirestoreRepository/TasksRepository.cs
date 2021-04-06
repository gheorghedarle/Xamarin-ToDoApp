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

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .GetAsync();

            return document.ToObjects<TaskModel>();
        }

        public async Task<IEnumerable<TaskModel>> GetAllContains(string field, object value)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("tasks")
                .WhereEqualsTo("date", value)
                .GetAsync();

            return document.ToObjects<TaskModel>();
        }
    }
}
