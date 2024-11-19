using Microsoft.AspNetCore.Mvc;
using Webapp1.Models.ViewModels;
using Webapp1.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Webapp1.Controllers
{
    public class AdminInnmeldingerController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminInnmeldingerController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddInnmeldingRequest
            {
                Tags = tags.Select(x => new SelectListItem {Text = x.DisplayName, Value = x.Id.ToString() })
               
            };
            return View(model);
        }


        public async Task<IActionResult> Add(AddInnmeldingRequest addInnmeldingRequest)
        {
           
            {
                return RedirectToAction("Add");
            }
            
        }
    }
}