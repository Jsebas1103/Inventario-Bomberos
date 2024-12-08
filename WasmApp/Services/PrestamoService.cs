using System.Data;
using System.Net.Http.Json;
using Entities;
using Services;

public class PrestamoService : IPrestamoService
{
    private readonly HttpClient httpClient;

    public PrestamoService(HttpClient _httpClient)
    {
        httpClient = _httpClient;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await httpClient.DeleteAsync($"api/Prestamo/{id}");
        return true;

    }

    public async Task<IEnumerable<Prestamo>> GetAllAsync()
    {
        var result = await httpClient.GetFromJsonAsync<List<Prestamo>>($"api/Prestamo");
        
        return result;
    }

    public async Task<Prestamo> GetByIdAsync(int id)
    {
        

            var result = await httpClient.GetFromJsonAsync<Prestamo>($"api/Prestamo/{id}");
        
        return result;
    }

    public async Task<bool> SaveAsync(Prestamo entity)
    {

         var response = await httpClient.PostAsJsonAsync<Prestamo>($"api/Prestamo",entity);
        return true;
    }

    public async Task<bool> UpdateAsync(int id,Prestamo entity)
    {
        await httpClient.PutAsJsonAsync<Prestamo>($"api/Prestamo/{id}", entity);
        return true;
    }
}