using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VocabularySheet.Maui.Domain.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
	protected IMediator Mediator { get; }
	protected ILogger Logger { get; }

    protected BaseViewModel(IMediator mediator, ILogger logger) 
	{
        Mediator = mediator;
        Logger = logger;
    }
}
