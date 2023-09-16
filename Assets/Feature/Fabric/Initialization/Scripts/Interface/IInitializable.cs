
namespace Jam.Fabric.Initable.Abstraction
{
    /// <summary>
    /// Интерфейс для добавления компонентов.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInitializable<T>
    {
        /// <summary>
        /// Метод для добавления компонентов. 
        /// </summary>
        /// <param name="model"></param>
        public void Init(T model);
    }
}