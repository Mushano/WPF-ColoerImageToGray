using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace 数字图像处理1
{
    public class ImageHandler
    {
        public int Rows;
        public int Columns;
        public PixelFormat f;
        public byte[] R;
        public byte[] G;
        public byte[] B;
        public ImageHandler(BitmapImage image)
        {
            Rows = image.PixelWidth;
            Columns = image.PixelHeight;
            f = image.Format;
            int s = Columns * f.BitsPerPixel/8;
            R = new byte[Rows * s/4];
            G = new byte[Rows * s/4];
            B = new byte[Rows * s/4];
        }
        /// <summary>
        /// 获取像素的数组
        /// </summary>
        /// <param name="source"></param>
        public void GetPixelArray(byte[] g)
        {
            int i = 0;
            int bi = 0;
            int gi = 0;
            int ri = 0;
            while (i<g.Length)
            {
                switch (i%4)
                {
                    case 0:
                        B[bi] = g[i];
                        bi++;
                        break;
                    case 1:
                        G[gi] = g[i];
                        gi++;
                        break;
                    case 2:
                        R[ri] = g[i];
                        ri++;
                        break;
                    default:
                        break;
                }
                i++;
            }
        }

        /// <summary>
        /// 彩色转灰色具体的实现函数
        /// </summary>
        /// <param name="source">BitmapImage</param>
        /// <returns></returns>
        public BitmapSource Color2Gray(BitmapImage ima)
        {
            WriteableBitmap source = new WriteableBitmap(ima.PixelWidth,
                                                         ima.PixelHeight,
                                                         ima.DpiX,
                                                         ima.DpiY,
                                                         ima.Format,
                                                         ima.Palette);
            int _pixelWidth = source.PixelWidth;
            int _pixelheight = source.PixelHeight ;
            double _dpiX = source.DpiX;
            double _dpiY = source.DpiY;
            BitmapPalette _p = source.Palette;
            int _stride = source.PixelWidth * source.Format.BitsPerPixel/8;
            //测试
            byte[] g = new byte[ima.PixelHeight * _stride];
            ima.CopyPixels(g, _stride, 0);
            GetPixelArray(g);
            byte[] gray = GetGrayArray();
            return BitmapSource.Create(_pixelWidth,
                                        _pixelheight,
                                        _dpiX,
                                        _dpiY,
                                        PixelFormats.Gray8,
                                        _p,
                                        gray,
                                        _stride/4);
            
        }
        /// <summary>
        /// 获取灰色数组
        /// </summary>
        /// <returns></returns>
        public byte[] GetGrayArray()
        {
            byte[] gray = new byte[B.Length];
            for (int i = 0; i < B.Length; i++)
            {
                gray[i] = (byte)Rgb2Gray(R[i], G[i], B[i]);
            }
            return gray;
        }
        /// <summary>
        /// 灰色转换的具体公式
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Rgb2Gray(long r, long g, long b)
        {
            return (r*0.299+g*0.587+b*0.114); //只针对正常情况
        }

    }
}
