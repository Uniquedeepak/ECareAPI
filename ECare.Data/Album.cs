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
    
    public partial class Album
    {
        public Album()
        {
            this.Photos = new HashSet<Photo>();
        }
    
        public int ID { get; set; }
        public string ALBUM_NAME { get; set; }
        public string ALBUM_IMG { get; set; }
        public string SchoolCode { get; set; }
    
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
