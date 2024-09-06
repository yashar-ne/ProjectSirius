using System.ComponentModel.DataAnnotations;

namespace Sirius.Scraping.Domain.Entities;

public class Base<T>
{
    [Key]
    public T? Id { get; set; }
    public int? EntryBy { get; set; }
    public DateTime? EntryDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}