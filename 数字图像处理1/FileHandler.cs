using System;
using System.Windows.Media.Imaging;
using System.IO;

namespace 数字图像处理1
{
    static class FileHandler
    {
        /// <summary>
        /// 获取文件后缀格式
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static string GetExtName(string fileName)
        {
            string extName;
            if (fileName != "")
            {
                extName = fileName.Substring(fileName.IndexOf('.'));
                return extName;
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取文件目录
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="extName">后缀名</param>
        /// <returns></returns>
        public static string GetFileDictionary(string fileName, string extName)
        {
            string res;
            res = fileName.Replace(extName, "");
            return res;
        }
        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="savePath">图像保存路径</param>
        /// <param name="BS">控件源</param>
        /// <param name="extName">文件拓展名</param>
        public static void SaveImage(string savePath,BitmapSource BS,string extName)
        {
            BitmapEncoder encoder = null;
            switch (extName)
            {
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
                case ".jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case ".gif":
                    encoder = new GifBitmapEncoder();
                    break;
                case ".tiff":
                    encoder = new TiffBitmapEncoder();
                    break;
                default:
                    throw new InvalidOperationException();
            }
            encoder.Frames.Add(BitmapFrame.Create(BS));
            encoder.Save(File.Create(savePath));
        }
    }
}
