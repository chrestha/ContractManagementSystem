using ContractManagementSystem.BusinessLogic.Converter.Base;
using ContractManagementSystem.BusinessLogic.ViewModel;
using ContractManagementSystem.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.Converter
{
    public class CompanyConverter: IConverter<Tbl_Company, CompanyVM>
    {
        public Tbl_Company ConvertToEntity(CompanyVM self, Tbl_Company returnValue = null)
        {
            if (returnValue == null)
            {
                returnValue = new Tbl_Company();
            }
            returnValue.ID = self.ID;
            returnValue.Name = self.Name;
            returnValue.CompanyABN_CAN = self.CompanyABN_CAN;
            returnValue.Description = self.Description;
            returnValue.URL = self.URL;
            return returnValue;
        }

        public CompanyVM ConvertToModel(Tbl_Company self, CompanyVM returnValue = null)
        {
            if (returnValue == null)
            {
                returnValue = new CompanyVM();
            }
            returnValue.ID = self.ID;
            returnValue.Name = self.Name;
            returnValue.CompanyABN_CAN = self.CompanyABN_CAN;
            returnValue.Description = self.Description;
            returnValue.URL = self.URL;
            List<ContactVM> contacts = new List<ContactVM>();
            if (self.Tbl_Contacts != null && self.Tbl_Contacts.Count > 0)
            {
                foreach (var contact in self.Tbl_Contacts)
                {
                    contacts.Add(new ContactConverter().ConvertToModel(contact));
                }
                returnValue.Contacts = contacts;
            }            

            return returnValue;
        }
        public CompanyVM ConvertToModel(SpCompany self, CompanyVM returnValue = null)
        {
            if (returnValue == null)
            {
                returnValue = new CompanyVM();
            }
            returnValue.ID = self.ID??0;
            returnValue.Name = self.Name;
            returnValue.CompanyABN_CAN = self.CompanyABN_CAN;
            returnValue.Description = self.Description;
            returnValue.URL = self.URL;          

            return returnValue;
        }
    }
    public class ContactConverter : IConverter<Tbl_Contact, ContactVM>
    {
        public Tbl_Contact ConvertToEntity(ContactVM self, Tbl_Contact returnValue = null)
        {
            if (returnValue == null)
            {
                returnValue = new Tbl_Contact();
            }
            returnValue.ID = self.ID;
            returnValue.FirstName = self.FirstName;
            returnValue.LastName = self.LastName;
            returnValue.PhoneNo = self.PhoneNo;
            returnValue.ContractType = self.ContractType;
            returnValue.Email = self.Email;
            returnValue.TitleId = self.TitleId;
            return returnValue;
        }

        public ContactVM ConvertToModel(Tbl_Contact self, ContactVM returnValue = null)
        {
            if (returnValue == null)
            {
                returnValue = new ContactVM();
            }
            returnValue.ID = self.ID;
            returnValue.FirstName = self.FirstName;
            returnValue.LastName = self.LastName;
            returnValue.PhoneNo = self.PhoneNo;
            returnValue.ContractType = self.ContractType;
            returnValue.Email = self.Email;
            returnValue.TitleId = self.TitleId;
            returnValue.Title = self.Tbl_Title!=null?self.Tbl_Title.Title:"";
            return returnValue;
        }
    }
}
