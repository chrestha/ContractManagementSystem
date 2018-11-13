
using ContractManagementSystem.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Tbl_Contact> contacts { get; set; }
  
        /// <summary>
        /// Initilized SaveAsync method for Repository
        /// </summary>
        /// <remarks>aNISH 2017 Apri 27</remarks>
        /// <returns></returns>
        ///
        int Save();
        Task<int> SaveAsync();
    }
}
