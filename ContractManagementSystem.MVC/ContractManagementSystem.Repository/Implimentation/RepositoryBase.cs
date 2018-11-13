
using ContractManagementSystem.Data.Database;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using ContractManagementSystem.Repository.Interfaces;
using System.Web.UI.WebControls;

namespace ContractManagementSystem.Repository.Implimentation
{
    /// <summary>
    /// Generic Repository base class
    /// </summary>
    /// <typeparam name="TEntity">Object to be processed</typeparam>
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private members
        internal CMS_DbEntities context;
        private readonly DbSet<TEntity> _dbSet;
        private const bool ShareContext = false;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public RepositoryBase()
        {
            context = new CMS_DbEntities();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(CMS_DbEntities context)
        {
            this.context = context;
            _dbSet = context.Set<TEntity>();
        }

        #endregion

        #region Protected properties


        /// <summary>
        /// 
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).AsEnumerable();
            }
            return query.AsEnumerable();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetOrderBy(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, bool>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return query.OrderBy(orderBy);
            }
            return query.AsEnumerable();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                _dbSet.Attach(entityToUpdate);
            }

            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity oldEntity, TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                _dbSet.Attach(entityToUpdate);
            }
            context.Entry(oldEntity).CurrentValues.SetValues(entityToUpdate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctxt"></param>
        /// <param name="originalEntity"></param>
        /// <param name="entityToUpdate"></param>
        public virtual void DetachAndUpdate(DbContext ctxt, TEntity originalEntity, TEntity entityToUpdate)
        {
            var objectContext = ((IObjectContextAdapter)ctxt).ObjectContext;
            var objSet = objectContext.CreateObjectSet<TEntity>();
            var entityKey = objectContext.CreateEntityKey(objSet.EntitySet.Name, originalEntity);

            Object foundEntity;
            var exists = objectContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (!exists)
            {
                ctxt.Set<TEntity>().Attach(entityToUpdate);
                ctxt.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                objectContext.Detach(foundEntity);
                ctxt.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Boolean Exists(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // TryGetObjectByKey attaches a found entity
            // Detach it here to prevent side-effects
            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> All()
        {
            return DbSet.AsEnumerable<TEntity>().ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }


        /// <summary>
        /// filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }


        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }


        /// <summary>
        /// get the count of the items
        /// </summary>
        public int Count
        {
            get
            {
                return DbSet.Count();
            }
        }


        /// <summary>
        /// Dispose the context
        /// </summary>
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }


        /// <summary>
        /// Create new 
        /// </summary>
        /// <param name="tEntity"></param>
        /// <returns></returns>
        public TEntity Create(TEntity tEntity)
        {
            var newEntry = DbSet.Add(tEntity);
            //if (ShareContext)
            context.SaveChanges();
            return newEntry;
        }


        /// <summary>
        /// Delete the data
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (ShareContext)
                return context.SaveChanges();
            return 0;
        }


        /// <summary>
        /// Fetch data from database using pag number
        /// </summary>
        /// <param name="totalPages"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="listSortDirection"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetWithPaging(out int totalPages, Expression<Func<TEntity, bool>> filter = null,
            int page = 0, int pageSize = 0, string orderBy = null, SortDirection listSortDirection = SortDirection.Ascending)
        {
            if (page == default(int))
            {
                page = 1;
            }

            if (pageSize == default(int))
            {
                pageSize = 10;
            }

            var query = DbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy == null || typeof(TEntity).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                var defaultOrderBy = typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name.ToLower().Contains("id")).Name;
                query = (listSortDirection == SortDirection.Ascending) ? query.OrderBy(defaultOrderBy) : query.OrderByDescending(defaultOrderBy);
            }
            else
            {
                query = (listSortDirection == SortDirection.Ascending) ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            totalPages = (int)Math.Ceiling((double)query.Count() / (double)pageSize);

            if (pageSize != default(int))
            {
                query = query.Skip(pageSize * (page - 1)).Take(pageSize);
            }

            return query.ToList();
        }


        /// <summary>
        /// ExecWithStoreProcedure to get the single value from database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>int value</returns>
        //to run stored procedre
        public int ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(query, parameters);

        }

        /// <summary>
        /// To execute the ExecReadWithStoreProcedure
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>List of items</returns>
        //to read from stored procedure
        public IEnumerable<TEntity> ExecReadWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(query, parameters).ToList();
        }


        /// <summary>
        /// To Execute the raw query of sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(sql, parameters);
        }

        #endregion
    }
}
