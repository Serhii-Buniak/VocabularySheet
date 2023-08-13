using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VocabularySheet.Maui.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
	protected IMediator Mediator { get; }
	protected ILogger<BaseViewModel> Logger { get; }

    protected BaseViewModel(IMediator mediator, ILogger<BaseViewModel> logger) 
	{
        Mediator = mediator;
        Logger = logger;
    }
}
