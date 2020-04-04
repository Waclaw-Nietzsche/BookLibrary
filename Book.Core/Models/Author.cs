using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Book.Core.Models
{
    public class Author
    {
        public Author()
        {
            Books = new Collection<Book>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}