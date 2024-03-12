using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IProjectRepository
    {

        public Task<int> CreateProjectAsync(Project project);
        public Task<int> CreateProjectDetailAsync(ProjectDetail detail);

        public Task<IEnumerable<dynamic>> GetAllProjects();

        public Task<string> GetLatestProjectIdAsync();
        public Task<string> GetLatestProjectDetailIdAsync();
    }
}
