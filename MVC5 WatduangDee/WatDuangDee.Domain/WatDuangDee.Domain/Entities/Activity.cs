using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WatDuangDee.Domain.Entities
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string Topic { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public string description { get; set; }
       
        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public string Type { get; set; }
    }
}
