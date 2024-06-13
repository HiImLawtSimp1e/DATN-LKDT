using System.ComponentModel.DataAnnotations;

namespace AppBusiness.Model.Ultil
{
    public  class MetadataFilterObject
    {
        [Required]
        public virtual string FieldName { get; set; }

        public virtual string LinkCombineOperators { get; set; }

        public virtual List<CompareModel> Compare { get; set; }
    }
}
