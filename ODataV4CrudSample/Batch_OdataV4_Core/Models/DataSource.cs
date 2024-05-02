using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Batch_OdataV4_Core.Models
{
    public static class DataSource
    {
        private static IList<Book> _books { get; set; }

        public static IList<Book> GetBooks()
        {
            if (_books != null)
            {
                return _books;
            }

            _books = new List<Book>();

            // book #1
            Book book = new Book
            {
                Id = "27664eec-3232-45bd-a8b5-fa1ca3b786c5",
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m
            };
            _books.Add(book);

            // book #2
            book = new Book
            {
                Id = "8be33eb2-cff6-4871-a7c3-f1a233d70184",
                ISBN = "063-6-920-02371-5",
                Title = "Enterprise Games",
                Author = "Michael Hugos",
                Price = 49.99m                
            };
            _books.Add(book);

            return _books;
        }
    }
}
