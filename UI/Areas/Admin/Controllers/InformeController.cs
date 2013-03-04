// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeController.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Acheve.Data.Services.Contracts;
    using Acheve.Infrastructure.Services.Contracts;
    using Acheve.UI.Areas.Admin.ViewModels;

    public class InformeController : Controller
    {
        private readonly IJornadaQueries jornadaQueries;

        private readonly IUsuariosQueries usuariosQueries;

        private readonly ISettingsProvider settingsProvider;

        public InformeController(IJornadaQueries jornadaQueries, IUsuariosQueries usuariosQueries, ISettingsProvider settingsProvider)
        {
            if (jornadaQueries == null)
            {
                throw new ArgumentNullException("jornadaQueries");
            }

            if (usuariosQueries == null)
            {
                throw new ArgumentNullException("usuariosQueries");
            }

            if (settingsProvider == null)
            {
                throw new ArgumentNullException("settingsProvider");
            }

            this.jornadaQueries = jornadaQueries;
            this.usuariosQueries = usuariosQueries;
            this.settingsProvider = settingsProvider;
        }

        public ViewResult Dia()
        {
            var vm = new InformeJornadaViewModel
            {
                Usuarios = this.usuariosQueries.ObtenerUsuarios(),
                Fecha = DateTime.Today,
                IsPost = false
            };

            return this.View(vm);
        }

        [HttpPost]
        public ViewResult Dia(CriteriosBusquedaFechaViewModel searchCriteria)
        {
            var vm = new InformeJornadaViewModel
            {
                Usuarios = this.usuariosQueries.ObtenerUsuarios(),
                Fecha = searchCriteria.Fecha,
                Usuario = searchCriteria.Usuario,
            };

            if (this.ModelState.IsValid)
            {
                var jornada = this.jornadaQueries.ObtenerJornada(searchCriteria.Usuario, searchCriteria.Fecha);
                var sumaPausas = jornada.Pausas.Aggregate(TimeSpan.Zero, (span, pausa) => span.Add(pausa.Duracion));

                vm.IsPost = true;
                vm.Jornada = jornada;
                vm.Total = jornada.Duracion;
                vm.SumaPausa = sumaPausas;
                vm.SumaTrabajo = jornada.Duracion.Subtract(sumaPausas);
            }

            return this.View(vm);
        }

        public ActionResult EntreFechas()
        {
            var vm = new InformeResumenEntreFechasViewModel
            {
                Usuarios = this.usuariosQueries.ObtenerUsuarios(),
                Inicio = DateTime.Today.AddDays(-7),
                Fin = DateTime.Today,
                IsPost = false
            };

            return this.View(vm);
        }

        [HttpPost]
        public ActionResult EntreFechas(CriteriosBusquedaEntreFechasViewModel searchCriteria)
        {
            var vm = new InformeResumenEntreFechasViewModel
            {
                Usuarios = this.usuariosQueries.ObtenerUsuarios(),
                Inicio = searchCriteria.Inicio,
                Fin = searchCriteria.Fin,
                Usuario = searchCriteria.Usuario
            };

            if (this.ModelState.IsValid)
            {
                var resumenJornadas = this.jornadaQueries.ObtenerResumenEntreFechas(
                    searchCriteria.Usuario,
                    searchCriteria.Inicio,
                    searchCriteria.Fin).ToArray();

                vm.ResumenJornadas = resumenJornadas;
                vm.IsPost = true;
            }

            return this.View(vm);
        }
    }
}
