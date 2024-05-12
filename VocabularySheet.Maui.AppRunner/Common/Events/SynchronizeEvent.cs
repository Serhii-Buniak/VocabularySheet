namespace VocabularySheet.Maui.AppRunner.Common.Events;

public static class SynchronizeEvent
{
    public delegate Task Handler(object? sender, Args e);

    public class Args
    {
    }
}