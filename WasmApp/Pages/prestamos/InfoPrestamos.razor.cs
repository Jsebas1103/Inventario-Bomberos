
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Entities;
using Services;

namespace WasmApp.Pages;

public class InfoPrestamosBase : ComponentBase
{
    #region Inject y Variables
    [Inject] IPrestamoService prestamoService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    [Parameter]
    public int Id { get; set; }

    protected bool showConfirmation;
    protected int idPrestamo;
    
    protected bool isEditMode = false;

    public Prestamo prestamo = new Prestamo();
    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            prestamo = await prestamoService.GetByIdAsync(Id);
            idPrestamo = prestamo.id;
            
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
            await prestamoService.UpdateAsync(idPrestamo, prestamo);
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
                await prestamoService.DeleteAsync(idPrestamo);
                ToastService.ShowSuccess($"Se han eliminado los datos del prestamo con id {idPrestamo} correctamente");
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al tratar de eliminar: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/prestamos");
            showConfirmation = false;
        }
    }
}
