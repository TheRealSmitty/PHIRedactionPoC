using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PHIRedactionPoC.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _hostingEnvironment;
        private readonly List<string> keywords = new List<string>() { "name", "birth", "number", "address", "email" };
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _hostingEnvironment = environment;
        }

        public void OnGet() { }

        public void OnPost()
        {
            try
            {
                var files = Request.Form.Files;
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        var redactedText = "";
                        String line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            //20241118 CS - This does consider that we have a ":" in the line.
                            if (keywords.Any(line.ToLower().Contains))
                                redactedText += $"{line.Replace(line.Split(':').Last(), " [REDACTED]")}\n";
                            else
                                redactedText += $"{line}\n";
                        }
                        var originalFilePath = Path.Combine(Path.Combine(_hostingEnvironment.ContentRootPath, "Data"), file.FileName);
                        var redactedFilePath = Path.Combine(Path.Combine(_hostingEnvironment.ContentRootPath, "Sanitized"), $"{Path.GetFileNameWithoutExtension(file.FileName)}_sanitized.txt");
                        //Original
                        System.IO.File.WriteAllText(originalFilePath, redactedText);
                        //Redacted
                        System.IO.File.WriteAllText(redactedFilePath, redactedText);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}