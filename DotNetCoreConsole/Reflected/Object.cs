using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreConsole.Reflected
{
    public class Object
    {
        public Object(object instance)
        {
            Instance = instance;
        }


        public AMember this[string name]
            => Properties.FirstOrDefault(x => x.Key == name).Value ??
               Fields.FirstOrDefault(x => x.Key == name).Value ??
               Methods.FirstOrDefault(x => x.Key == name).Value ??
               LocalVariables.FirstOrDefault(x => x.Key == name).Value as AMember;

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

        public Dictionary<string, Variable> LocalVariables { get; } = new Dictionary<string, Variable>();
    }
}