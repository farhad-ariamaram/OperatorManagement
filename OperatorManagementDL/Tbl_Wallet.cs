//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Tbl_Wallet
    {
        public int Fld_Wallet_Id { get; set; }
        public decimal Fld_Wallet_Balance { get; set; }
    
        public virtual Tbl_Sim Tbl_Sim { get; set; }
    }
}
