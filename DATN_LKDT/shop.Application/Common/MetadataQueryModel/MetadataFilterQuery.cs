using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Model.Ultil
{
    public class MetadataFilterQuery
    {
        [MaxLength(3)]
        public virtual string CombineOperators { get; set; } = "AND";
        public virtual string LinkCombineOperators { get; set; }
        public virtual List<MetadataFilterObject> Filters { get; set; }
    }
}
