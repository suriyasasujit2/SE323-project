using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WatDuangDee.Domain.Entities
{
   public class News
    {
       [Key]
       public int NewsId { get; set; }

       [Required]
       public string Topic { get; set; }

       [DataType(DataType.Date)]
       public DateTime Date { get; set; }
       public string Description { get; set; }
       public byte[] ImageData { get; set; }

       [HiddenInput(DisplayValue = false)]
       public string ImageMimeType { get; set; }
    }
}
