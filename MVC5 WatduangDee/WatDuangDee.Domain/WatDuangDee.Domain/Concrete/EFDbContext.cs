using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatDuangDee.Domain.Entities;

namespace WatDuangDee.Domain.Concrete
{
   public class EFDbContext : DbContext
    {
       
       public DbSet<Activity> Activity { get; set; }
       public DbSet<Lesson> Lesson { get; set; }

       

       public DbSet<Gallery> Gallery { get; set; }

       public DbSet<History> History { get; set; }

       public DbSet<News> News { get; set; }

       public DbSet<Video> Video { get; set; }

       public DbSet<QuestionAnswerDhamma> QuestionAnswerDhamma { get; set; }

       
    }
}
