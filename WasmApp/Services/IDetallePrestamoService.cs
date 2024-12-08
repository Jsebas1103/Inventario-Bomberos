using Entities;

namespace Services;

public interface IDetallePrestamoService:IGenericService<DetallePrestamo>{
    Task<bool> DeleteAsync(DetallePrestamo detallePrestamo);
    Task<DetallePrestamo> GetByIdAsync(DetallePrestamo detallePrestamo);
    Task<IEnumerable<DetallePrestamo>> GetByIdAsync2(int id);
}
