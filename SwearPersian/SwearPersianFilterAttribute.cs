using System.ComponentModel.DataAnnotations;

namespace SwearPersian;

[AttributeUsage(AttributeTargets.Property)]
public class SwearPersianFilterAttribute : ValidationAttribute
{
    private FilterPersianWords _service;
    public SwearPersianFilterAttribute()
    {
        _service = new FilterPersianWords();
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var valuee= value as string;

        if (string.IsNullOrWhiteSpace(valuee))
            return ValidationResult.Success;

        if (_service.IsSensitivePhrase(valuee))
            throw new ArgumentException();

        if (_service.IsSensitiveSentence(valuee))
            throw new ArgumentException();

        if(_service.RemoveSensitivePhrasesSpace(valuee))
            throw new ArgumentException();

        return ValidationResult.Success;
    }
}
