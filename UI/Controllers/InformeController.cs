// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeController.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Acheve.Data.Services.Contracts;
    using Acheve.UI.ViewModels;

    public class InformeController : Controller
    {
        private readonly IJornadaQueries jornadaQueries;

        public InformeController(IJornadaQueries jornadaQueries)
        {
            if (jornadaQueries == null)
            {
                throw new ArgumentNullException("jornadaQueries");
            }

            this.jornadaQueries = jornadaQueries;
        }

        public ViewResult Dia()
        {
            var vm = new InformeJornadaViewModel
            {
                Fecha = DateTime.Today.AddDays(-1),
                IsPost = false
            };

            return this.View(vm);
        }

        [HttpPost]
        public ViewResult Dia(CriteriosBusquedaFechaViewModel searchCriteria)
        {
            var vm = new InformeJornadaViewModel
            {
                Fecha = searchCriteria.Fecha,
            };

            if (this.ModelState.IsValid)
            {
                var jornada = this.jornadaQueries.ObtenerJornada(this.User.Identity.Name, searchCriteria.Fecha);
                var sumaPausas = jornada.Pausas.Aggregate(TimeSpan.Zero, (span, pausa) => span.Add(pausa.Duracion));

                vm.IsPost = true;
                vm.Jornada = jornada;
                vm.Total = jornada.Duracion;
                vm.SumaPausa = sumaPausas;
                vm.SumaTrabajo = jornada.Duracion.Subtract(sumaPausas);
            }

            return this.View(vm);
        }

        public ViewResult EntreFechas()
        {
            var vm = new InformeResumenEntreFechasViewModel
            {
                Inicio = DateTime.Today.AddDays(-7),
                Fin = DateTime.Today.AddDays(-1),
                IsPost = false
            };

            return this.View(vm);
        }

        [HttpPost]
        public ViewResult EntreFechas(CriteriosBusquedaEntreFechasViewModel searchCriteria)
        {
            var vm = new InformeResumenEntreFechasViewModel
            {
                Inicio = searchCriteria.Inicio,
                Fin = searchCriteria.Fin,
            };

            if (this.ModelState.IsValid)
            {
                var resumenJornadas = this.jornadaQueries.ObtenerResumenEntreFechas(
                        this.User.Identity.Name,
                        searchCriteria.Inicio,
                        searchCriteria.Fin).ToArray();

                vm.ResumenJornadas = resumenJornadas;
                vm.IsPost = true;
            }

            return this.View(vm);
        }
    }
}
