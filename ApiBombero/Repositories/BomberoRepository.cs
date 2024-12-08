
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Entities;
using repositories;
public class BomberoRepository : IBomberoRepository
{
    private readonly string connectionString;
    

    public BomberoRepository(IConfiguration configuration)
    {
        this.connectionString=configuration.GetConnectionString("DefaultConnection");
        
    }
    private IDbConnection connection => new SqliteConnection(connectionString);
    
    
    public async Task<IEnumerable<Bombero>> GetAllAsync()
    {
        var bomberos = await connection.QueryAsync<Bombero>("select * from bombero");
        return bomberos;
    }

    public async Task<bool> SaveAsync(Bombero entity)
    {
        string sql = "insert into bombero (id, nombre,edad,direccion,telefono,correo,contraseña,acceso)"+
            "values (@id,@nombre,@edad,@direccion,@telefono,@correo,@contraseña,@acceso);";

            var count = await connection.ExecuteAsync(sql, entity);
            if(count !=1) return false;
            return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
            var sql = "DELETE FROM Bombero WHERE id = @id";     
            var affectedRows =  await connection.ExecuteAsync(sql,new{id=id});

            if(affectedRows>0)return true;
            return false;
    }

    public async Task<bool> UpdateAsync(Bombero entity)
    {
        var sql = "UPDATE bombero SET id = @id, nombre=@nombre ,edad=@edad,direccion=@direccion,telefono=@telefono,correo=@correo,contraseña=@contraseña,acceso=@acceso WHERE id = @id;";
            
            var affectedRows = await connection.ExecuteAsync(sql,entity);
    
            Console.WriteLine($"Affected Rows: {affectedRows}");
            if(affectedRows>0) return true;
            return false;
    }

    public async Task<Bombero> GetByIdAsync(int id)
    {
        string sql = "SELECT * FROM bombero WHERE id = @id";
            return await connection.QueryFirstOrDefaultAsync<Bombero>(sql, new { id = id });
    }
}