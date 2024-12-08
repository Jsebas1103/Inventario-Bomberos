using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Services;
using Entities;

namespace WasmApp.Pages;

public class NuevoBomberoBase : ComponentBase
{
    #region Inject y Variables
    [Inject] IBomberoService BomberoService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public Bombero bombero = new Bombero();
    #endregion

    protected async Task HandleOnValidSubmit()
    {
        try
        {
            var newBombero = await BomberoService.SaveAsync(bombero);
            ToastService.ShowSuccess("Se ha creado el bombero correctamente");
        }
        catch (System.Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al crear el bombero: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/bomberos");
        }
    }
}
