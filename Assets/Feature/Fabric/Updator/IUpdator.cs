using System;
namespace Jam.Fabric.Updator.Abstraction
{
    public interface IUpdator
    {
        public event Action OnUpdate;
    }
}
