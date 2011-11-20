﻿namespace Kreissl.Showcase.Model
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Autor { get; set; }

        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}
