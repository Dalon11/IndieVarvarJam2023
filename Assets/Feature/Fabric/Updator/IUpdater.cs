using System;
namespace Jam.Fabric.Updator.Abstraction
{
    public interface IUpdater
    {
        public event Action onUpdate;

        public event Action onFixedUpdate;
    }
}
