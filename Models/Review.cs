﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Autor { get; set; }
        public int StarRating { get; set; }
        public Review(DateTime date, string text, string autor, int starrating)
        {
            Date = date;
            Text = text;
            Autor = autor;
            StarRating = starrating;
        }
        public Review(DateTime date, int starrating, string title)
        {
            Date = date;
            StarRating = starrating;
            Title = title;
        }
    }
}
