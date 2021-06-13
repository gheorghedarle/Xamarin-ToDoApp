using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;

namespace ToDoApp.Droid.Repositories
{
    public class TasksRepository : IFirestoreRepository<TaskModel>
    {
        public TaskModel Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskModel>> GetAll(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskModel>> GetAllContains(string userId, string field, Java.Lang.Object value)
        {
            //var document = await FirebaseFirestore
            //    .Instance
            //    .Collection("tasks")
            //    .WhereEqualsTo("date", value)
            //    .WhereEqualsTo("userId", userId)
            //    .GetAsync();

            var document = FirebaseFirestore
                .Instance
                .Collection("tasks")
                .WhereEqualTo("date", value)
                .WhereEqualTo("userId", userId);

            return document;
        }
    }
}