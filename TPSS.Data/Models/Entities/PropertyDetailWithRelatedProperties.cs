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
        public IEnumerable<Property> RelatedProperties { get; set; }

        public PropertyDetailWithRelatedProperties(PropertyDetail propertyDetail, IEnumerable<Property> relatedProperties)
        {
            PropertyDetail = propertyDetail;
            RelatedProperties = relatedProperties;
        }
    }
}
