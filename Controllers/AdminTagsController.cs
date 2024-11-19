using Microsoft.AspNetCore.Mvc;
using Webapp1.Data;
using Webapp1.Models.ViewModels;
using Webapp1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Webapp1.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly Webapp1DbContext webapp1DbContext;
        public AdminTagsController(Webapp1DbContext webapp1DbContext)
        {
            this.webapp1DbContext = webapp1DbContext; 
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await webapp1DbContext.Tags.AddAsync(tag);
            await webapp1DbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()   
        {
            // Use dcContext to read tags from the database
            var tags = await webapp1DbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await webapp1DbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                }; 
                return View(editTagRequest);
            }
            return View(null);
       
        }
        [HttpPost]
         public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
         {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = await webapp1DbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes
               await webapp1DbContext.SaveChangesAsync();

                //Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
         }

         public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
         {
           var tag = await webapp1DbContext.Tags.FindAsync(editTagRequest.Id);

              if (tag != null)
              {
                webapp1DbContext.Tags.Remove(tag);
                await webapp1DbContext.SaveChangesAsync();

                //show success notification
                return RedirectToAction("List");
              }
                //show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
         }
       
    }
}

