using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Controllers
{
    public class LichSuController : Controller
    {
        // GET: LichSuController
        public ActionResult Index(int userId)
        {
            ViewBag.UserId = userId;
            return View();
        }
        // GET: LichSuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
