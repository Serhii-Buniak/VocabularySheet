using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Dtos;

public record CategoryItem
{
    public Category? Key { get; set; }
    public string Description { get; set; }

    public CategoryItem(Category? key, string description)
    {
        Key = key;
        Description = description;
    }

    public override string ToString()
    {
        return Description;
    }
}