using Plugin.CloudFirestore;
using System;
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

        public Task<bool> Update(ProjectModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
