using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Repositories.FirestoreRepository
{
    public class ProjesctsRepository : IFirestoreRepository<ProjectModel>
    {
        public ProjectModel Get()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ProjectModel>> GetAll(string userId)
        {
            var document = await CrossCloudFirestore.Current
                    .Instance
                    .Collection("projects")
                    .WhereEqualsTo("userId", userId)
                    .GetAsync();

            return document.ToObjects<ProjectModel>();
        }

        public Task<IEnumerable<ProjectModel>> GetAllContains(string userId, string field, object value)
        {
            throw new NotImplementedException();
        }
    }
}
