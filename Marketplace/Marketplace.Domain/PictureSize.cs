using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class PictureSize
    {
        public int Height { get; internal set; }

        public int Width { get; internal set; }

        public PictureSize(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Picture width must be a positive number");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Picture height must be a positive number");
            }

            Width = width;
            Height = height;
        }

        internal PictureSize()
        {

        }
    }
}
