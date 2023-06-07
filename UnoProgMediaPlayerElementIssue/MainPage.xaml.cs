
namespace UnoProgMediaPlayerElementIssue
{
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Windows.Media.Core;
    using Windows.Media.Playback;
    using Windows.UI.Core;

    public sealed partial class MainPage : Page
    {
        private SynchronizationContext dispatcher;

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.dispatcher = SynchronizationContext.Current;
                this.hiddenMediaPlayer.MediaPlayer.MediaEnded += this.OnMediaEnded;
                this.hiddenMediaPlayer.MediaPlayer.MediaFailed += this.OnMediaFailed;
            };

            this.Unloaded += (s, e) =>
            {
                this.hiddenMediaPlayer.MediaPlayer.MediaEnded -= this.OnMediaEnded;
                this.hiddenMediaPlayer.MediaPlayer.MediaFailed -= this.OnMediaFailed;
            };
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.IsEnabled = false;
            }

            var soundSource = "ms-appx:///CommonMedia/bong.mp3";
            this.hiddenMediaPlayer.Source = MediaSource.CreateFromUri(new Uri(soundSource));

            if (this.hiddenMediaPlayer.MediaPlayer != null)
            {
                this.hiddenMediaPlayer.MediaPlayer.Play();
            }
        }

        private void OnMediaEnded(MediaPlayer mediaPlayer, object args)
        {
            this.dispatcher.Post((_) =>
            {
                this.playButton.IsEnabled = true;
            }, null);
    }

    private void OnMediaFailed(MediaPlayer mediaPlayer, object args)
        {
            this.dispatcher.Post((_) =>
            {
                this.playButton.IsEnabled = true;
            }, null);
        }
    }
}