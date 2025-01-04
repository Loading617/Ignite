using System.Collections.Generic;
using System.Windows;

namespace InstagramViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadFeed_Click(object sender, RoutedEventArgs e)
        {
            string accessToken = AccessTokenBox.Text;

            if (string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Please enter a valid access token.");
                return;
            }

            try
            {
                var instagramFeed = new InstagramFeed();
                List<InstagramPost> posts = await instagramFeed.GetInstagramFeedAsync(accessToken);
                FeedListView.ItemsSource = posts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading feed: {ex.Message}");
            }
        }
    }
}
