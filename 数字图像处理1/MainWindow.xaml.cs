using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.Win32;

namespace 数字图像处理1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        BitmapImage bi = new BitmapImage();      //图像文件位图         
        string extName;                   //文件拓展名
        string fileDictionary;            //文件目录
        
        /// <summary>
        /// 打开图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "图像文件|*.jpg;*.png;*.jpeg;*.bmp;*.gif|所有文件|*.*";
            bool? result = fd.ShowDialog();
            string path = fd.FileName;
            //文件拓展名
            extName = FileHandler.GetExtName(path);
            fileDictionary = FileHandler.GetFileDictionary(path, extName);
            if (result ==true)
            {
                try
                {
                    bi = new BitmapImage(new Uri(path));
                    OriginalImageCtr.Source = bi;
                    TabControl1.SelectedIndex = 0;              //切换Tabcontrol的窗口
                }
                catch (Exception)
                {
                    throw new Exception("图片导入失败！");
                }
            }
        }
        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "图像文件|*"+extName;
            sf.DefaultExt = extName;
            sf.InitialDirectory = fileDictionary;
            bool? result = sf.ShowDialog();
            if (result ==true)
            {
                if (TabControl1.SelectedIndex==0)
                {
                    BitmapSource bs = (BitmapSource)OriginalImageCtr.Source;
                    FileHandler.SaveImage(sf.FileName, bs, extName);
                }
                if (TabControl1.SelectedIndex == 1)
                {
                    BitmapSource bs = (BitmapSource)GreyImageCtr.Source;
                    FileHandler.SaveImage(sf.FileName, bs, extName);
                }
            }
            
        }
        /// <summary>
        /// 图像灰阶处理
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void GreyHandle_Click(object Sender, RoutedEventArgs e)
        {
            /*        直接改变图像的图像格式
            FormatConvertedBitmap myFormatedConvertedBitmap = new FormatConvertedBitmap();
            myFormatedConvertedBitmap.BeginInit();
            myFormatedConvertedBitmap.Source = bi;
            myFormatedConvertedBitmap.DestinationFormat = PixelFormats.Gray8;
            myFormatedConvertedBitmap.EndInit();

            GreyImageCtr.Source = myFormatedConvertedBitmap;
            TabControl1.SelectedIndex = 1;
            */
            ImageHandler lh = new ImageHandler(bi);
            GreyImageCtr.Source = lh.Color2Gray(bi);
            TabControl1.SelectedIndex = 1;              //切换控件的窗口为灰色图像

        }
    }
}
