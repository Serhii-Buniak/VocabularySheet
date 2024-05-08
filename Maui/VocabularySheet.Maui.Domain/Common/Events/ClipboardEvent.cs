namespace VocabularySheet.Maui.Domain.Common.Events;

public static class ClipboardEvent
{
    public delegate Task Handler(object? sender, Args e);

    public class Args
    {
        public required string Text { get; init; }
    }
}