using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public class Image
    {
        public int Id { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }

        public byte[] Data { get; internal set; }

        public bool IsLoaded { get; internal set; }

        /// <summary>
        /// Return true if this image couldn't be loaded for some reason
        /// </summary>
        public bool IsError { get; internal set; }

        unsafe internal bool TryLoad( SteamNative.SteamUtils utils )
        {
            if ( IsLoaded ) return true;

            uint width = 0, height = 0;

            if ( utils.GetImageSize( Id, out width, out height ) == false )
            {
                IsError = true;
                return false;
            }

            var buffer = new byte[ width * height * 4 ];

            fixed ( byte* ptr = buffer )
            {
                if ( utils.GetImageRGBA( Id, (IntPtr) ptr, buffer.Length ) == false )
                {
                    IsError = true;
                    return false;
                }
            }

            Width = (int) width;
            Height = (int) height;
            Data = buffer;
            IsLoaded = true;
            IsError = false;

            return true;
        }

        public Color GetPixel( int x, int y )
        {
            if ( !IsLoaded ) throw new System.Exception( "Image not loaded" );
            if ( x < 0 || x >= Width ) throw new System.Exception( "x out of bounds" );
            if ( y < 0  || y >= Height ) throw new System.Exception( "y out of bounds" );

            Color c = new Color();

            var i = ( y * Width + x ) * 4;

            c.r = Data[i + 0];
            c.g = Data[i + 1];
            c.b = Data[i + 2];
            c.a = Data[i + 3];

            return c;
        }
    }

    public struct Color
    {
        public byte r, g, b, a;
    }
}


