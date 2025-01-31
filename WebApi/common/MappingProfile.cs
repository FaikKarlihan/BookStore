using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBookQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, GetAuthorsQueryViewModel>().ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyyy")));
            CreateMap<Author, GetAuthorsDetailQueryViewModel>().ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyyy")));
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}
