using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Services;
using Entities;

namespace WasmApp.Pages;

public class NuevoPrestamoBase : ComponentBase
{
    #region Inject y Variables
    [Inject] IPrestamoService prestamoService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public Prestamo prestamo = new Prestamo();
    #endregion

    protected async Task HandleOnValidSubmit()
    {
        try
        {
            var newPrestamo = await prestamoService.SaveAsync(prestamo);
            ToastService.ShowSuccess("Se ha creado el prestamo correctamente");
        }
        catch (System.Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al crear el prestamo: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/prestamos");
        }
    }
}
