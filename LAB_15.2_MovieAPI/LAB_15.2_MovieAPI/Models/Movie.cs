using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_15._2_MovieAPI.Models
{
    public class Movie
    {

        private int id;
        private string title;
        private string category;
        private string year;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Category { get => category; set => category = value; }
        public string Year { get => year; set => year = value; }
    }
}
