using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lord10.Forms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        private DispatcherTimer _updateTimer;
        private int _count;


        public About()
        {
            this.InitializeComponent();
            _count = 0;
            _updateTimer = new DispatcherTimer();
            _updateTimer.Interval = TimeSpan.FromSeconds(4.5);
            _updateTimer.Tick += UpdateTimer_Tick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _updateTimer.Start();
        }


        private async void UpdateTimer_Tick(object sender, object e)
        {
            // Updating the section triggers a cool animation!
            // See SectionView.xaml and SectionView.xaml.cs
            var sectionsInView = cMainHub.SectionsInView;
            var sectionsCount = cMainHub.Sections.Count;
            int old;
            // var index = cMainHub.

            if (sectionsCount > 0)
            {
                old = _count;
                _count++;
                if (_count > 2) _count = 0;

                //      await this.cMainHub.ScrollToSectionAnimated(cMainHub.Sections[old],cMainHub.Sections[_count]);

                cMainHub.ScrollToSection(cMainHub.Sections[_count]);
            }
        }


        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), e);
        }
    }
}
