using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context)
        {
            context.Users.AddRange
            (
                new User {Name = "Kobe", Surname = "Bryant", Email = "kobe@outlook.com", Password = "123456"},
                new User {Name = "Lebron", Surname = "James", Email = "lebron@outlook.com", Password = "654321"},
                new User {Name = "Stephen", Surname = "Curry", Email = "stephen@outlook.com", Password = "111111"}
            );
        }
    }
}