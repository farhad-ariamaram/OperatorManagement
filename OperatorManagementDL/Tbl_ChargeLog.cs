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
    
    public partial class Tbl_ChargeLog
    {
        public int Fld_ChargeLog_Id { get; set; }
        public int Fld_Sim_SimId { get; set; }
        public System.DateTime Fld_ChargeLog_Date { get; set; }
        public decimal Fld_ChargeLog_Value { get; set; }
    
        public virtual Tbl_Sim Tbl_Sim { get; set; }
    }
}