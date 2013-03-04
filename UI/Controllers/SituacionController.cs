// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SituacionController.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the SituacionController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Controllers
{
    using System;
    using System.Web.Mvc;

    using Acheve.Data.Services.Contracts;
    using Acheve.Domain.Services.Contracts;
    using Acheve.UI.Infrastructure;
    using Acheve.UI.ViewModels;

    public class SituacionController : Controller
    {
        private readonly IJornadaService jornadaService;

        private readonly IJornadaQueries jornadaQueries;

        public SituacionController(IJornadaService jornadaService, IJornadaQueries jornadaQueries)
        {
            if (jornadaService == null)
            {
                throw new ArgumentNullException("jornadaService");
            }

            if (jornadaQueries == null)
            {
                throw new ArgumentNullException("jornadaQueries");
            }

            this.jornadaService = jornadaService;
            this.jornadaQueries = jornadaQueries;
        }

        public ViewResult Actual()
        {
            var jornada = this.jornadaQueries.ObtenerUltimaJornada(this.User.Identity.Name);
            var vm = new JornadaViewModel
            {
                Jornada = jornada,
                Error = (string)this.TempData[Constantes.TempDataErrorKey]
            };

            return this.View(vm);
        }

        [ChildActionOnly]
        public PartialViewResult UltimasJornadas()
        {
            var ultimasJornadasRegistradas =
                this.jornadaQueries.ObtenerDiasUltimasJornadasRegistradas(this.User.Identity.Name);

            var vm = new JornadaNoIniciadaViewModel(ultimasJornadasRegistradas);

            return this.PartialView(vm);
        }

        [HttpPost]
        public RedirectToRouteResult IniciarJornada()
        {
            try
            {
                this.jornadaService.IniciarJornada(this.User.Identity.Name);
            }
            catch (InvalidOperationException e)
            {
                this.TempData[Constantes.TempDataErrorKey] = e.Message;
            }

            return this.RedirectToAction("Actual");
        }

        [HttpPost]
        public RedirectToRouteResult TerminarJornada()
        {
            try
            {
                this.jornadaService.TerminarJornada(this.User.Identity.Name);
            }
            catch (InvalidOperationException e)
            {
                this.TempData[Constantes.TempDataErrorKey] = e.Message;
            }

            return this.RedirectToAction("Actual");
        }

        [HttpPost]
        public RedirectToRouteResult IniciarPausa()
        {
            try
            {
                this.jornadaService.IniciarPausa(this.User.Identity.Name);
            }
            catch (InvalidOperationException e)
            {
                this.TempData[Constantes.TempDataErrorKey] = e.Message;
            }

            return this.RedirectToAction("Actual");
        }

        [HttpPost]
        public RedirectToRouteResult TerminarPausa()
        {
            try
            {
                this.jornadaService.TerminarPausa(this.User.Identity.Name);
            }
            catch (InvalidOperationException e)
            {
                this.TempData[Constantes.TempDataErrorKey] = e.Message;
            }

            return this.RedirectToAction("Actual");
        }
    }
}
