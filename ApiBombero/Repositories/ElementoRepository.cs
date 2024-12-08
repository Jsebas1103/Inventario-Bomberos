using System.Data;
using Dapper;
using Entities;
using Microsoft.Data.Sqlite;
using repositories;

public class ElementoRepository : IElementoRepository
{
    private readonly string connectionString;
    

    public ElementoRepository(IConfiguration configuration)
    {
        this.connectionString=configuration.GetConnectionString("DefaultConnection");
        
    }
    private IDbConnection connection => new SqliteConnection(connectionString);

    public async Task<bool> DeleteAsync(int id)
    {
            var sql = "DELETE FROM elemento WHERE id = @id";     
            var affectedRows =  await connection.ExecuteAsync(sql,new{id=id});

            if(affectedRows>0)return true;
            return false;

    }


    public async Task<IEnumerable<Elemento>> GetAllAsync()
    {
        string sql = @"select * from elemento e join categoria c on e.idCategoria=c.id;";

            var elementos = await connection.QueryAsync<Elemento,Categoria,Elemento>(sql, (elemento,categoria)=>{
                elemento.categoria=categoria;
                return elemento;
            },
            splitOn: "id");
            
            return elementos;
    }

    public async Task<Elemento> GetByIdAsync(int id)
    {
        string sql = @"select * from elemento e join categoria c on e.idCategoria=c.id where e.id=@id;";

            var elementos = await connection.QueryAsync<Elemento,Categoria,Elemento>(sql, (elemento,categoria)=>{
                elemento.categoria=categoria;
                return elemento;
            },new {id=id},
            splitOn: "id");
            
            return elementos.FirstOrDefault();
    }

    public async Task<bool> SaveAsync(Elemento entity)
    {

        string sql = "insert into elemento (id, idCategoria,nombre,descripcion,estado,vidaUtil)"+
            "values (@id,@idCategoria,@nombre,@descripcion,@estado,@vidaUtil);";

            var count = await connection.ExecuteAsync(sql, entity);
            if(count !=1) return false;
            return true;
    }

    public async Task<bool> UpdateAsync(Elemento entity)
    {
        var sql = "UPDATE elemento SET id=@id, idCategoria=@idCategoria,nombre=@nombre,descripcion=@descripcion,estado=@estado,vidaUtil=@vidaUtil WHERE id = @id";
            
            var affectedRows = await connection.ExecuteAsync(sql,entity);
    
            Console.WriteLine($"Affected Rows: {affectedRows}");
            if(affectedRows>0) return true;
            return false;
    }


}