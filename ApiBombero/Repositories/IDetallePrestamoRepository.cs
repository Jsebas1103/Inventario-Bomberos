using Entities;

namespace repositories;

public interface IDetallePrestamoRepository:IGenericRepository<DetallePrestamo>{
    Task<bool> DeleteAsync(DetallePrestamo detallePrestamo);
    Task<DetallePrestamo> GetByIdAsync(DetallePrestamo detallePrestamo);
    Task<IEnumerable<DetallePrestamo>> GetByIdAsync2(int id);
}
