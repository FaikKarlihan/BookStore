using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture : IDisposable
    {
        public BookStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public IConfiguration Configuration {get; private set;}

        public CommonTestFixture()
        {
            ResetDatabase(); // Her yeni test için veritabanını sıfırla
            Mapper = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

            // In-memory Configuration ekleniyor
            var configData = new Dictionary<string, string>
            {
                { "Token:SecurityKey", "TestSuperSecretKey123!" },
                { "Token:Issuer", "TestIssuer" },
                { "Token:Audience", "TestAudience" }
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData) // In-memory ayarları ekle
                .Build();
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
            Context.AddUsers();
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
