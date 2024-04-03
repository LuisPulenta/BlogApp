﻿using ClienteBlazorWASM.Modelos;
using ClienteBlazorWASM.Servicios.IServicio;
using Microsoft.AspNetCore.Components;

namespace ClienteBlazorWASM.Pages.Autenticacion
{
    public partial class Registro
    {
        private UsuarioRegistro UsuarioParaRegistro = new UsuarioRegistro();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarErroresRegistro {  get; set; }

        public IEnumerable<string>? Errores { get; set; }

        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; } = null!;

        [Inject]
        public NavigationManager navigationManager { get; set; } = null!;

        private async Task RegistrarUsuario()
        {
            MostrarErroresRegistro = false;
            EstaProcesando = true;
            var result = await servicioAutenticacion.RegistrarUsuario(UsuarioParaRegistro);
            if (result.registroCorrecto)
            {
                EstaProcesando = false;
                navigationManager.NavigateTo("/acceder");
            }
            else
            {
                EstaProcesando = false;
                Errores = result.Errores;
                MostrarErroresRegistro = true;
            }
        }
    }
}
