using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6
{
    public interface IService
    {
        string GetTransient();
        string GetScoped();
        string GetSingleton();
    }

    public class Service : IService
    {
        private readonly ITransientSalutante _transientsalutante;
        private readonly IScopedSalutante _scopedsalutante;
        private readonly ISingletonSalutante _singletonsalutante;

        public Service(ITransientSalutante transientsalutante, IScopedSalutante scopedsalutante, ISingletonSalutante singletonsalutante)
        {
            _transientsalutante = transientsalutante;
            _scopedsalutante = scopedsalutante;
            _singletonsalutante = singletonsalutante;
        }

        public string GetScoped() => _scopedsalutante.SayHello();

        public string GetSingleton() => _singletonsalutante.SayHello();

        public string GetTransient() => _transientsalutante.SayHello();
    }
}
