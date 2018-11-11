using System;
using System.Collections.Generic;

namespace DotNetCoreConsole
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public List<string> Hobbies { get; set; }
        public List<Person> Children { get; set; }
    }
}
