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

        public bool IsLoaded { get; internal set; }

        /// <summary>
        /// Return true if this image couldn't be loaded for some reason
        /// </summary>
        public bool IsError { get; internal set; }

        public void Read( byte[] buffer )
        {

        }

    }
}
