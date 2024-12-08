

using System.Data;
using System.Net.Http.Json;
using Entities;
using Services;
public class DetallePrestamoService : IDetallePrestamoService
{
    private readonly HttpClient httpClient;

    public DetallePrestamoService(HttpClient _httpClient)
    {
        this.httpClient=_httpClient;
        
    }
    
    
    public async Task<IEnumerable<DetallePrestamo>> GetAllAsync()
    {
        var response = await httpClient.GetFromJsonAsync<List<DetallePrestamo>>("api/DetallePrestamo");
        return response;
    }

    public async Task<bool> SaveAsync(DetallePrestamo entity)
    {
        var result = await httpClient.PostAsJsonAsync($"api/DetallePrestamo", entity);
        
        Console.WriteLine(result);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await httpClient.DeleteAsync($"api/DetallePrestamo/{id}");
        Console.WriteLine(result);

        return true;
    }

    public async Task<bool> UpdateAsync(int id,DetallePrestamo entity)
    {
        
        var result = await httpClient.PutAsJsonAsync<DetallePrestamo>($"api/DetallePrestamo/{id}", entity);
        
        Console.WriteLine(result);
        return true;

    }

    public async Task<DetallePrestamo> GetByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<DetallePrestamo>($"api/DetallePrestamo/{id}");
    }

    public Task<bool> DeleteAsync(DetallePrestamo detallePrestamo)
    {
        throw new NotImplementedException();
    }

    public Task<DetallePrestamo> GetByIdAsync(DetallePrestamo detallePrestamo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DetallePrestamo>> GetByIdAsync2(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<DetallePrestamo>> IGenericService<DetallePrestamo>.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}