using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange
            (
                new Author{ Name = "Fyodor", Surname = "Dostoyevski", BirthDate = new DateTime(1821,11,11)},
                new Author{ Name = "Franz", Surname = "Kafka", BirthDate = new DateTime(1883,07,03)}
            );
        }
    }
}