using System.ComponentModel.DataAnnotations;

namespace Sirius.Application;

public record BaseDto<T>
{
    public required T Id { get; set; }
    public int? EntryBy { get; set; }
    public DateTime? EntryDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}