// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuscarController.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BuscarController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;

    using Acheve.Data.Services.Contracts;
    using Acheve.UI.Areas.Admin.ViewModels;

    public class BuscarController : Controller
    {
        private readonly IJornadaQueries jornadaQueries;

        private readonly IUsuariosQueries usuariosQueries;

        public BuscarController(IJornadaQueries jornadaQueries, IUsuariosQueries usuariosQueries)
        {
            if (jornadaQueries == null)
            {
                throw new ArgumentNullException("jornadaQueries");
            }

            if (usuariosQueries == null)
            {
                throw new ArgumentNullException("usuariosQueries");
            }

            this.jornadaQueries = jornadaQueries;
            this.usuariosQueries = usuariosQueries;
        }

        public ActionResult Index()
        {
            var vm = new BuscarJornadasViewModel
            {
                Inicio = DateTime.Today.AddDays(-7),
                Fin = DateTime.Today,
                Usuarios = this.usuariosQueries.ObtenerUsuarios(),
                IsPost = false
            };

            return this.View(vm);
        }

        [HttpPost]
        public ActionResult Index(CriteriosBusquedaEntreFechasViewModel searchCriteria)
        {
            var vm = new BuscarJornadasViewModel
            {
                Inicio = searchCriteria.Inicio,
                Fin = searchCriteria.Fin,
                Usuario = searchCriteria.Usuario,
                Usuarios = this.usuariosQueries.ObtenerUsuarios(),
            };

            if (this.ModelState.IsValid)
            {
                var jornadas = this.jornadaQueries.ObtenerInformacionJornadasEntreFechas(
                    searchCriteria.Usuario, searchCriteria.Inicio, searchCriteria.Fin);

                vm.IsPost = true;
                vm.Usuario = searchCriteria.Usuario;
                vm.Jornadas = jornadas;
            }

            return this.View(vm);
        }
    }
}
