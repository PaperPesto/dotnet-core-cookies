using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6
{
    public interface ISalutante
    {
        string SayHello();
    }

    public interface ITransientSalutante : ISalutante { }
    public interface IScopedSalutante : ISalutante { }
    public interface ISingletonSalutante : ISalutante { }

    public class Salutatore : ITransientSalutante, IScopedSalutante, ISingletonSalutante
    {
        Guid Id { get; set; }

        public Salutatore()
        {
            Id = Guid.NewGuid();
        }
        public string SayHello()
        {
            return "Hello, my ID is " + Id;
        }
    }
}
