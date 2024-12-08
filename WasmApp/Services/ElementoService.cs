using System.Data;
using System.Net.Http.Json;

using Entities;

using Services;

public class ElementoService : IElementoService
{
    private readonly HttpClient httpClient;

    public ElementoService(HttpClient _httpClient)
    {
        httpClient = _httpClient;
    }

    public async Task<bool> DeleteAsync(int id)
    {
            await httpClient.DeleteAsync($"api/Elemento/{id}");
        return true;

    }


    public async Task<IEnumerable<Elemento>> GetAllAsync()
    {
        var result = await httpClient.GetFromJsonAsync<List<Elemento>>($"api/Elemento");
        
        return result;
    }

    public async Task<Elemento> GetByIdAsync(int id)
    {
        var result = await httpClient.GetFromJsonAsync<Elemento>($"api/Elemento/{id}");
        
        return result;
    }

    public async Task<bool> SaveAsync(Elemento entity)
    {

        var response = await httpClient.PostAsJsonAsync<Elemento>($"api/Elemento",entity);
        return true;
    }

    public async Task<bool> UpdateAsync(int id,Elemento entity)
    {
        await httpClient.PutAsJsonAsync<Elemento>($"api/Elemento/{id}", entity);
        return true;
    }


}