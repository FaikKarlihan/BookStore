using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.DeleteGenre
{
    public class DeleteGenreComand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _contex;

        public DeleteGenreComand(IBookStoreDbContext contex)
        {
            _contex = contex;
        }

        public void Handle()
        {
            var genre = _contex.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Book type not found");
            
            _contex.Genres.Remove(genre);
            _contex.SaveChanges();
        }
    }
}