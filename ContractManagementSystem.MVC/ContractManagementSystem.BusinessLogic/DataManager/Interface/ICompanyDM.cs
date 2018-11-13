using ContractManagementSystem.BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.DataManager.Interface
{
    public interface ICompanyDM
    {
         List<CompanyVM> GetList(string searchValue, int CurrentPage, int PageSize, out int TotalRows);
         int Insert(CompanyVM model);
        int Update(CompanyVM companyVm);
        int Delete(object id);
        CompanyVM GetById(object id);
        List<CompanyVM> GetFilteredList(string searchName="", string searchUrl="");
    }
}
