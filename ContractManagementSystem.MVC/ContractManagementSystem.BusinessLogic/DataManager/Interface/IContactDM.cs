using ContractManagementSystem.BusinessLogic.Helper;
using ContractManagementSystem.BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.DataManager.Interface
{
    public interface IContactDM
    {
        List<ContactVM> GetAll();
        SimpleMoldelFilter<ContactVM> GetFilter(SimpleMoldelFilter<ContactVM> filterModel);
        ContactVM GetById(object id);
        int Insert(ContactVM model);
        int Update(ContactVM model);
        int Delete(object id);
        int ChangeStatus(object id);
        List<TitleVM> GetTitles();
    }
}
