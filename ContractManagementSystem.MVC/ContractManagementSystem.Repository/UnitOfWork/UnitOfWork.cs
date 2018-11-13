using ContractManagementSystem.Data.Database;
using ContractManagementSystem.Repository.Implimentation;
using ContractManagementSystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Create private Instance of dbcontext
        /// </summary>
        private CMS_DbEntities _context = null;
        public UnitOfWork()
        {
            _context = new CMS_DbEntities();
        }

        public UnitOfWork(CMS_DbEntities dbContext)
        {
            _context = dbContext;
        }
        /// <summary>
        /// 
        /// </summary>
        public CMS_DbEntities Context { get { return _context; } }
        /// <summary>




        #region Contacts
        private IRepository<Tbl_Contact> _contact;
        public IRepository<Tbl_Contact> contacts
        {
            get { return _contact ?? (_contact = new RepositoryBase<Tbl_Contact>(_context)); }
            set { _contact = value; }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            try
            {

                return _context.SaveChanges();

            }
            catch (OptimisticConcurrencyException ex)
            {

                throw ex;
            }
        }

        public Task<int> SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (OptimisticConcurrencyException ex)
            {

                throw ex;
            }
        }
    }
}
