//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECare.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> FineDay { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> StartMonth { get; set; }
    }
}