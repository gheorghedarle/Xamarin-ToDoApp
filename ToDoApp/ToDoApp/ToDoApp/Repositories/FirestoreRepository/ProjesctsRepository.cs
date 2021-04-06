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

        public async Task<IEnumerable<ProjectModel>> GetAll()
        {
            var document = await CrossCloudFirestore.Current
                    .Instance
                    .Collection("projects")
                    .GetAsync();

            return document.ToObjects<ProjectModel>();
        }

        public Task<IEnumerable<ProjectModel>> GetAllContains(string field, object value)
        {
            throw new NotImplementedException();
        }
    }
}
