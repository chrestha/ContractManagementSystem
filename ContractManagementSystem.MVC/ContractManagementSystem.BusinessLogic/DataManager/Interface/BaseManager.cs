using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.DataManager.Interface
{
    public class BaseManager
    {
        //protected IUnitOfWork _unitOfWork;
        public BaseManager()
        {

        }
    }
    public class BaseManager<Converter> : BaseManager
    where Converter : class, new()
    {
        protected Converter converter;
        public BaseManager() : base()
        {
            converter = new Converter();
        }
    }
}
