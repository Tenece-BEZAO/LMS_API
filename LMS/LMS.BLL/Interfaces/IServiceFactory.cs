namespace LMS.BLL.Interfaces
{
    public interface IServiceFactory
    {
        T GetService<T>() where T : class;
    }
}