using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatDuangDee.Domain.Abstract;
using WatDuangDee.Domain.Entities;

namespace WatDuangDee.Domain.Concrete
{
    public class EFRepository : IRepository
    {
        private EFDbContext context = new EFDbContext();
 
        public IQueryable<Activity> Activity
        {
            get { return context.Activity; }
        }

        public IQueryable<Lesson> Lesson
        {
            get { return context.Lesson; }
        }


       
        public IQueryable<Gallery> Gallery { get { return context.Gallery; } }

        public IQueryable<History> History { get { return context.History; } }
        public void SaveHistory(History history)
        {
            History dbEntry = context.History.Find(history.HistoryId);
                if (dbEntry != null)
                     {
        dbEntry.HistoryDetail = history.HistoryDetail;
        dbEntry.ImageData = history.ImageData;
        dbEntry.ImageMimeType = history.ImageMimeType;
             }
                     
                context.SaveChanges();
        }

        public IQueryable<News> News { get { return context.News; } }

        public IQueryable<Video> Video { get { return context.Video; } }

        public IQueryable<QuestionAnswerDhamma> QuestionAnswerDhamma { get { return context.QuestionAnswerDhamma; } }


    }
}
