using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Webapp1.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Webapp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
       public async Task<IActionResult> UploadAsync(IFormFile file)
        {
        //call a repository
        var imageURL = await imageRepository.UploadAsync(file);

        if (imageURL != null)
        {
            return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
        }
        return new JsonResult(new { link = imageURL});
    }
}
}
