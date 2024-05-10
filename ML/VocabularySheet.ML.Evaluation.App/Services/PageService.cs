using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;
using VocabularySheet.ML.Client;
using VocabularySheet.ML.Evaluation.App.Contracts.Services;
using VocabularySheet.ML.Evaluation.App.ViewModels;
using VocabularySheet.ML.Evaluation.App.Views;

namespace VocabularySheet.ML.Evaluation.App.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<MainViewModel, MainPage>();
        Configure<PredictionViewModel, PredictionPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}

internal class MlModelsFolder : IMlModelsFolder
{
    public static readonly string BasePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "models");
    
    public async Task<Stream> GetModel(string path)
    {
        await Task.CompletedTask;
        var modelPath = Path.Combine(BasePath, path);
        
        if (File.Exists(modelPath))
        {
            return File.OpenRead(modelPath);
        }
        else
        {
            throw new FileNotFoundException($"Model file not found at path: {modelPath}");
        }
    }
}
