using Microsoft.AspNetCore.Mvc;
using Webapp1.Models.ViewModels;
using Webapp1.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Webapp1.Models.Domain;
using System.Reflection.Metadata.Ecma335;



namespace Webapp1.Controllers
{
    public class AdminInnmeldingerController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IInnmeldingerRepository innmeldingerRepository;
        public AdminInnmeldingerController(ITagRepository tagRepository, IInnmeldingerRepository innmeldingerRepository)
        {
            this.tagRepository = tagRepository;
            this.innmeldingerRepository = innmeldingerRepository;
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
                //Map ViewModel to domain Model
                var innmelding = new Innmelding
                {
                    Heading = addInnmeldingRequest.Heading,
                    PageTitle = addInnmeldingRequest.PageTitle,
                    Content = addInnmeldingRequest.Content,
                    ShortDescription = addInnmeldingRequest.ShortDescription,
                    FeaturedImageUrl = addInnmeldingRequest.FeaturedImageUrl,
                    UrlHandle = addInnmeldingRequest.UrlHandle,
                    PublishedDate = addInnmeldingRequest.PublishedDate,
                    Author = addInnmeldingRequest.Author,
                    Visible = addInnmeldingRequest.Visible,
                };
                //Map Tags from selected Tags
                var selectedTags = new List<Tag>();
                foreach (var selectedTagId in addInnmeldingRequest.SelectedTags)
                {
                    var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                    var existingTag = await tagRepository.GetByIdAsync(selectedTagIdAsGuid);

                    if (existingTag != null)
                    {
                        selectedTags.Add(existingTag);
                    }                    
                }

                //Mapping Tags back to Domain Model
                innmelding.Tags = selectedTags;


               await innmeldingerRepository.AddAsync(innmelding);
                return RedirectToAction("Add");
            }
            
        }

        public async Task<IActionResult> List()
        {
            // Call the repoistory
            var innmeldinger = await innmeldingerRepository.GetAllAsync();
            return View(innmeldinger);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the result from the repository
            var innmelding = await innmeldingerRepository.GetAsync(id); 
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (innmelding == null) 
            {

            // map the domain model into the view model
            var model = new EditInnmeldingRequest
           
            {
                Id = innmelding.Id,
                Heading = innmelding.Heading,
                PageTitle = innmelding.PageTitle,
                Content = innmelding.Content,
                Author = innmelding.Author, 
                FeaturedImageUrl = innmelding.FeaturedImageUrl,
                UrlHandle = innmelding.UrlHandle,
                ShortDescription = innmelding.ShortDescription,
                PublishedDate = innmelding.PublishedDate,
                Visible = innmelding.Visible,
                Tags = tagsDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Name, Value = x.Id.ToString()
                }),
                SelectedTags = innmelding.Tags.Select(x => x.Id.ToString()).ToArray()
            };
           
                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditInnmeldingRequest editInnmeldingRequest)
        {
            //map view model back to damin model
            var innmeldingDomainModel = new Innmelding
            {
                Id = editInnmeldingRequest.Id,
                Heading = editInnmeldingRequest.Heading,
                PageTitle = editInnmeldingRequest.PageTitle,
                Content = editInnmeldingRequest.Content,
                Author = editInnmeldingRequest.Author,
                FeaturedImageUrl = editInnmeldingRequest.FeaturedImageUrl, 
                UrlHandle = editInnmeldingRequest.UrlHandle,
                ShortDescription = editInnmeldingRequest.ShortDescription,
                PublishedDate = editInnmeldingRequest.PublishedDate,
                Visible = editInnmeldingRequest.Visible

            };

            //Map Tags into domain model
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editInnmeldingRequest.SelectedTags)
            {
                

                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetByIdAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            innmeldingDomainModel.Tags = selectedTags;

            //Submit information to repository to update
            var updatedInnmelding = await innmeldingerRepository.UpdateAsync(innmeldingDomainModel);

            if (updatedInnmelding != null)
            {
                //show success message
                return RedirectToAction("Edit");
            }

            //show error message
            return RedirectToAction("Edit");

            
        }
         [HttpGet]
        public async Task<IActionResult> Delete(EditInnmeldingRequest editInnmeldingRequest)
        {
            //Talk to repository to delete this innmelding  and tags
            var deletedInnmelding = await innmeldingerRepository.DeleteAsync(editInnmeldingRequest.Id);

            if (deletedInnmelding != null)
            {
                //show success message
                return RedirectToAction("List");
            }

            //show error notification
            return RedirectToAction("Edit", new { id = editInnmeldingRequest.Id });
          

        }
    }
}