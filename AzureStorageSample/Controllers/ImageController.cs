using AzureStorageSample.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections;
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
            CloudStorageAccount storageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("sampleimage");

            List<string> blobs = new List<string>();
            foreach(var blobItem in container.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
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