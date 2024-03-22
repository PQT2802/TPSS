using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.Entities
{
    public class ProjectDetailWithRelatedProperties
    {
        public ProjectDetail PropertyDetail { get; set; }
        public IEnumerable<Property> RelatedProperties { get; set; }

        public ProjectDetailWithRelatedProperties(ProjectDetail propertyDetail, IEnumerable<Property> relatedProperties) 
        {
            PropertyDetail = propertyDetail;
            RelatedProperties = relatedProperties;
        }
    }
}
            