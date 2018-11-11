using System;
using System.Collections.Generic;

namespace DotNetCoreConsole
{
    internal class Program
    {
        public static List<Person> People { get; set; }

        private static void Main()
        {
            GeneratePeople();
        }

        private static void GeneratePeople()
        {
            var bob = new Person
            {
                Id = 0,
                Name = "Bob",
                BirthDay = new DateTime(2000, 10, 30),
                Hobbies = new List<string> {"Football"},
            };
            var nina = new Person
            {
                Id = 1,
                Name = "Nina",
                BirthDay = new DateTime(2002, 8, 7),
                Hobbies = new List<string> {"Ballet", "Music (singing)"},
            };
            var john = new Person
            {
                Id = 2,
                Name = "John",
                BirthDay = new DateTime(1979, 5, 23),
                Hobbies = new List<string> {"Football", "Children"},
                Children = new List<Person> {bob, nina},
            };
            var ella = new Person
            {
                Id = 3,
                Name = "Ella",
                BirthDay = new DateTime(1977, 3, 12),
                Hobbies = new List<string> {"Children", "Art"},
                Children = new List<Person> {bob, nina},
            };

            People = new List<Person> {ella, john, bob, nina};
        }
    }
}