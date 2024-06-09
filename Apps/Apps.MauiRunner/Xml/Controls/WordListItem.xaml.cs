using System.Windows.Input;
using Application.Common.Commons.Dtos;
using Domain.WordModels;
using ML.Predictor;

namespace Apps.MauiRunner.Xml.Controls;

public partial class WordListItem : ContentView
{
    public static readonly BindableProperty WordProperty = BindableProperty.Create(
        nameof(Word),
        typeof(WordModel),
        typeof(WordListItem),
        propertyChanged: OnWordPropertyChanged);

    public static readonly BindableProperty OnClickProperty = BindableProperty.Create(
        nameof(OnClick),
        typeof(ICommand),
        typeof(WordListItem));

    public static readonly BindableProperty MaximumWidthProperty = BindableProperty.Create(
        nameof(MaximumWidth),
        typeof(int?),
        typeof(WordListItem));

    public static readonly BindableProperty IsWordVisibleProperty = BindableProperty.Create(
        nameof(IsWordVisible),
        typeof(bool),
        typeof(WordListItem),
        true,
        propertyChanged: OnWordVisiblePropertyChanged);

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(WordListItem));

    public static readonly BindableProperty ArticleColorProperty = BindableProperty.Create(
        nameof(ArticleColor),
        typeof(Color),
        typeof(WordListItem));

    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
        nameof(LineColor),
        typeof(Color),
        typeof(WordListItem));


    public WordListItem()
    {
        InitializeComponent();
    }

    public ICommand? OnClick
    {
        get => (ICommand)GetValue(OnClickProperty);
        set => SetValue(OnClickProperty, value);
    }

    public WordModel Word
    {
        get => (WordModel)GetValue(WordProperty);
        set => SetValue(WordProperty, value);
    }

    public int? MaximumWidth
    {
        get => (int?)GetValue(MaximumWidthProperty);
        set => SetValue(MaximumWidthProperty, value);
    }

    public bool IsWordVisible
    {
        get => (bool)GetValue(IsWordVisibleProperty);
        set => SetValue(IsWordVisibleProperty, value);
    }

    public Color? BorderColor
    {
        get => (Color?)GetValue(BorderColorProperty);
        private set => SetValue(BorderColorProperty, value);
    }

    public Color? ArticleColor
    {
        get => (Color?)GetValue(ArticleColorProperty);
        private set => SetValue(ArticleColorProperty, value);
    }

    public Color? LineColor
    {
        get => (Color?)GetValue(LineColorProperty);
        private set => SetValue(LineColorProperty, value);
    }

    private static void OnWordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is WordListItem wordListItem && newValue is WordModel newWord)
        {
            if (wordListItem.IsWordVisible)
            {
                wordListItem.LineColor = newWord.Category.ToMauiColor();
                wordListItem.ArticleColor = newWord.ArticleType.ToAlphaMauiColor();
                wordListItem.BorderColor = newWord.ArticleType.ToMauiColor();
            }
            else
            {
                SetDefaultColors(wordListItem);
            }
        }
    }

    private static void OnWordVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is WordListItem wordListItem && newValue is bool isVisible)
        {
            if (isVisible)
            {
                OnWordPropertyChanged(bindable, wordListItem.Word, wordListItem.Word);
            }
            else
            {
                SetDefaultColors(wordListItem);
            }
        }
    }

    private static void SetDefaultColors(WordListItem wordListItem)
    {
        wordListItem.LineColor = Category.Unknown.ToMauiColor();
        wordListItem.ArticleColor = ArticleType.Other.ToAlphaMauiColor();
        wordListItem.BorderColor = ArticleType.Other.ToMauiColor();
    }


    private async void Tapped(object sender, EventArgs e)
    {
        if (sender is Border frame)
        {
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Label or member is obsolete
            Color darkerColor = Color.FromHex("#0A0F1C"); // Adjust the color code as needed
#pragma warning restore CS0618 // Label or member is obsolete

            // Animate to darker color
            await frame.FadeTo(0.8, 10, Easing.Linear);
            frame.BackgroundColor = darkerColor;
            await frame.FadeTo(1, 10, Easing.Linear);

            // Implement your additional logic here

            // Animate back to the original color
            await frame.FadeTo(0.8, 10, Easing.Linear);
            frame.BackgroundColor = originalColor;
            await frame.FadeTo(1, 10, Easing.Linear);
        }

        OnClick?.Execute(Word.Id);
        // Implement your additional logic here
    }
}

