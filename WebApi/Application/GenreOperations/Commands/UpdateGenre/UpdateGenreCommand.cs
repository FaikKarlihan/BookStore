using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model;
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Id==GenreId);
            if(genre is null)
                throw new InvalidOperationException("Book type not found");

            if (_context.Genres.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("A book genre with the same name already exists");
            
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}