using ContractManagementSystem.BusinessLogic.Converter;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
using ContractManagementSystem.BusinessLogic.Helper;
using ContractManagementSystem.BusinessLogic.ViewModel;
using ContractManagementSystem.Data.Database;
using ContractManagementSystem.Repository.Interfaces;
using ContractManagementSystem.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.DataManager
{
    // For Contact I use Unit of work with repository pattern it has all functionality like exec SP, Exists, all, run raw sql
    public class ContactDM : BaseManager<ContactConverter>, IContactDM
    {
        public ContactDM(IUnitOfWork unitOfWork) : base()
        {
            _unitOfWork = unitOfWork;
        }
        public SimpleMoldelFilter<ContactVM> GetFilter(SimpleMoldelFilter<ContactVM> filterModel)
        {
            IQueryable<Tbl_Contact> query = _unitOfWork.Contacts.Filter(x => x.ID > 0).OrderBy(x => x.FirstName);
            if (!String.IsNullOrEmpty(filterModel.field1))
            {
                query = query.Where(x => x.FirstName.Contains(filterModel.field1) || x.LastName.Contains(filterModel.field1));
            }
            if (!String.IsNullOrEmpty(filterModel.field2))
            {
                query = query.Where(x => x.Department.Contains(filterModel.field2));
            }
            if (filterModel.field3 > 0)
            {
                query = query.Where(x => x.ContractType.Equals(filterModel.field3));
            }
            if (query.Any())
            {

                filterModel.Pager.TotalRecords = query.Count();
                query = PagingService.QueryRecordsForPage(query, filterModel.Pager.CurrentPage, filterModel.Pager.PageSize);
                filterModel.Pager.CalcululatePagesAndRecords(filterModel.Pager.TotalRecords
                    );

                List<ContactVM> list = new List<ContactVM>();
                if (filterModel.Pager.TotalRecords > 0)
                {
                    foreach (var dbStudent in query.ToList())
                    {
                        list.Add(converter.ConvertToModel(dbStudent));
                    }
                }
                filterModel.ListOfItemsToShow = list.OrderBy(x => x.FullName);
            }
            else
            {
                filterModel.Pager = new Helper.Pager();
            }

            return filterModel;
        }

        public List<ContactVM> GetAll()
        {
            List<ContactVM> contactList = new List<ContactVM>();
            try
            {
                var dbContactList = _unitOfWork.Contacts.All();
                if (dbContactList != null)
                {
                    foreach (var dbContact in dbContactList)
                    {
                        contactList.Add(converter.ConvertToModel(dbContact));
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return contactList;
        }
        public ContactVM GetById(object id)
        {
            ContactVM contact = new ContactVM();
            try
            {
                
                Tbl_Contact dbContact = _unitOfWork.Contacts.Find(id);
                if (dbContact != null)
                {
                    contact = converter.ConvertToModel(dbContact);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return contact;

        }
        public int Insert(ContactVM model)
        {
            int result = 0;
            try
            {
                Tbl_Contact contact = converter.ConvertToEntity(model);
                _unitOfWork.Contacts.Insert(contact);
                result = _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }
        public int ChangeStatus(object id)
        {
            int result = 0;
            try
            {
                Tbl_Contact dbContact = _unitOfWork.Contacts.Find(id);
                dbContact.Status = dbContact.Status ? false:true ;               
                _unitOfWork.Contacts.Update(dbContact);
                result = _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
            return result;

        }
        public int Update(ContactVM model)
        {
            int result = 0;
            try
            {
                Tbl_Contact dbContact = _unitOfWork.Contacts.Find(model.ID);
                dbContact = converter.ConvertToEntity(model, dbContact);
                _unitOfWork.Contacts.Update(dbContact);
                result = _unitOfWork.Save();
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
                Tbl_Contact dbContact = _unitOfWork.Contacts.Find(id);
                _unitOfWork.Contacts.Delete(dbContact);
                result = _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

         public List<TitleVM> GetTitles()
        {
            List<TitleVM> list = new List<TitleVM>();
            try
            { // can run raw sql query directly
               var titles  = _unitOfWork.Titles.GetWithRawSql("Select * from Tbl_Title").ToList();
                foreach (var item in titles)
                {
                    list.Add(new TitleVM() { Id = item.ID, Title = item.Title });
                }
            }
            catch (Exception ex)
            {

                throw;
            }         
          
            return list;
        }
    }
}
