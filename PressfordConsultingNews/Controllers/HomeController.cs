using PressfordConsultingNews.Models;
using PressfordConsultingNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PressfordConsultingNews.Controllers
{
    public class HomeController : Controller
    {
        private const int DisplayTopXArticles = 6;
        private readonly Repository _repository;

        public HomeController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            var vm = new HomeViewModel();
            vm.Articles = _repository.GetTopArticlePreviews(DisplayTopXArticles);

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}