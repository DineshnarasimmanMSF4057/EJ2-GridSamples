using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Batch_OdataV4_Core.Models;
using Microsoft.AspNet.OData;

namespace Batch_OdataV4_Core.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ODataController
    {
        private BookStoreContext _db;


        public BooksController(BookStoreContext context)
        {
            _db = context;
            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                }
                context.SaveChanges();
            }

        }
        // GET api/values
        [HttpGet]
        [EnableQuery]
        public IQueryable<Book> Get()
        {
            return _db.Books;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody]Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Created(book);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<Book> Put(string id, [FromBody] Book book)
        {
            var entity = await _db.Books.FindAsync(book.Id);
            _db.Entry(entity).CurrentValues.SetValues(book);
            await _db.SaveChangesAsync();
            return book;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<int> Delete(int key)
        {
            var od = await _db.Books.FindAsync(key);

            _db.Books.Remove(od);
            await _db.SaveChangesAsync();
            return key;
        }
    }
}
