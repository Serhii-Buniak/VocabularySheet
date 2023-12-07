﻿using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Parsing.Common;

public record WebPageResponse
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required string Html { get; init; }
    public required string Link { get; init; }
}