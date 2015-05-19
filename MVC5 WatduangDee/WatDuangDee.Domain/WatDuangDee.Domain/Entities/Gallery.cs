using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatDuangDee.Domain.Entities
{
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }
 
        public string Photo_Path {get; set;}
    }
}
