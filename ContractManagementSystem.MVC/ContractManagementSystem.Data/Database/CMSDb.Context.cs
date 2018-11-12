﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContractManagementSystem.Data.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CMS_DbEntities : DbContext
    {
        public CMS_DbEntities()
            : base("name=CMS_DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Company> Tbl_Company { get; set; }
        public virtual DbSet<Tbl_Contact> Tbl_Contact { get; set; }
        public virtual DbSet<Tbl_Title> Tbl_Title { get; set; }
    
        public virtual int UserSP_DeleteCompany(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UserSP_DeleteCompany", iDParameter);
        }
    
        public virtual ObjectResult<Tbl_Company> UserSP_GetCompany(string searchValue, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var searchValueParameter = searchValue != null ?
                new ObjectParameter("SearchValue", searchValue) :
                new ObjectParameter("SearchValue", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tbl_Company>("UserSP_GetCompany", searchValueParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<Tbl_Company> UserSP_GetCompany(string searchValue, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder, MergeOption mergeOption)
        {
            var searchValueParameter = searchValue != null ?
                new ObjectParameter("SearchValue", searchValue) :
                new ObjectParameter("SearchValue", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tbl_Company>("UserSP_GetCompany", mergeOption, searchValueParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> UserSP_InsertCompany(string name, string companyABN_CAN, string description, string uRL)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var companyABN_CANParameter = companyABN_CAN != null ?
                new ObjectParameter("CompanyABN_CAN", companyABN_CAN) :
                new ObjectParameter("CompanyABN_CAN", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var uRLParameter = uRL != null ?
                new ObjectParameter("URL", uRL) :
                new ObjectParameter("URL", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("UserSP_InsertCompany", nameParameter, companyABN_CANParameter, descriptionParameter, uRLParameter);
        }
    
        public virtual int UserSP_UpdateCompany(Nullable<int> iD, string name, string companyABN_CAN, string description, string uRL)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var companyABN_CANParameter = companyABN_CAN != null ?
                new ObjectParameter("CompanyABN_CAN", companyABN_CAN) :
                new ObjectParameter("CompanyABN_CAN", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var uRLParameter = uRL != null ?
                new ObjectParameter("URL", uRL) :
                new ObjectParameter("URL", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UserSP_UpdateCompany", iDParameter, nameParameter, companyABN_CANParameter, descriptionParameter, uRLParameter);
        }
    }
}
