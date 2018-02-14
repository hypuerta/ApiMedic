// -------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Implements Home Controller.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Api.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Implements Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns>Index view.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return this.View();
        }
    }
}