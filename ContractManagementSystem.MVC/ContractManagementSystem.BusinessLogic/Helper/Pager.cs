using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.Helper
{
    public class Pager
    {
        public Pager()
        {
            CurrentPage = 1;
            PageSize = 20;
            TotalPages = 1;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }

        public void CalcululatePagesAndRecords(int totalRecords = 0)
        {
            this.TotalRecords = totalRecords;
            this.TotalPages = System.Convert.ToInt32(System.Math.Ceiling(this.TotalRecords / System.Convert.ToDouble(this.PageSize)));

        }
    }
    public class PagingService
    {
        public static IQueryable<T> QueryRecordsForPage<T>(IQueryable<T> query, int? currentPage, int? pageSize)
        {
            query = query.Skip(((int)currentPage - 1) * (int)pageSize).Take((int)pageSize);
            return query;
        }
    }
}
