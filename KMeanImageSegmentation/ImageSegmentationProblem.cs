using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeanImageSegmentation
{
    class ImageSegmentationProblem
    {
        private Bitmap raw_color;
        private Bitmap raw_grayscale;
        private Bitmap result;

        public Bitmap GrayScale
        {
            get { return raw_grayscale; }
        }

        public Bitmap Result
        {
            get { return result; }
        }

        public void ReadFromFile(string path)
        {
            raw_color = (Bitmap)Image.FromFile(path);
            CreateGrayScale();
            result = new Bitmap(raw_color.Width, raw_color.Height);
        }

        private void CreateGrayScale()
        {
            raw_grayscale = new Bitmap(raw_color.Width, raw_color.Height);
            for (int i =0; i < raw_color.Width; i++)
            {
                for(int j = 0; j < raw_color.Height; j++)
                {
                    Color pixelColor = raw_color.GetPixel(i, j);
                    int newValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color newColor = Color.FromArgb(newValue, newValue, newValue);
                    raw_grayscale.SetPixel(i, j, newColor);
                }
            }
        }
    }
}
