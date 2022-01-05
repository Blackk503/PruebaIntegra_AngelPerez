using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngelPerezIntegra.Models;
using AngelPerezIntegra.DTO;
using AngelPerezIntegra.Interfaces;
using System.Collections.Generic;

namespace AngelPerezIntegra.Controllers
{
    public class EmpleadoController : Controller
    {
        readonly IEmpleadoService _emp;

        public EmpleadoController(IEmpleadoService emp)
        {
            _emp = emp;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            List<DTOEmpleado> model = new List<DTOEmpleado>();
            var empleado = await _emp.LstEmpleado();
            foreach (var item in empleado)
            {
                model.Add(new DTOEmpleado()
                {
                    IdEmpleado = item.id,
                    Apellido = item.apellido,
                    Nombre = item.nombre,
                    Telefono = item.telefono,
                    Email = item.email,
                    RutaFoto = item.foto,
                    FechaFormat = item.fecha_contratacion.ToString("MM/dd/yyyy")
                });
            }
            return View(model);
        }
        public async Task<IActionResult> LstEmpleadoPartialAsync(string valor)
        {
            List<DTOEmpleado> model = new List<DTOEmpleado>();
            List<empleado> empleado = await _emp.LstEmpleado_Email_Apellido(valor);
            foreach (var item in empleado)
            {
                model.Add(new DTOEmpleado()
                {
                    IdEmpleado = item.id,
                    Apellido = item.apellido,
                    Nombre = item.nombre,
                    Telefono = item.telefono,
                    Email = item.email,
                    RutaFoto = item.foto,
                    FechaFormat = item.fecha_contratacion.ToString("MM/dd/yyyy")
                });
            }
            return PartialView("LstEmpleadoPartial", model);
        }
        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            empleado registro = await _emp.RegEmpleadoAsync(id);
            DTOEmpleado model = new DTOEmpleado()
            {
                IdEmpleado = registro.id,
                Nombre = registro.nombre,
                Apellido = registro.apellido,
                Telefono = registro.telefono,
                Email = registro.email,
                RutaFoto = registro.foto,
                FechaFormat = registro.fecha_contratacion.ToString("MM/dd/yyyy")
            };
            return View(model);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Obsolete]
        public async Task<IActionResult> Create([Bind("IdEmpleado,Apellido,Nombre,Telefono,Email,Foto,FechaContratacion")] DTOEmpleado empleado)
        {
            if (ModelState.IsValid)
            {
                if (_emp.EmpleadoExiste(empleado) == null)
                {
                    string result = await _emp.GuardarEmpleadoAsync(empleado);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ModelState.AddModelError("CustomError", result);
                        return View(empleado);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("CustomError", "El empleado ya existe");
                }
            }
            return View(empleado);
        }
        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            empleado registro = await _emp.RegEmpleadoAsync(id);
            DTOEmpleado model = new DTOEmpleado()
            {
                IdEmpleado = registro.id,
                Nombre = registro.nombre,
                Apellido = registro.apellido,
                Telefono = registro.telefono,
                Email = registro.email,
                RutaFoto = registro.foto,
                FechaContratacion = registro.fecha_contratacion
            };
            return View(model);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,Apellido,Nombre,Telefono,Email,Foto,RutaFoto,FechaContratacion")] DTOEmpleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }
            if (empleado.Foto == null && !string.IsNullOrEmpty(empleado.RutaFoto) && ModelState.ErrorCount == 1)
            {
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_emp.EmpleadoExisteActualizar(empleado) == null)
                    {
                        string result = await _emp.ActualizarEmpleadoAsync(empleado);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ModelState.AddModelError("CustomError", result);
                            return View(empleado);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "El empleado ya existe");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _emp.EmpleadoExistsAsync(empleado.IdEmpleado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }                
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empleado = await _emp.RegEmpleadoAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _emp.RegEmpleadoAsync(id);
            string result = await _emp.EliminarEmpleadoAsync(empleado);
            if (!string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("CustomError", result);
                return View(empleado);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
