﻿using Blazored.LocalStorage;
using ClienteBlazorWASM.Helpers;
using ClienteBlazorWASM.Modelos;
using ClienteBlazorWASM.Servicios.IServicio;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;


namespace ClienteBlazorWASM.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly HttpClient _cliente;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _estadoProveedorAutenticacion;

        public ServicioAutenticacion(HttpClient cliente, 
            ILocalStorageService localStorage,
            AuthenticationStateProvider estadoProveedorAutenticacion)
        {
            _cliente = cliente;
            _localStorage = localStorage;
            _estadoProveedorAutenticacion = estadoProveedorAutenticacion;
        }

        public async Task<RespuestaAutenticacion> Acceder(UsuarioAutenticacion usuarioDesdeAutenticacion)
        {
            var content = JsonConvert.SerializeObject(usuarioDesdeAutenticacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = (JObject)JsonConvert.DeserializeObject(contentTemp)!;

            if (response.IsSuccessStatusCode)
            {
                var Token = resultado["result"]!["token"]!.Value<string>();
                var Usuario = resultado["result"]!["usuario"]!["nombreUsuario"]!.Value<string>();

                await _localStorage.SetItemAsync(Inicializar.Token_Local, Token);
                await _localStorage.SetItemAsync(Inicializar.Datos_Usuario_Local, Usuario);
                ((AuthStateProvider)_estadoProveedorAutenticacion).NotificarUsuarioLogueado(Token!);
                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                return new RespuestaAutenticacion { IsSuccess = true };
            }
            else
            {
                return new RespuestaAutenticacion { IsSuccess = false };
            }
        }

        public async Task<RespuestaRegistro> RegistrarUsuario(UsuarioRegistro usuarioParaRegistro)
        {
            var content = JsonConvert.SerializeObject(usuarioParaRegistro);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/registro", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<RespuestaRegistro>(contentTemp);

            if (response.IsSuccessStatusCode)
            {                
                return new RespuestaRegistro { registroCorrecto = true };
            }
            else
            {
                return resultado!;
            }
        }

        public async Task Salir()
        {
            await _localStorage.RemoveItemAsync(Inicializar.Token_Local);
            await _localStorage.RemoveItemAsync(Inicializar.Datos_Usuario_Local);
            ((AuthStateProvider)_estadoProveedorAutenticacion).NotificarUsuarioSalir();
            _cliente.DefaultRequestHeaders.Authorization = null;
        }   
    }
}
