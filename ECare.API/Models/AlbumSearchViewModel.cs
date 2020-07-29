using ECare.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECare.API.Models
{
    public class AlbumSearchViewModel
    {
        [Display(Name = "Search")]
        public string SearchWord { get; set; }

        [Display(Name = "SchoolCode")]
        public string SchoolCode { get; set; }

        public List<Album> SearchResults { get; set; }

        public AlbumSearchViewModel()
        {
            SearchResults = new List<Album>();
        }
    }

    public class AlbumCreateViewModel
    {
        [Required]
        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        [Required]
        [Display(Name = "SchoolCode")]
        public string SchoolCode { get; set; }
    }

    public class AlbumDetailsViewModel
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string AlbumImage { get; set; }
        public List<Photo> Photos { get; set; }
    }
}