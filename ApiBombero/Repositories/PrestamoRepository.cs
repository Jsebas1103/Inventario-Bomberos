using System.Data;
using Dapper;
using Entities;
using Microsoft.Data.Sqlite;
using repositories;

public class PrestamoRepository : IPrestamoRepository
{
    private readonly string connectionString;
    

    public PrestamoRepository(IConfiguration configuration)
    {
        this.connectionString=configuration.GetConnectionString("DefaultConnection");
        
    }
    private IDbConnection connection => new SqliteConnection(connectionString);

    public async Task<bool> DeleteAsync(int id)
    {
            var sql = "DELETE FROM prestamo WHERE id = @id";     
            var affectedRows =  await connection.ExecuteAsync(sql,new{id=id});

            if(affectedRows>0)return true;
            return false;

    }

    public async Task<IEnumerable<Prestamo>> GetAllAsync()
    {
        string sql = @"select * from prestamo p join bombero b on b.id=p.idBombero;";

            var prestamos = await connection.QueryAsync<Prestamo,Bombero,Prestamo>(sql, (prestamo,bombero)=>{
                prestamo.bombero=bombero;
                return prestamo;
            },
            splitOn: "id");
            
            return prestamos;
    }

    public async Task<Prestamo> GetByIdAsync(int id)
    {
        string sql = @"select * from prestamo p join bombero b on b.id=p.idBombero where p.id=@id;";

            var prestamo = await connection.QueryAsync<Prestamo,Bombero,Prestamo>(sql, (prestamo,bombero)=>{
                prestamo.bombero=bombero;
                return prestamo;
            },new {id=id},
            splitOn: "id");
            
            return prestamo.FirstOrDefault();
    }

    public async Task<bool> SaveAsync(Prestamo entity)
    {

        string sql = "insert into prestamo (id, idbombero,estado,fechaprestamo,fechadevolucion,observaciones)"+
            "values (@id,@idbombero,@estado,@fechaprestamo,@fechadevolucion,@observaciones);";

            var count = await connection.ExecuteAsync(sql, entity);
            if(count !=1) return false;
            return true;
    }

    public async Task<bool> UpdateAsync(Prestamo entity)
    {
        var sql = "UPDATE prestamo SET id=@id, idbombero=@idbombero,estado=@estado,fechaprestamo=@fechaprestamo,fechadevolucion=@fechaprestamo,observaciones=@observaciones WHERE id = @id";
            
            var affectedRows = await connection.ExecuteAsync(sql,entity);
    
            Console.WriteLine($"Affected Rows: {affectedRows}");
            if(affectedRows>0) return true;
            return false;
    }
}