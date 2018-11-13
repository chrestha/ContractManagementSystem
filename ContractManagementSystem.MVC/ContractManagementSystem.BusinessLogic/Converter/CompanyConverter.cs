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
            if (self.Tbl_Contact != null && self.Tbl_Contact.Count > 0)
            {
                foreach (var contact in self.Tbl_Contact)
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
}
