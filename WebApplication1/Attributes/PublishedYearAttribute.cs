using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Attributes
{
    // Validates a reasonable printed-book range (adjust as you like)
    public sealed class PublishedYearAttribute : ValidationAttribute
    {
        public int Min { get; }
        public int Max { get; }

        public PublishedYearAttribute(int min = 1450, int max = 2100)
        {
            Min = min;
            Max = max;
            ErrorMessage = $"Year must be between {Min} and {Max}.";
        }

        public override bool IsValid(object? value)
        {
            if (value is null) return true; // [Required] handles nulls
            if (value is int y) return y >= Min && y <= Max;
            return false;
        }
    }
}
