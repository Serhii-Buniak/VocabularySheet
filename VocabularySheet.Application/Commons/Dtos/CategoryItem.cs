﻿using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Dtos;

public record CategoryItem(Category? Key, string Description)
{
    public override string ToString()
    {
        return Description;
    }
}