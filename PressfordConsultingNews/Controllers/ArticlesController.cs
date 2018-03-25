using PressfordConsultingNews.Models;
using PressfordConsultingNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PressfordConsultingNews.Controllers
{
    [RoutePrefix("Articles")]
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly Repository _repository;

        public ArticlesController()
        {
            _repository = new Repository();
        }

        [Route]
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var vm = new ArticlesViewModel();
            vm.Articles = _repository.GetAllArticle();

            return View(vm);
        }

        [Route("{articleId:int?}")]
        [Authorize(Roles = "User")]
        public ActionResult Article(int articleId)
        {
            var articles = _repository.GetArticle(articleId);

            return View(articles);
        }

        [Route("Editor/{articleId:int?}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Editor(int? articleId)
        {
            var article = new Article();

            if (articleId.HasValue)
            {
                article = _repository.GetArticle(articleId.Value);
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Save([Bind(Include = "Title,Content,CreateDate,UpdateDate,AuthorId,ArticleId")] Article article)
        {
            if (!ModelState.IsValid)
            {
                return View("Article", article);
            }

            if (article.ArticleId == 0)
            {
                article.CreateDate = DateTime.Now;
                article.UpdateDate = article.CreateDate;
                article.AuthorId = 1;

                _repository.SaveArticle(article);
            }
            else
            {
                var existingArticle = _repository.GetArticle(article.ArticleId);

                if (existingArticle == null)
                {
                    return View("Article", article);
                }

                existingArticle.Title = article.Title;
                existingArticle.Content = article.Content;
                existingArticle.UpdateDate = DateTime.Now;
                existingArticle.AuthorId = 1;

                _repository.UpdateArticle(existingArticle);
            }

            return RedirectToAction("Index", "Articles");
        }

        [Route("Delete/{articleId:int?}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? articleId)
        {
            if (!articleId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var existingArticle = _repository.GetArticle(articleId.Value);

            if (existingArticle == null) {

                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            _repository.DeleteArticle(existingArticle);

            return RedirectToAction(string.Empty);
        }
    }
}