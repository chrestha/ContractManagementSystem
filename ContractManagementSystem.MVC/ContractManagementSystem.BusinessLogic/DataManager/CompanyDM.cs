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
    //According to given instruction I have Created Stored procedure for CURD operation. I also mapped SP of create update and delete in ADO.net
    public class CompanyDM : BaseManager<CompanyConverter>, ICompanyDM
    {
        /// <summary>
        /// Filter company using name and url using stored procedure
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="searchUrl"></param>
        /// <returns></returns>
        public List<CompanyVM> GetFilteredList(string searchName = "", string searchUrl = "")
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
                // we can add a method to log all exception messages
                throw;
            }
            return list;
        }/// <summary>
         /// Insert A new company Using Custom Stored Procedure
         /// </summary>
         /// <param name="companyVm"></param>
         /// <returns></returns>
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
        /// <summary>
        /// Update existing Company details
        /// </summary>
        /// <param name="companyVm"></param>
        /// <returns></returns>
        public int Update(CompanyVM companyVm)
        {
            int result = 0;
            try
            {
                using (var context = new CMS_DbEntities())
                {
                    Tbl_Company company = context.UserSP_CompanyById(companyVm.ID).FirstOrDefault();
                    converter.ConvertToEntity(companyVm, company);
                    //will execute UserSP_UpdateCompany
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }
        /// <summary>
        ///  ///delete for end user but changing Delete column 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(object id)
        {
            int result = 0;
            try
            {
                using (var context = new CMS_DbEntities())
                {
                    //Get company using SP UserSP_CompanyById
                    Tbl_Company company = context.UserSP_CompanyById((int)id).FirstOrDefault();
                    // my storeprocedure change delete status in database for Delete
                    context.Tbl_Company.Remove(company);
                    //will execute sp_DeleteStudentInfo 
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        /// <summary>
        /// returns company details using its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// This is the method that provide Paging mechanism using stored procedure but current not imilimented
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<CompanyVM> GetList(string searchValue, int CurrentPage, int PageSize, out int TotalRows)
        {
            List<CompanyVM> list = new List<CompanyVM>();
            try
            {
                using (CMS_DbEntities db = new CMS_DbEntities())
                {
                    List<SpCompany> companies = new List<SpCompany>();
                    companies = db.UserSP_GetCompany(searchValue, 1, 10, "Name", "ASC").ToList();
                    TotalRows = (companies.Count() > 0) ? (companies.FirstOrDefault().MaxRows ?? 0) : 0;
                    foreach (SpCompany company in companies)
                    {
                        list.Add(converter.ConvertToModel(company));
                    }
                }
            }
            catch (Exception ex)
            {
                // We can add a error logger to record all exception
                throw;
            }
            return list;
        }
    }
}
