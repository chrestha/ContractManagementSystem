using ContractManagementSystem.BusinessLogic.Converter.Base;
using ContractManagementSystem.BusinessLogic.ViewModel;
using ContractManagementSystem.Data.Database;

namespace ContractManagementSystem.BusinessLogic.Converter
{
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
