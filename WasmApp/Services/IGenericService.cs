public interface IGenericService <T> where T : class{

        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> SaveAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool>UpdateAsync(int id,T entity);
        Task<T> GetByIdAsync(int id);
}
