using System;

namespace LD31.Math
{
    /// <summary>
    /// This value type represents a colour.
    /// </summary>
    public struct Colour
    {
        public static readonly Colour White = new Colour(255, 255, 255, 255);
        public static readonly Colour Red = new Colour(255, 0, 0, 255);
        public static readonly Colour Green = new Colour(0, 255, 0, 255);
        public static readonly Colour Blue = new Colour(0, 0, 255, 255);

        /// <summary>
        /// backing field
        /// </summary>
        private Byte _R;

        /// <summary>
        /// backing field
        /// </summary>
        private Byte _G;

        /// <summary>
        /// backing field
        /// </summary>
        private Byte _B;

        /// <summary>
        /// backing field
        /// </summary>
        private Byte _A;

        /// <summary>
        /// The red value
        /// </summary>
        public Byte R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
            }
        }

        /// <summary>
        /// The green value
        /// </summary>
        public Byte G
        {
            get
            {
                return _G;
            }
            set
            {
                _G = value;
            }
        }

        /// <summary>
        /// The blue value
        /// </summary>
        public Byte B
        {
            get
            {
                return _B;
            }
            set
            {
                _B = value;
            }
        }

        /// <summary>
        /// The alpha/transparency value.
        /// </summary>
        public Byte A
        {
            get
            {
                return _A;
            }
            set
            {
                _A = value;
            }
        }

        /// <summary>
        /// Constructor. This sets all RGB values to the same number.
        /// </summary>
        /// <param name="rgb"></param>
        /// <param name="a"></param>
        public Colour(Byte rgb, Byte a)
        {
            _R = rgb;
            _G = rgb;
            _B = rgb;
            _A = a;
        }

        /// <summary>
        /// Constructor. This sets all RGB values to the individual arguments..
        /// </summary>
        /// <param name="rgb"></param>
        /// <param name="a"></param>
        public Colour(Byte r, Byte g, Byte b, Byte a)
        {
            _R = r;
            _G = g;
            _B = b;
            _A = a;
        }
    }
}
