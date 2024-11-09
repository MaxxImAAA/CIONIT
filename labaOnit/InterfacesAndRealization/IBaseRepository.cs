namespace labaOnit.InterfacesAndRealization
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task CreateAsync(T entity);

        Task Delete(T entity);

        Task Update(T entity);
    }
}
