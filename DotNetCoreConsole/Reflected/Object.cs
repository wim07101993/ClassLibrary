using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreConsole.Reflected
{
    public class Object
    {
        public Object(object instance, Type type)
        {
            Instance = instance;
        }

        public object Instance { get; }

        public Type Type => Instance.GetType();

        public Dictionary<string, Property> Properties
            => Type
                .GetProperties()
                .ToDictionary(x => x.Name, x => new Property(x, Instance));

        public Dictionary<string, Method> Methods
            => Type
                .GetMethods()
                .ToDictionary(x => x.Name, x => new Method(x, Instance));

        public Dictionary<string, Field> Fields
            => Type
                .GetFields()
                .ToDictionary(x => x.Name, x => new Field(x, Instance));

        public Dictionary<string, Object> LocalVariables { get; } = new Dictionary<string, Object>();
    }
}