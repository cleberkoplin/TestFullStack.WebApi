using System;
using SimpleInjector;

namespace TestFullStack.Domain.Injection
{
    public class Kernel
    {
        private Container _kernel;

        public Container GetKernel()
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            return _kernel;
        }
        public void StartKernel()
        {
            _kernel = new Container();
        }


        private void StartBase()
        {
            _kernel = new Container();

        }

        public T Get<T>() where T : class
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            return _kernel.GetInstance<T>();
        }

        public void Bind<TFrom, TTo>()
            where TTo : class, TFrom
            where TFrom : class
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            _kernel.Register<TFrom, TTo>();
        }

        public void Bind(Type type1, Type type2)
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            _kernel.Register(type1, type2);
        }
    }
}
