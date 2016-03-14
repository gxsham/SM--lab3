using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VideoProccess
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string FullVimeoUrl;
        double time; 
        MediaCapture capture = new MediaCapture();
        public MainPage()
        {
            this.InitializeComponent();
            capture.InitializeAsync();

            FullVimeoUrl = "http://qthttp.apple.com.edgesuite.net/1010qwoeiuryfg/sl.m3u8";

        }

        private async Task<IRandomAccessStream> file_pick()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".avi");
            openPicker.FileTypeFilter.Add(".mp4"); 

            StorageFile file = await openPicker.PickSingleFileAsync();
            return await file.OpenAsync(FileAccessMode.Read);
            
        }
      

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Visible; 
            if (CaptureElement.Source != null)
            {
                await capture.StopPreviewAsync();
                CaptureElement.Source = null;
            }
            mediaElement1.SetSource(await file_pick(), "");
           
            mediaElement1.Play();
            
        }

        private async  void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Visible;
            if (CaptureElement.Source != null)
            {
                await capture.StopPreviewAsync();
                CaptureElement.Source = null; 
            }
            mediaElement1.Source = new Uri(FullVimeoUrl.ToString(), UriKind.RelativeOrAbsolute);

        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Collapsed; 
            mediaElement1.Stop();
           
            CaptureElement.Source = capture;
            await capture.StartPreviewAsync();
            
           
        }

        private void Progress_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            time = mediaElement1.NaturalDuration.TimeSpan.TotalSeconds;
            if (time!=0)
            {
                mediaElement1.Position = TimeSpan.FromSeconds((e.NewValue * (time + 0.1) / 100));
            }
           

        }

        private void Volume_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
                mediaElement1.Volume = e.NewValue;
           
           
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
           
                mediaElement1.Pause();
          
            
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {

                mediaElement1.Play();
            
           
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
           
                Progress.Value = 0;
                mediaElement1.Stop();
        }
    }
}
