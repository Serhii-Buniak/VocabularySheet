using System.Globalization;
using System.Windows;

namespace VocabularySheet.ML.Evaluation.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MlWordEvaluationService _evaluationService;

    public MainWindow()
    {
        InitializeComponent();
        _evaluationService = new MlWordEvaluationService();
    }

    private async void RunEvaluation_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            RunEvaluationButton.IsEnabled = false;

            var metrics = await _evaluationService.EvaluateAsync(CancellationToken.None);

            if (metrics != null)
            {
                var results = new Dictionary<string, string>
                {
                    ["LogLoss"] = metrics.LogLoss.ToString(CultureInfo.InvariantCulture),
                    ["Macro Accuracy"] = metrics.MacroAccuracy.ToString(CultureInfo.InvariantCulture),
                    ["Micro Accuracy"] = metrics.MicroAccuracy.ToString(CultureInfo.InvariantCulture),
                    ["Confusion Matrix"] = metrics.ConfusionMatrix.GetFormattedConfusionTable(),
                };

                ResultList.ItemsSource = results;
            }
            else
            {
                ResultList.ItemsSource = new Dictionary<string, string> { { "Result", "Evaluation failed." } };
            }
        }
        finally
        {
            RunEvaluationButton.IsEnabled = true;
        }
    }
    
}
