
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Entities;
using repositories;
public class DetallePrestamoRepository : IDetallePrestamoRepository
{
    private readonly string connectionString;
    
    
    private readonly IElementoRepository elementoRepository;
    private readonly IPrestamoRepository prestamoRepository;
    public DetallePrestamoRepository(IConfiguration configuration, IElementoRepository elementoRepository,IPrestamoRepository prestamoRepository)
    {
        this.connectionString=configuration.GetConnectionString("DefaultConnection");
        this.elementoRepository = elementoRepository;
        this.prestamoRepository = prestamoRepository;
    }
    private IDbConnection connection => new SqliteConnection(connectionString);
    
    
    public async Task<IEnumerable<DetallePrestamo>> GetAllAsync()
    {
        var detallePrestamos = await connection.QueryAsync<DetallePrestamo>("select * from detallePrestamo");
        
        
        foreach( var detallePrestamo in detallePrestamos){
            
            detallePrestamo.elemento= elementoRepository.GetByIdAsync(detallePrestamo.idElemento).Result;
            detallePrestamo.prestamo= prestamoRepository.GetByIdAsync(detallePrestamo.idPrestamo).Result;
            
        }
        
        return detallePrestamos;
    }

    public async Task<bool> SaveAsync(DetallePrestamo entity)
    {
        string sql = "insert into detallePrestamo (idPrestamo, idElemento)"+
            "values (@idPrestamo,@idElemento);";

            var count = await connection.ExecuteAsync(sql, entity);
            if(count !=1) return false;
            return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
            var sql = "DELETE FROM detallePrestamo WHERE idPrestamo = @id";     
            var affectedRows =  await connection.ExecuteAsync(sql,new{id=id});

            if(affectedRows>0)return true;
            return false;
    }

    public async Task<bool> UpdateAsync(DetallePrestamo entity)
    {
        var sql = "UPDATE detallePrestamo SET idPrestamo=@idPrestamo, idElemento=@idElemento WHERE idPrestamo = @idPrestamo and idElemento = @idElemento";
            
            var affectedRows = await connection.ExecuteAsync(sql,entity);
    
            Console.WriteLine($"Affected Rows: {affectedRows}");
            if(affectedRows>0) return true;
            return false;
    }



    public async Task<IEnumerable<DetallePrestamo>> GetByIdAsync2(int id)
    {
        string sql = "SELECT * FROM detallePrestamo WHERE idPrestamo = @id";
        
            var esncontrados = await connection.QueryAsync<DetallePrestamo>(sql, new { id = id });

            foreach( var detallePrestamo in esncontrados){
            
            detallePrestamo.elemento= elementoRepository.GetByIdAsync(detallePrestamo.idElemento).Result;
            detallePrestamo.prestamo= prestamoRepository.GetByIdAsync(detallePrestamo.idPrestamo).Result;
            
        }
            return esncontrados;
    }

public async Task<DetallePrestamo> GetByIdAsync(int id)
    {
        string sql = "SELECT * FROM detallePrestamo WHERE idPrestamo = @id";
        
            var esncontrados = await connection.QueryAsync<DetallePrestamo>(sql, new { id = id });

            foreach( var detallePrestamo in esncontrados){
            
            detallePrestamo.elemento= elementoRepository.GetByIdAsync(detallePrestamo.idElemento).Result;
            detallePrestamo.prestamo= prestamoRepository.GetByIdAsync(detallePrestamo.idPrestamo).Result;
            
        }
            return esncontrados.FirstOrDefault();
    }

    public async Task<bool> DeleteAsync(DetallePrestamo detallePrestamo)
    {
        var sql = "DELETE FROM detallePrestamo WHERE idPrestamo = @idPrestamo and idElemento = @idElemento";     
            var affectedRows =  await connection.ExecuteAsync(sql,detallePrestamo);

            if(affectedRows>0)return true;
            return false;
    }

    public async Task<DetallePrestamo> GetByIdAsync(DetallePrestamo detallePrestamo)
    {
        string sql = "SELECT * FROM detallePrestamo WHERE idPrestamo = @idPrestamo and idElemento = @idElemento";
        
            var esncontrados = await connection.QueryAsync<DetallePrestamo>(sql, detallePrestamo);

            var unico = esncontrados.FirstOrDefault();
            unico.elemento= elementoRepository.GetByIdAsync(detallePrestamo.idElemento).Result;
            unico.prestamo= prestamoRepository.GetByIdAsync(detallePrestamo.idPrestamo).Result;

            
            return esncontrados.FirstOrDefault();
    }
}