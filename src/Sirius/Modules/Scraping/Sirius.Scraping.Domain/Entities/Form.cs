using Sirius.Scraping.Domain.Enums;

namespace Sirius.Scraping.Domain.Entities;

public class Form : Base<int>
{
       public string? Title { get; set; }
       public string? Description { get; set; }
       // public FormType? FormType { get; set; }
       public string? Url { get; set; }
       //public FileStream? Pdf { get; set; }
} 