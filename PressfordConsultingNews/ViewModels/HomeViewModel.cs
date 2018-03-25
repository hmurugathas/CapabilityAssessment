using PressfordConsultingNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PressfordConsultingNews.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ArticlePreview> Articles { get; set; }
    }
}