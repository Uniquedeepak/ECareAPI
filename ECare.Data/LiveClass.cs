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
    
    public partial class LiveClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public string Link { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
