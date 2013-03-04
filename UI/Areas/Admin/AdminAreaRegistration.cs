// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminAreaRegistration.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the AdminAreaRegistration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin
{
    using System.Web.Mvc;

    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}
