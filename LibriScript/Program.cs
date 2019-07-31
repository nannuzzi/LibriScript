using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibriScript
{
    class Program
    {
        public static int i = 1;
        public static int k = 754;
        static void Main(string[] args)
        {
            GetImage(@"C:\Users\Michele\Pictures\Screenshots");
            //TagliaImmagine("test1.jpg", 26, 1884, 111, 1045);
            //TagliaImmagine("test2.jpg", 26, 1884, 117, 966);
            //TagliaImmagine("test3.jpg", 26, 1884, 231, 1062);
            //UnireImmagini("test1.jpg.png", "test2.jpg.png", "test3.jpg.png");
        }

        static void UnireImmagini(string img1, string img2, string img3)
        {
            Image Img1 = Image.FromFile(img1);
            Image Img2 = Image.FromFile(img2);
            Image Img3 = Image.FromFile(img3);
            int width = Img1.Width;
            int height = Img1.Height + Img2.Height + Img3.Height;
            Bitmap Finale = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(Finale);
            g.Clear(Color.Violet);
            g.DrawImage(Img1, new Point(0, 0));
            g.DrawImage(Img2, new Point(0, Img1.Height));
            g.DrawImage(Img3, new Point(0, Img1.Height+Img2.Height));
            g.Dispose();
            Img1.Dispose();
            Img2.Dispose();
            Img3.Dispose();
            Finale.Save(k++ + ".png", System.Drawing.Imaging.ImageFormat.Png);
            Finale.Dispose();
        }

        static void TagliaImmagine(string img, int x, int xF, int y, int yF)
        {
            Rectangle cropRect = new Rectangle(x, y, xF, yF);
            Bitmap src = Image.FromFile(img) as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width-x, cropRect.Height-y);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, xF, yF),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            target.Save(i + ".png.appo");
            target.Dispose();
        }

        static void GetImage(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
            foreach (FileInfo file in files)
            {
                if (file.Name.Contains(".png")) 
                {
                    if (i == 1)
                        TagliaImmagine(file.FullName, 26, 1884, 111, 1045);
                    else if (i == 2)
                        TagliaImmagine(file.FullName, 26, 1884, 173, 966);
                    else
                        TagliaImmagine(file.FullName, 26, 1884, 176, 1007);
                    i++;
                    if (i == 4)
                    {
                        i = 1;
                        UnireImmagini("1.png.appo", "2.png.appo", "3.png.appo");
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }

            }

        }
    }
}
