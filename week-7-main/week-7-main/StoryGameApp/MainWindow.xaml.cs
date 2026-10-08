using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace StoryGameApp;

public partial class MainWindow : Window
{
    // Each Slide keeps its text, image and description together.
    private record Slide(
        string Text,
        string ImagePath,
        string Description
    );

    // =========================
    // 4 SLIDES TOTAL
    // =========================

    private readonly Slide[] slides =
    {
        new Slide(
            "Iron Man and Captain America had been hiding their relationship from the other avengers for a while. Captain America was sick of hiding, " +
            "but Iron Man was worried they would be judged... While Captain America, Iron Man, and Thor are fighting Thanos, they work together to defeat him. ",
            "Assets/Images/01.png",
            " "
        ),

        new Slide(
            "During the fight, Thor ends up saving Captain America and Captain America realizes that maybe he's been barking up the wrong tree.",
            "Assets/Images/02.png",
            " "
        ),

        new Slide(
            "As Thanos laid on the ground, Captain America and Thor got closer and held hands... Captain America blushed and so did Thor. However, Iron Man was watching..." +
            " He was watching his love get taken away by Thor.",
            "Assets/Images/03.png",
            " "
        ),

        new Slide(
            "In the end, Iron Man makes a selfish decision. He uses Jarvis to neuter Thor. As Thor yelped in pain, Iron Man realizes his mistakes and decides to show off Captain America to everyone. " +
            "They lived happily ever after.",
            "Assets/Images/04.png",
            " "
        )
    };

    // Array positions start at zero.
    private int currentSlide = 0;

    public MainWindow()
    {
        InitializeComponent();

        ShowSlide(currentSlide);
    }

    private void ShowSlide(int index)
    {
        if (index < 0 || index >= slides.Length)
            return;

        currentSlide = index;

        Slide slide = slides[currentSlide];

        StoryTextBlock.Text = slide.Text;
        ImageDescription.Text = slide.Description;

        SlideCounter.Text =
            $"Slide {currentSlide + 1} of {slides.Length}";

        BackButton.IsEnabled = currentSlide > 0;

        NextButton.IsEnabled =
            currentSlide < slides.Length - 1;

        try
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                slide.ImagePath
            );

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    "Image file was not found.",
                    path
                );
            }

            var image = new BitmapImage();

            image.BeginInit();

            image.CacheOption = BitmapCacheOption.OnLoad;

            image.CreateOptions =
                BitmapCreateOptions.IgnoreImageCache;

            image.UriSource =
                new Uri(path, UriKind.Absolute);

            image.EndInit();

            StoryImage.Source = image;

            StatusText.Text = "Story ready.";
        }
        catch (Exception ex)
            when (
                ex is IOException ||
                ex is NotSupportedException ||
                ex is System.IO.FileFormatException
            )
        {
            StoryImage.Source = null;

            StatusText.Text =
                "Image unavailable. Check Assets/Images and the filename. "
                + ex.Message;
        }
    }

    private void NextButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowSlide(currentSlide + 1);
    }

    private void BackButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowSlide(currentSlide - 1);
    }

    private void RestartButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowSlide(0);
    }

    // DAY 7B:
    // paste CaptureHandlers.cs.txt HERE,
    // inside these class braces.
}