using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions.ErrorHandler
{
    public static class ProjectErrors
    {
        
        public static Error ProjectNameIsEmpty => new(
            "Project.ProjectNameIsEmpty", $"ProjectName should not be empty !!!");
        public static Error CityIsEmpty => new(
            "Project.CityIsEmpty", $"City should not be empty !!!");
        public static Error DistrictIsEmpty => new(
            "Project.DistrictIsEmpty", $"District should not be empty !!!");
        public static Error WardIsEmpty => new(
            "Project.WardIsEmpty", $"Ward should not be empty !!!");
        public static Error ProjectDescriptionIsEmpty => new(
            "Project.ProjectDescriptionIsEmpty", $"ProjectDescription should not be empty !!!");
    }
}
