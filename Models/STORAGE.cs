//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MindMap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class STORAGE
    {
        public string NAME_S { get; set; }
        public int ID_BOARD { get; set; }
        public Nullable<System.DateTime> DATE_MODIFIED { get; set; }
    
        public virtual BOARD BOARD { get; set; }
    }
}
