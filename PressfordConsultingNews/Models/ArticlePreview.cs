﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PressfordConsultingNews.Models
{
    public class ArticlePreview
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}