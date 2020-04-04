using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Book.Core.Models
{
    public class AuthorModel
    {
        public AuthorModel()
        {
            Books = new Collection<BookModel>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookModel> Books { get; set; }
    }
}