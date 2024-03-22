using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions.ErrorHandler
{
    public static class CreateErrors
    {
        
        public static Error ProjectID(string ProjectID) => new(
            "Property.ProjectIDNotExist", $"The property with ProjectID '{ProjectID}' is not exist!!");
        public static Error AreaIsEmpty => new(
            "Property.AreaIsEmpty", $"Area should not be empty !!!");
        public static Error PropertyTitleIsEmpty => new(
            "Property.PropertyTitleIsEmpty", $"PropertyTitle should not be empty !!!");
        public static Error PriceIsEmpty => new(
            "Property.PriceIsEmpty", $"Price should not be empty !!!");
        public static Error CityIsEmpty => new(
            "Property.CityIsEmpty", $"City should not be empty !!!");
        public static Error DistrictIsEmpty => new(
            "Property.DistrictIsEmpty", $"District should not be empty !!!");
        public static Error WardIsEmpty => new(
            "Property.WardIsEmpty", $"Ward should not be empty !!!");
        public static Error StreetIsEmpty => new(
            "Property.StreetIsEmpty", $"Street should not be empty !!!");
        public static Error DescriptionIsEmpty => new(
            "Property.DescriptionIsEmpty", $"Description should not be empty !!!");
        public static Error ImagesIsEmpty => new(
            "Property.ImagesIsEmpty", $"Images should not be empty !!!");
    }

}
