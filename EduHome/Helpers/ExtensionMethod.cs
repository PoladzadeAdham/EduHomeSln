using System.Threading.Tasks;

namespace EduHome.Helpers
{
    public static class ExtensionMethod
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string folderPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath, uniqueFileName);

            using FileStream fs = new(path, FileMode.Create);

            await file.CopyToAsync(fs);

            return uniqueFileName;
        }

        public static bool CheckSize(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024* 1024;
        }

        public static bool CheckType(this IFormFile file, string type = "image")
        {
            return file.ContentType.Contains(type);
        }

    }
}
