using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.Converter.Base
{
    public interface IConverter<EntityModel, ViewModel>
         where EntityModel : class
         where ViewModel : class
    {
        EntityModel ConvertToEntity(ViewModel self, EntityModel returnValue);
        ViewModel ConvertToModel(EntityModel self, ViewModel returnValue);

    }
}
