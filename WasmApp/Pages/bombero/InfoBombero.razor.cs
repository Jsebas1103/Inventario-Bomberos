
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Entities;
using Services;

namespace WasmApp.Pages;

public class InfoBomberoBase : ComponentBase
{
    #region Inject y Variables
    [Inject] IBomberoService BomberoService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    [Parameter]
    public int Id { get; set; }

    protected bool showConfirmation;
    protected int bomberoId;
    protected string bomberoName;
    protected bool isEditMode = false;

    public Bombero bombero = new Bombero();
    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            bombero = await BomberoService.GetByIdAsync(Id);
            bomberoId = bombero.id;
            bomberoName = bombero.nombre;
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Error: {ex.Message}");
        }
    }

    protected async Task HandleOnValidSubmit()
    {
        try
        {
            await BomberoService.UpdateAsync(bomberoId, bombero);
            ToastService.ShowSuccess("Se han actualizado los datos correctamente");
            isEditMode = false; // Volver al modo de visualización después de guardar
        }
        catch (System.Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al actualizar los datos: {ex.Message}");
        }
    }

    protected void ToggleEditMode()
    {
        isEditMode = !isEditMode;
    }

    protected async Task ShowConfirmationModal()
    {
        showConfirmation = true;
    }

    protected async Task OnConfirmDelete(bool confirmed)
    {
        try
        {
            if (confirmed)
            {
                await BomberoService.DeleteAsync(bomberoId);
                ToastService.ShowSuccess($"Se han eliminado los datos de {bomberoName} correctamente");
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al tratar de eliminar: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/bomberos");
            showConfirmation = false;
        }
    }
}
