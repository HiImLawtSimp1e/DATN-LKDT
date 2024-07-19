using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Model.Pagination
{
    public interface IPagination
    {
        int CurrentPage { get; set; }

        int Pages { get; set; }

        int PageResults { get; set; }

        int NumberOfRecords { get; set; }

        int TotalRecords { get; set; }
    }
}
