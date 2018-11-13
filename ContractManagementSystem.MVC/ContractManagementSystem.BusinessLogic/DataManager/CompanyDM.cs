using ContractManagementSystem.BusinessLogic.Converter;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
using ContractManagementSystem.BusinessLogic.ViewModel;
using ContractManagementSystem.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ContractManagementSystem.BusinessLogic.DataManager
{
    public class CompanyDM : BaseManager<CompanyConverter>, ICompanyDM
    {
        public List<CompanyVM> GetList(string searchValue, int CurrentPage, int PageSize, out int TotalRows)
        {
            List<CompanyVM> list = new List<CompanyVM>();
            try
            {
                using (CMS_DbEntities db = new CMS_DbEntities())
                {
                    List<SpCompany> companies = new List<SpCompany>();
                    companies= db.UserSP_GetCompany(searchValue, 1, 10, "Name", "ASC").ToList();                    
                    TotalRows= (companies.Count()>0)? (companies.FirstOrDefault().MaxRows??0):0;
                    foreach (SpCompany company in companies)
                    {
                        list.Add(converter.ConvertToModel(company));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<CompanyVM> GetFilteredList(string searchName="",string searchUrl="")
        {
            List<CompanyVM> list = new List<CompanyVM>();
            try
            {
                using (CMS_DbEntities db = new CMS_DbEntities())
                {
                    List<Tbl_Company> companies = new List<Tbl_Company>();
                    companies = db.UserSP_GetCompanyByName_URL(searchName, searchUrl).ToList();                    
                    foreach (Tbl_Company company in companies)
                    {
                        list.Add(converter.ConvertToModel(company));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public int Insert(CompanyVM companyVm)
        {
            int result = 0;
            try
            {                
                using (var context = new CMS_DbEntities())
                {
                    context.Tbl_Company.Add(converter.ConvertToEntity(companyVm));
                    //will execute UserSP_InsertCompany
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;

        }
        public int Update(CompanyVM companyVm)
        {
            int result = 0;
            try
            {
                using (var context = new CMS_DbEntities())
                {
                    Tbl_Company company = context.UserSP_CompanyById(companyVm.ID).FirstOrDefault();
                    converter.ConvertToEntity(companyVm,company);
                    //will execute UserSP_UpdateCompany
                    context.SaveChanges();

                }            
            }
            catch (Exception)
            {

                throw;
            }
            return result;

        }
        public int Delete(object id)
        {
            int result = 0;
            try
            {
                using (var context = new CMS_DbEntities())
                {
                    //Get company using SP UserSP_CompanyById
                    Tbl_Company company = context.UserSP_CompanyById((int)id).FirstOrDefault();
                    context.Tbl_Company.Remove(company);
                    //will execute sp_DeleteStudentInfo 
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public CompanyVM GetById(object id)
        {
            CompanyVM result = new CompanyVM();
            try
            {
                
                using (var context = new CMS_DbEntities())
                {
                    Tbl_Company company = context.UserSP_CompanyById((int)id).FirstOrDefault();
                    converter.ConvertToModel(company, result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
