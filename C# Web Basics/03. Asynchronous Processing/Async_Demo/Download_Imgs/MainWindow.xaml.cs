using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Download_Imgs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowImage(Image1, "https://vignette.wikia.nocookie.net/spongebob/images/2/2a/SpongeBob_SquarePants%28copy%290.png/revision/latest?cb=20160507142128");
            ShowImage(Image2, "https://static01.nyt.com/images/2018/05/03/us/03spongebob_xp/03spongebob_xp-articleLarge.jpg");
            ShowImage(Image3, "https://assets.foxdcg.com/dpp-uploaded/images/the-simpsons/keyart1.jpg");
            ShowImage(Image4, "https://assets1.ignimgs.com/2018/09/28/simpsonstop25-1280-1538160288952_1280w.jpg");
            ShowImage(Image5, "https://i.ytimg.com/vi/vM4aTLBi_zg/maxresdefault.jpg");
            ShowImage(Image6, "https://bgdrehi.com/userfiles/brand/image_0c332c4330fc825b7fd38c5c6d5e7562.jpg");
        }

        private void ShowImage(Image image, string url)
        {
            WebClient client = new WebClient();
            var result = client.DownloadData(url);
            var bitmapImage = LoadImage(result);
            Thread.Sleep(2000);
            image.Source = bitmapImage;
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
