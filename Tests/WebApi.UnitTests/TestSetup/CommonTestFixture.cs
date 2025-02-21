using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture : IDisposable
    {
        public BookStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public CommonTestFixture()
        {
            ResetDatabase(); // Her yeni test için veritabanını sıfırla
            Mapper = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
        }

        private void ResetDatabase()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();

            Context.AddAuthors();
            Context.AddGenres();
            Context.AddBooks();
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
