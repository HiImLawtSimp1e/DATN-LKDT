using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Model.Ultil
{
    public class CompareModel
    {
        [Required]
        public string FieldValues { get; set; }

        public string FieldOperator { get; set; } = "eq";


        public string DataType { get; set; }
    }
}
