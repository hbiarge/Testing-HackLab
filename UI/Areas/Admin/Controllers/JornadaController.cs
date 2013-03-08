// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaController.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;

    using Acheve.Data.Services.Contracts;
    using Acheve.UI.Areas.Admin.ViewModels;
    using Acheve.UI.Infrastructure;
    using Acheve.UI.ViewModels;

    using CriteriosBusquedaFechaViewModel = Acheve.UI.Areas.Admin.ViewModels.CriteriosBusquedaFechaViewModel;
    using JornadaViewModel = Acheve.UI.Areas.Admin.ViewModels.JornadaViewModel;

    public class JornadaController : Controller
    {
        private readonly IJornadaQueries jornadaQueries;

        private readonly IJornadaCommands jornadaCommands;

        public JornadaController(IJornadaQueries jornadaQueries, IJornadaCommands jornadaCommands)
        {
            if (jornadaQueries == null)
            {
                throw new ArgumentNullException("jornadaQueries");
            }

            if (jornadaCommands == null)
            {
                throw new ArgumentNullException("jornadaCommands");
            }

            this.jornadaQueries = jornadaQueries;
            this.jornadaCommands = jornadaCommands;
        }

        public ActionResult Editar(CriteriosBusquedaFechaViewModel searchCriteria)
        {
            var jornada = this.jornadaQueries.ObtenerJornada(searchCriteria.Usuario, searchCriteria.Fecha);
            var vm = JornadaViewModel.FormJornada(jornada, searchCriteria.Usuario);

            return this.View(vm);
        }

        [HttpPost]
        public ActionResult Editar(JornadaEditViewModel jornadaViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { hasError = true, errors = this.ModelState.GetValidationErrors() });
            }

            try
            {
                var jornada = jornadaViewModel.ToJornada();
                this.jornadaCommands.ActualizarJornada(jornada, jornadaViewModel.Usuario);

                return this.Json(new { hasError = false, location = this.Url.Action("Index", "Buscar", new { usuario = jornadaViewModel.Usuario, fecha = jornadaViewModel.Dia.ToShortDateString() }) });
            }
            catch (Exception ex)
            {
                return this.Json(new { hasError = true, errors = new[] { new Error { ErrorMessage = ex.Message } } });
            }
        }

        public ActionResult Crear(CriteriosBusquedaFechaViewModel searchCriteria)
        {
            var vm = new JornadaViewModel
            {
                Usuario = searchCriteria.Usuario,
                Dia = searchCriteria.Fecha.ToShortDateString(),
                Inicio = searchCriteria.Fecha.ToShortDateString(),
                Fin = searchCriteria.Fecha.ToShortDateString()
            };

            return this.View(vm);
        }

        [HttpPost]
        public JsonResult Crear(JornadaEditViewModel jornadaViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { hasError = true, errors = this.ModelState.GetValidationErrors() });
            }

            try
            {
                var jornada = jornadaViewModel.ToJornada();
                this.jornadaCommands.CrearJornada(jornada, jornadaViewModel.Usuario);
                return this.Json(new { hasError = false, location = this.Url.Action("Index", "Buscar", new { usuario = jornadaViewModel.Usuario, fecha = jornadaViewModel.Dia.ToShortDateString() }) });
            }
            catch (Exception ex)
            {
                return this.Json(new { hasError = true, errors = new[] { new Error { ErrorMessage = ex.Message } } });
            }
        }

        [HttpPost]
        public JsonResult Eliminar(CriteriosBusquedaFechaViewModel searchCriteria)
        {
            var jornada = this.jornadaQueries.ObtenerJornada(searchCriteria.Usuario, searchCriteria.Fecha);

            if (jornada != null)
            {
                try
                {
                    this.jornadaCommands.EliminarJornada(jornada.Id);
                    return this.Json(new { hasError = false });
                }
                catch (Exception ex)
                {
                    return this.Json(new { hasError = true, errors = ex.Message });
                }
            }

            return this.Json(new { hasError = false });
        }
    }
}
