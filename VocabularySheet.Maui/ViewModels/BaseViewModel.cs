using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;

namespace VocabularySheet.Maui.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
	protected IMediator Mediator { get; }

    protected BaseViewModel(IMediator mediator) 
	{
        Mediator = mediator;
    }
}