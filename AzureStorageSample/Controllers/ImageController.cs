using AzureStorageSample.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AzureStorageSample.Controllers
{
    public class ImageController : Controller
    {
        ImageService imageService = new ImageService();

        // GET: Image
        public ActionResult Index()
        {
            var LastestImage = string.Empty;
            if(TempData["LastestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LastestImage"]);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase photo) 
        {
            var imageUrl = await imageService.UploadImageAsync(photo);
            TempData["LastestImage"] = imageUrl.ToString();
            return RedirectToAction("Index", "Image");
        }
    }
}