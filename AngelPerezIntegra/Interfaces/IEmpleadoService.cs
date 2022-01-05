using AngelPerezIntegra.Data;
using AngelPerezIntegra.DTO;
using AngelPerezIntegra.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelPerezIntegra.Interfaces
{
    /// <summary>Interface <c>IEmpleadoService</c> 
    /// Consultas de datos de empleado. 
    /// Funciones para guardar, actualizar y eliminar.
    /// .</summary>
    public interface IEmpleadoService
    {
        /// <summary>Interface <c>LstEmpleado</c> 
        /// Listado general de empleados
        /// .</summary>
        Task<List<empleado>> LstEmpleado();
        Task<List<empleado>> LstEmpleado_Email_Apellido(string campo);
        empleado EmpleadoExiste(DTOEmpleado emp);
        /// <summary>Interface <c>EmpleadoExistsAsync</c> 
        /// Solamente valida que exista el empleado
        /// .</summary>
        Task<bool> EmpleadoExistsAsync(int id);
        /// <summary>Interface <c>RegEmpleadoAsync</c> 
        /// Busca un empleado por código y obtiene sus datos
        /// .</summary>
        Task<empleado> RegEmpleadoAsync(int? id);
        empleado EmpleadoExisteActualizar(DTOEmpleado emp);
        /// <summary>Función <c>GuardarEmpleadoAsync</c> 
        /// Registra los datos de un empleado nuevo.
        /// .</summary>
        Task<string> GuardarEmpleadoAsync(DTOEmpleado empleado);
        /// <summary>Función <c>ActualizarEmpleadoAsync</c> 
        /// Actualiza datos del empleado.
        /// Elimina foto que será sustituida.
        /// .</summary>
        Task<string> ActualizarEmpleadoAsync(DTOEmpleado empleado);
        Task<bool> UploadedFile(IFormFile archivo);
        /// <summary>Función <c>EliminarEmpleadoAsync</c> 
        /// Elimina el registro del empleado y su foto.
        /// .</summary>
        Task<string> EliminarEmpleadoAsync(empleado model);
        Task<bool> ValidarFotoEmpleado(string Foto);
        /// <summary>Función <c>ExisteArchivo</c>
        /// Verifica la existencia de un archivo
        /// .</summary>
        Task<bool> ExisteArchivo(string archivo);
        /// <summary>Función <c>EliminarFotoAsync</c> 
        /// Elimina una foto con ingresar su nombre y extensión
        /// .</summary>
        Task<bool> EliminarFotoAsync(string NombreFoto);
    }
}
