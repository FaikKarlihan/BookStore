using System.Linq;
using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBookQuery;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.Application.UserOperations.Queries.GetUsers;
using WebApi.Application.UserOperations.Queries.GetUserDetail;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>()
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    
            CreateMap<Book, CreateBookModel>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.AuthorSurname, opt => opt.MapFrom(src => src.Author.Surname))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Author.BirthDate));

            CreateMap<Book, BookDetailViewModel>()
            .ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.Name} {src.Author.Surname}"));

            CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.Name} {src.Author.Surname}"))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));


            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, GetAuthorsQueryViewModel>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));
            
            CreateMap<Author, GetAuthorsDetailQueryViewModel>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));

            CreateMap<CreateAuthorModel, Author>();

            CreateMap<CreateUserModel, User>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<User, GetUsersViewModel>();
            CreateMap<User, GetUserDetailQueryViewModel>();
        }
    }
}
