// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CuentaController.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the CuentaController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Controllers
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Security;

    using Acheve.UI.ViewModels;

    public class CuentaController : Controller
    {
        public ActionResult LogOn()
        {
            this.FillVersionInfo();
            return this.View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    if (this.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return this.Redirect(returnUrl);
                    }
                    else
                    {
                        return this.RedirectToAction("Actual", "Situacion", new { area = string.Empty });
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "El usuario o la contraseña no son correctas.");
                }
            }

            // If we got this far, something failed, redisplay form
            this.FillVersionInfo();
            return this.View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("LogOn", "Cuenta");
        }

        private void FillVersionInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.ViewBag.Version = string.Format("{0}.{1}", assemblyVersion.Major, assemblyVersion.Minor);
            this.ViewBag.FileVersion = fileVersionInfo.FileVersion;
        }
    }
}
