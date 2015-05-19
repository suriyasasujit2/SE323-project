using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatDuangDee.Domain.Entities
{
   public class Video
    {
       [Key]
       public int VideoId { get; set; }
       
       public String Video_Path { get; set; }
    }
}
