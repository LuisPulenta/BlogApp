using ClienteBlazorWASM.Servicios.IServicio;
using Microsoft.AspNetCore.Components;

namespace ClienteBlazorWASM.Pages.Autenticacion
{
    public partial class Salir
    {
        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; } = null!;
        [Inject]
        public NavigationManager navigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await servicioAutenticacion.Salir();
            navigationManager.NavigateTo("/");
        }
    }
}
