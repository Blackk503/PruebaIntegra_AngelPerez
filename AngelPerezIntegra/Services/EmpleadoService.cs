using AngelPerezIntegra.DTO;
using Microsoft.AspNetCore.Hosting;
using System;
using AngelPerezIntegra.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngelPerezIntegra.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using AngelPerezIntegra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AngelPerezIntegra.Services
{
    
    public class EmpleadoService: IEmpleadoService
    {
        private readonly TestintegraContext _context;
        private readonly IWebHostEnvironment _web;
        private string PathFile = string.Empty;
        private readonly string[] ExtensionsFoto = new string[] {".jpg", ".png", ".jpeg" };
        private string mensaje = string.Empty;

        public EmpleadoService(TestintegraContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _web = webHostEnvironment;
        }
        #region Consultas
        public async Task<List<empleado>> LstEmpleado()
        {
            return await _context.empleado.ToListAsync();
        }
        public async Task<List<empleado>> LstEmpleado_Email_Apellido(string campo)
        {
            return await _context.empleado
                .Where(x => x.apellido.Contains(campo) || x.email.Contains(campo)).ToListAsync();
        }
        public empleado EmpleadoExiste(DTOEmpleado emp)
        {
            return _context.empleado
                .Where(x =>
                (x.nombre == emp.Nombre && x.apellido == emp.Apellido) ||
                x.telefono == emp.Telefono || x.email == emp.Email).FirstOrDefault();
        }
        public async Task<bool> EmpleadoExistsAsync(int id)
        {
            return await Task.FromResult(_context.empleado.Any(e => e.id == id));
        }
        public empleado EmpleadoExisteActualizar(DTOEmpleado emp)
        {
            return _context.empleado
                .Where(x =>
                ((x.nombre == emp.Nombre && x.apellido == emp.Apellido) ||
                x.telefono == emp.Telefono || x.email == emp.Email) && x.id != emp.IdEmpleado).FirstOrDefault();
        }
        public async Task<empleado> RegEmpleadoAsync(int? id)
        {
            return await _context.empleado.FindAsync(id);
        }
        #endregion

        #region Funciones
        public async Task<string> GuardarEmpleadoAsync(DTOEmpleado empleado)
        {
            PathFile = Path.Combine(_web.WebRootPath, "img\\Empleado\\" + empleado.Foto.FileName);
            mensaje = await ComprobarArchivo(empleado.Foto);
            if (!string.IsNullOrEmpty(mensaje))
            {
                return await Task.FromResult(mensaje);
            }
            try
            {
                empleado emp = new empleado
                {
                    apellido = empleado.Apellido,
                    nombre = empleado.Nombre,
                    telefono = empleado.Telefono,
                    email = empleado.Email,
                    foto = empleado.Foto.FileName,
                    fecha_contratacion = empleado.FechaContratacion
                };
                _context.Add(emp);
                await _context.SaveChangesAsync();
                mensaje = string.Empty;
            }
            catch (Exception)
            {
                mensaje = "Algo salio mal";
            }
            return await Task.FromResult(mensaje);
        }
        public async Task<string> ActualizarEmpleadoAsync(DTOEmpleado model)
        {
            if (model.Foto != null)
            {
                await EliminarFotoAsync(model.RutaFoto);
                PathFile = Path.Combine(_web.WebRootPath, "img\\Empleado\\" + model.Foto.FileName);
                mensaje = await ComprobarArchivo(model.Foto);
                model.RutaFoto = model.Foto.FileName;
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                return await Task.FromResult(mensaje);
            }
            try
            {
                empleado emp = new empleado
                {
                    id = model.IdEmpleado,
                    apellido = model.Apellido,
                    nombre = model.Nombre,
                    telefono = model.Telefono,
                    email = model.Email,
                    foto = model.RutaFoto,
                    fecha_contratacion = model.FechaContratacion
                };
                _context.Update(emp);
                await _context.SaveChangesAsync();
                mensaje = string.Empty;
            }
            catch (Exception)
            {
                mensaje = "Algo salio mal";
            }
            return await Task.FromResult(mensaje);
        }
        public async Task<string> EliminarEmpleadoAsync(empleado model)
        {
            try
            {
                _context.empleado.Remove(model);
                await _context.SaveChangesAsync();
                await EliminarFotoAsync(model.foto);
                mensaje = string.Empty;
            }
            catch (Exception)
            {
                mensaje = "Algo salio mal intentalo mas tarde";
            }
            return await Task.FromResult(mensaje);
        }
        #endregion

        #region Archivo
        public Task<bool> UploadedFile(IFormFile archivo)
        {
            bool subir = false;

            if (archivo != null)
            {
                using FileStream fileStream = File.Create(PathFile);
                archivo.CopyTo(fileStream);
                fileStream.Flush();
                subir = true;
            }
            return Task.FromResult(subir);
        }
        public Task<bool> ValidarFotoEmpleado(string Foto)
        {
            bool valida = false;
            if (!ExtensionsFoto.Contains(Path.GetExtension(Foto)))
                valida = true;
            return Task.FromResult(valida);
        }
        public Task<bool> ExisteArchivo(string archivo)
        {
            bool existe = false;
            if (!string.IsNullOrEmpty(archivo) && File.Exists(archivo))
                existe = true;
            return Task.FromResult(existe);
        }
        public async Task<string> ComprobarArchivo(IFormFile foto)
        {
            if (await ValidarFotoEmpleado(PathFile))
            {
                mensaje = "El formato de la imagen debe ser .jpg | .jpeg | .png";
            }
            if (await ExisteArchivo(PathFile))
            {
                mensaje = "Ya existe una foto con este nombre";
            }
            if (!await UploadedFile(foto))
            {
                mensaje = "La imagen no fue cargada o no existe la ruta";
            }
            return await Task.FromResult(mensaje);
        }

        public async Task<bool> EliminarFotoAsync(string NombreFoto)
        {
            bool result = true;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\Empleado", NombreFoto);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return await Task.FromResult(result);
        }
        #endregion
    }
}
