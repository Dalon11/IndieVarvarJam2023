
namespace Jam.Fabric.Initable.Abstraction
{
    /// <summary>
    /// ��������� ��� ���������� �����������.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInitable<T>
    {
        /// <summary>
        /// ����� ��� ���������� �����������. 
        /// </summary>
        /// <param name="model"></param>
        public void Init(T model);
    }
}