using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatDuangDee.Domain.Entities;
namespace WatDuangDee.Domain.Abstract
{
   public interface IRepository
    {
       
       IQueryable<Activity> Activity { get; }
       IQueryable<Lesson> Lesson { get; }

       

       IQueryable<Gallery> Gallery { get; }

       IQueryable<History> History { get; }

       void SaveHistory(History history);

       IQueryable<News> News { get; }

       IQueryable<Video> Video { get; }

       IQueryable<QuestionAnswerDhamma> QuestionAnswerDhamma { get; }
    }
}
