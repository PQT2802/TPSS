using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TPSS.Business.Common;
using TPSS.Business.Exceptions.ErrorHandler;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

namespace TPSS.Business.Service.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;
        public ProjectService(IProjectRepository projectRepository, IConfiguration configuration, IImageService imageService, IAlbumRepository albumRepository)
        {
            _projectRepository = projectRepository;
            _imageService = imageService;
            _albumRepository = albumRepository;
        }

        public async Task<dynamic> CreateProjectAsync(ProjectDTO projectDTO, string userID)
        {
            try
            {
                List<Error> Errors = new List<Error>();

                Project project = new Project();

                if (string.IsNullOrEmpty(projectDTO.ProjectName))
                {
                    Errors.Add(ProjectErrors.ProjectNameIsEmpty);
                }
                if (string.IsNullOrEmpty(projectDTO.City))
                {
                    Errors.Add(ProjectErrors.CityIsEmpty);
                }
                if (string.IsNullOrEmpty(projectDTO.Ward))
                {
                    Errors.Add(ProjectErrors.WardIsEmpty);
                }
                if (string.IsNullOrEmpty(projectDTO.District))
                {
                    Errors.Add(ProjectErrors.DistrictIsEmpty);
                }
                if (string.IsNullOrEmpty(projectDTO.ProjectDescription))
                {
                    Errors.Add(ProjectErrors.ProjectDescriptionIsEmpty);
                }        
                if (projectDTO.Image == null)
                {
                    Errors.Add(CreateErrors.ImagesIsEmpty);
                }

                if (Errors.Count > 0)
                {
                    return Result.Failures(Errors);
                }

                project.ProjectId = await AutoGenerateProjectId();
                project.ProjectName = projectDTO.ProjectName;
                project.City = projectDTO.City;
                project.District = projectDTO.District;
                project.Ward = projectDTO.Ward;
                project.Status = "Waiting";
                project.IsDelete = false;
                project.Image = await _imageService.UploadAvatarAsync(projectDTO.Image, "Projects", project.ProjectId);
                
                int result1 = await _projectRepository.CreateProjectAsync(project);
                int result2 = 0;

                if (result1 == 1)
                {
                    ProjectDetail detail = new ProjectDetail();

                    detail.ProjectDetailId = await AutoGenerateProjectDetailId();
                    detail.ProjectId = project.ProjectId;
                    detail.ProjectDescription = projectDTO.ProjectDescription;

                    DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
                    detail.CreateDate = currentDate;
                    detail.UpdateDate = null;
                    detail.UpdateBy = null;
                    detail.CreateBy = userID;
                    detail.Verify = false;

                    result2 = await _projectRepository.CreateProjectDetailAsync(detail);

                    

                }

                return result2;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private async Task<string> AutoGenerateProjectId()
        {
            string newPropertyid = "";
            string latestPropertyId = await _projectRepository.GetLatestProjectIdAsync();
            if (latestPropertyId.IsNullOrEmpty())
            {
                newPropertyid = "PJ00000001";
            }
            else
            {
                int numericpart = int.Parse(latestPropertyId.Substring(2));
                int newnumericpart = numericpart + 1;

                newPropertyid = $"PJ{newnumericpart:d8}";
            }
            return newPropertyid;
        }

        private async Task<string> AutoGenerateProjectDetailId()
        {
            string newPropertyid = "";
            string latestPropertyDetailId = await _projectRepository.GetLatestProjectDetailIdAsync();
            if (latestPropertyDetailId.IsNullOrEmpty())
            {
                newPropertyid = "DD00000001";
            }
            else
            {
                int numericpart = int.Parse(latestPropertyDetailId.Substring(2));
                int newnumericpart = numericpart + 1;

                newPropertyid = $"DD{newnumericpart:d8}";
            }
            return newPropertyid;
        }

        public async Task<IEnumerable<dynamic>> GetAllProjects()
        {
            try
            {

                var resultList = await _projectRepository.GetAllProjects();

                return resultList;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
