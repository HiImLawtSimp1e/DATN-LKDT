using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Model.Pagination
{
    public class Pagination<T> : IPagination
    {
        public int CurrentPage { get; set; }

        public int Pages { get; set; }

        public int PageResults { get; set; }

        //public int NumberOfRecords { get; set; }

        //public int TotalRecords { get; set; }

        public T? Result { get; set; }

        //public Pagination()
        //{
        //    CurrentPage = 1;
        //}

        //public Pagination(int currentPage, int pageResults)
        //{
        //    CurrentPage = currentPage;
        //    PageResults = pageResults;
        //}

        //public Pagination(int totalRecords, int currentPage, int pageResults)
        //{
        //    TotalRecords = totalRecords;
        //    CurrentPage = currentPage;
        //    PageResults = pageResults;
        //    NumberOfRecords = totalRecords;
        //    double a = (double)TotalRecords / (double)pageResults;
        //    Pages = (int)Math.Ceiling(a);
        //}

        //public Pagination(T result, int totalRecords, int currentPage, int pageResults)
        //{
        //    Result = result;
        //    TotalRecords = totalRecords;
        //    CurrentPage = currentPage;
        //    PageResults = pageResults;
        //    NumberOfRecords = totalRecords;
        //    double a = (double)TotalRecords / (double)pageResults;
        //    Pages = (int)Math.Ceiling(a);
        //}
    }
}
