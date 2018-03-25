using PressfordConsultingNews.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PressfordConsultingNews.Models
{
    public class Repository
    {
        private ApplicationDbContext _newsContext;

        public Repository()
        {
            _newsContext = new ApplicationDbContext();
        }

        public List<ArticlePreview> GetTopArticlePreviews(int top)
        {
            return (from article in _newsContext.Articles
                    join author in _newsContext.Authors
                    on article.AuthorId equals author.AuthorId
                    orderby article.UpdateDate descending
                    select new ArticlePreview
                    {
                        ArticleId = author.AuthorId,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        Title = article.Title,
                        Content = article.Content,
                        Date = article.UpdateDate
                    }).Take(top).ToList();
        }

        public List<Article> GetAllArticle()
        {
            return (from article in _newsContext.Articles.Include(a=> a.Author)                    
                    orderby article.UpdateDate descending
                    select article).ToList();
        }

        public Article GetArticle(int articleId)
        {
            return (from article in _newsContext.Articles.Include(a => a.Author)
                    where article.ArticleId == articleId
                    select article).FirstOrDefault();
        }
        
        public void SaveArticle(Article article)
        {
            _newsContext.Articles.Add(article);
            _newsContext.SaveChanges();
        }

        public void UpdateArticle(Article article)
        {
            _newsContext.Entry(article).State = EntityState.Modified;            
            _newsContext.SaveChanges();
        }

        public void DeleteArticle(Article article)
        {
            _newsContext.Articles.Remove(article);
            _newsContext.SaveChanges();
        }
    }
}