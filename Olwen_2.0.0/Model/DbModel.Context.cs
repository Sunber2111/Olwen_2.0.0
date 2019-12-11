﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Olwen_2._0._0.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Logistic> Logistics { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<PurchasingHeader> PurchasingHeaders { get; set; }
        public virtual DbSet<SalaryInfo> SalaryInfoes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreDetail> StoreDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<PurchasingDetail> PurchasingDetails { get; set; }
    
        [DbFunction("DbEntities", "GetOrdersByCusID")]
        public virtual IQueryable<GetOrdersByCusID_Result> GetOrdersByCusID(Nullable<int> maKH)
        {
            var maKHParameter = maKH.HasValue ?
                new ObjectParameter("maKH", maKH) :
                new ObjectParameter("maKH", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetOrdersByCusID_Result>("[DbEntities].[GetOrdersByCusID](@maKH)", maKHParameter);
        }
    
        [DbFunction("DbEntities", "GetOrdersDetailByOrderID")]
        public virtual IQueryable<GetOrdersDetailByOrderID_Result> GetOrdersDetailByOrderID(Nullable<int> maHD)
        {
            var maHDParameter = maHD.HasValue ?
                new ObjectParameter("maHD", maHD) :
                new ObjectParameter("maHD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetOrdersDetailByOrderID_Result>("[DbEntities].[GetOrdersDetailByOrderID](@maHD)", maHDParameter);
        }
    
        [DbFunction("DbEntities", "GetOrdersDetailInfoByOrderID")]
        public virtual IQueryable<GetOrdersDetailInfoByOrderID_Result> GetOrdersDetailInfoByOrderID(Nullable<int> maHD)
        {
            var maHDParameter = maHD.HasValue ?
                new ObjectParameter("maHD", maHD) :
                new ObjectParameter("maHD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetOrdersDetailInfoByOrderID_Result>("[DbEntities].[GetOrdersDetailInfoByOrderID](@maHD)", maHDParameter);
        }
    
        [DbFunction("DbEntities", "GetSlspFromCus")]
        public virtual IQueryable<GetSlspFromCus_Result> GetSlspFromCus(Nullable<int> maKH)
        {
            var maKHParameter = maKH.HasValue ?
                new ObjectParameter("maKH", maKH) :
                new ObjectParameter("maKH", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetSlspFromCus_Result>("[DbEntities].[GetSlspFromCus](@maKH)", maKHParameter);
        }
    
        [DbFunction("DbEntities", "GetSlspFromSup")]
        public virtual IQueryable<GetSlspFromSup_Result> GetSlspFromSup(Nullable<int> maNSX)
        {
            var maNSXParameter = maNSX.HasValue ?
                new ObjectParameter("maNSX", maNSX) :
                new ObjectParameter("maNSX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetSlspFromSup_Result>("[DbEntities].[GetSlspFromSup](@maNSX)", maNSXParameter);
        }
    }
}
