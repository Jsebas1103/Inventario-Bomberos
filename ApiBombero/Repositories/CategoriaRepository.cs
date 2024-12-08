using Entities;
using repositories;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

public class CategoriaRepository : ICategoriaRepository
{

    private readonly string connectionString;

    public CategoriaRepository(IConfiguration configuration)
    {
        connectionString=configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection connection=> new  SqliteConnection(connectionString);
    

    public async Task<bool> DeleteAsync(int id)
    {
        
            const string query = "DELETE FROM categoria WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

        if(affectedRows>0)return true;
            return false;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        
            var categorias = await connection.QueryAsync<Categoria>("SELECT * FROM categoria");
            
            return categorias;
    }

    public async Task<Categoria> GetByIdAsync(int id)
    {
        
            const string query="Select * from  categoria where id=@Id";
            return await connection.QueryFirstOrDefaultAsync<Categoria>(query,new {Id=id});
        
    }


    public async Task<bool> SaveAsync(Categoria entity)
    {
        
            const string sql="Insert into categoria (id,tipo) values (@id,@tipo);";

            var count = await connection.ExecuteAsync(sql, entity);
            if(count !=1) return false;
            return true;
    }


    public async Task<bool> UpdateAsync(Categoria entity)
    {
        const string query = "UPDATE categoria SET id = @id, tipo=@tipo WHERE id = @id;";
        var affectedRows =await connection.ExecuteAsync(query, entity);

        Console.WriteLine($"Affected Rows: {affectedRows}");
            if(affectedRows>0) return true;
            return false;
    }
}