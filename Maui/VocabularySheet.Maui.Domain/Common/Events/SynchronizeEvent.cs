namespace VocabularySheet.Maui.Domain.Common.Events;

public static class SynchronizeEvent
{
    public delegate Task Handler(object? sender, Args e);

    public class Args
    {
    }
}