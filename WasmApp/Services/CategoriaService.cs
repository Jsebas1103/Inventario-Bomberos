using Entities;
using Services;
using System.Data;
using System.Net.Http.Json;

public class CategoriaService : ICategoriaService
{

    private readonly HttpClient httpClient;

    public CategoriaService(HttpClient _httpClient)
    {
        httpClient = _httpClient;
    }

    

    public async Task <bool> DeleteAsync(int id)
    {
        await httpClient.DeleteAsync($"api/Categoria/{id}");
        return true;
           
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        
        var result = await httpClient.GetFromJsonAsync<List<Categoria>>($"api/Categoria");
        
        return result;
    }

    public async Task<Categoria> GetByIdAsync(int id)
    {
        
        var result = await httpClient.GetFromJsonAsync<Categoria>($"api/Categoria/{id}");
        
        return result;
        
    }


    public async Task<bool> SaveAsync(Categoria entity)
    {
        
        var response = await httpClient.PostAsJsonAsync<Categoria>($"api/Categoria",entity);
        return true;
    }


    public async Task<bool> UpdateAsync(int id,Categoria entity)
    {
        await httpClient.PutAsJsonAsync<Categoria>($"api/Categoria/{id}", entity);
        return true;
    }

    
}