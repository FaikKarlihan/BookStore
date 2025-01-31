using System;

namespace WebApi.Services
{
    public class DbLogger : IloggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DbLogger] - "+ message);
        }
    }
}