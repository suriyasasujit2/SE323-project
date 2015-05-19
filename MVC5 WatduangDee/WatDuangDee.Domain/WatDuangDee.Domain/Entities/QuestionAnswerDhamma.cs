using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatDuangDee.Domain.Entities
{
   public class QuestionAnswerDhamma
    {
       [Key]
       public int QuestionId { get; set; }
       public string UserId { get; set; }
       [Required]
       public string Topic { get; set; }
       [Required]
       public string Question { get; set; }

       public string Answer { get; set; }
       [DataType(DataType.Date)]
       public DateTime Date { get; set; }

       public string Status { get; set; }
       public byte[] ImageData { get; set; }

       [HiddenInput(DisplayValue = false)]
       public string ImageMimeType { get; set; }
    }
}
