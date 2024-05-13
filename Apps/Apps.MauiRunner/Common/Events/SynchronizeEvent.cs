namespace Apps.MauiRunner.Common.Events;

public static class SynchronizeEvent
{
    public delegate Task Handler(object? sender, Args e);

    public class Args
    {
    }
}