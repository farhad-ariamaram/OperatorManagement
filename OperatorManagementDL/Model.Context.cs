﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OperatorManagementDL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OperatorManagementDBEntities : DbContext
    {
        public OperatorManagementDBEntities()
            : base("name=OperatorManagementDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Person> Tbl_Person { get; set; }
        public virtual DbSet<Tbl_Sim> Tbl_Sim { get; set; }
        public virtual DbSet<Tbl_SimType> Tbl_SimType { get; set; }
        public virtual DbSet<Tbl_Transaction> Tbl_Transaction { get; set; }
        public virtual DbSet<Tbl_TransactionType> Tbl_TransactionType { get; set; }
        public virtual DbSet<Tbl_Wallet> Tbl_Wallet { get; set; }
    }
}
