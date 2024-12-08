
using System.Data;
using System.Net.Http.Json;
using Entities;
using Services;

public class BomberoService : IBomberoService
{
    private readonly string connectionString;
    

   private readonly HttpClient httpClient;

    public BomberoService(HttpClient _httpClient)
    {
        httpClient = _httpClient;
    }

    
    public async Task<IEnumerable<Bombero>> GetAllAsync()
    {
        var result = await httpClient.GetFromJsonAsync<List<Bombero>>($"api/bombero");
        
        return result;
    }

    public async Task<bool> SaveAsync(Bombero entity)
    {
        var response = await httpClient.PostAsJsonAsync<Bombero>($"api/bombero",entity);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await httpClient.DeleteAsync($"api/bombero/{id}");
        return true;
    }

    public async Task<bool> UpdateAsync(int id,Bombero entity)
    {
        await httpClient.PutAsJsonAsync<Bombero>($"api/bombero/{id}", entity);
        return true;
    }

    public async Task<Bombero> GetByIdAsync(int id)
    {
        var result = await httpClient.GetFromJsonAsync<Bombero>($"api/bombero/{id}");
        
        return result;
    }
}