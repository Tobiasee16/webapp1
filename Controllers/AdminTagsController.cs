using Microsoft.AspNetCore.Mvc;
using Webapp1.Data;
using Webapp1.Models.ViewModels;
using Webapp1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Webapp1.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace Webapp1.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminTagsController(ITagRepository tagRepository)

        {
            this.tagRepository = tagRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(
            string? searchQuery,
            string? sortBy,
            string? sortDirection)          
        {
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            
            // Use dcContext to read tags from the database
            var tags = tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetByIdAsync(id);
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
           var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //Show success notification
            }
            else
            {
            //Show error notification
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
         }


          [HttpPost]
         public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
         {
          var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
                //show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
         }

        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("Name", "Name and Display Name must be different");
                }
               
            }
        }
       
    }
}

