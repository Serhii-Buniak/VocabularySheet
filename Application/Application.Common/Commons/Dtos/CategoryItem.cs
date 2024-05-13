using Domain.WordModels;

namespace Application.Common.Commons.Dtos;

public record CategoryItem(Category? Key, string Description)
{
    public override string ToString()
    {
        return Description;
    }
}