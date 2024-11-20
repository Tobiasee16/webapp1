namespace Webapp1.Repositories
{
     public interface IImageRepository 
     {
          Task<string> UploadAsync(IFormFile file);
     }
}