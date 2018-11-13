using ContractManagementSystem.BusinessLogic.Converter;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
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
    public class ContactDM : BaseManager<ContactConverter>, IContactDM
    {
        public ContactDM(IUnitOfWork unitOfWork) : base()
        {
            _unitOfWork = unitOfWork;
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
                Tbl_Contact dbContact = _unitOfWork.Contacts.Find(x => x.ID.Equals(id));
                if (dbContact != null)
                {
                    contact = converter.ConvertToModel(dbContact);
                }

            }
            catch (Exception)
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

    }
}
