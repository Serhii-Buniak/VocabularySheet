﻿namespace VocabularySheet.Application.Commons.Dtos;

public record WordReadDto
{
    public required long Index { get; set; }

    public required string Original { get; set; }

    public required string Translation { get; set; }

    public string? Description { get; set; }
}