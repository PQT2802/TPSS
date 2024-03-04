using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.Entities
{
    public class PropertyDetailWithRelatedProperties
    {
        public PropertyDetail PropertyDetail { get; set; }
        public UserDetail UserDetail { get; set; }
        public string ProjectName { get; set; }
        public IEnumerable<Property> RelatedProperties { get; set; }

        public PropertyDetailWithRelatedProperties(PropertyDetail propertyDetail, IEnumerable<Property> relatedProperties, UserDetail userDetail, string projectName )
        {
            PropertyDetail = propertyDetail;
            RelatedProperties = relatedProperties;
            UserDetail = userDetail;
            ProjectName = projectName;
        }
    }
}
