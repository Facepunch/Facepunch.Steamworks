
namespace Steamworks.Data
{
	/// <summary>
	/// A raw, uncompressed RGBA bitmap handed over by Steam &#8212; in practice a user's avatar, or
	/// an achievement icon. There is no file format here: it is a plain pixel buffer you upload to a
	/// texture yourself.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Pixels are 4 bytes each in <b>RGBA</b> order, laid out row by row starting at the <b>top-left
	/// corner</b>. Graphics APIs that expect a bottom-left origin (OpenGL) need the rows flipped, and
	/// engines that want BGRA need the channels swizzled. Getting either wrong is why an avatar
	/// renders upside down or blue-tinted.
	/// </para>
	/// <para>
	/// Avatars come in fixed sizes and are <b>not</b> available until Steam has the user's image
	/// cached; asking too early yields nothing, and the image arrives later via the persona-state
	/// callback. See the avatar members on <see cref="Steamworks.Friend"/>.
	/// </para>
	/// </remarks>
	/// <example>
	/// Turning a friend's avatar into an engine texture:
	/// <code>
	/// var avatar = await friend.GetMediumAvatarAsync();
	/// if ( avatar.HasValue )
	/// {
	///     var img = avatar.Value;
	///     var tex = new Texture( (int)img.Width, (int)img.Height );
	///
	///     // Data is RGBA, top-left origin, 4 bytes per pixel
	///     tex.LoadRawTextureData( img.Data );
	///     tex.Apply();
	/// }
	/// </code>
	/// </example>
	public struct Image
	{
		/// <summary>Width of the image in pixels.</summary>
		public uint Width;

		/// <summary>Height of the image in pixels.</summary>
		public uint Height;

		/// <summary>
		/// The raw pixels: <c>Width * Height * 4</c> bytes, RGBA order, top-left origin, no padding
		/// between rows and no header. Freshly allocated managed memory, so it is yours to keep and
		/// safe to hold on to.
		/// </summary>
		public byte[] Data;

		/// <summary>
		/// Returns the color of the pixel at the specified position.
		/// </summary>
		/// <param name="x">X-coordinate, 0 at the left edge.</param>
		/// <param name="y">Y-coordinate, <b>0 at the top edge</b>, not the bottom.</param>
		/// <returns>The color, with all four channels.</returns>
		/// <exception cref="System.ArgumentException">If X or Y is out of bounds.</exception>
		/// <remarks>
		/// A convenience for inspecting or converting a few pixels &#8212; it recomputes the offset
		/// and copies four bytes per call, so do not build a whole texture with it. Blit
		/// <see cref="Data"/> directly instead.
		/// </remarks>
		public Color GetPixel( int x, int y )
		{
			if ( x < 0 || x >= Width ) throw new System.ArgumentException( "x out of bounds" );
			if ( y < 0 || y >= Height ) throw new System.ArgumentException( "y out of bounds" );

			Color c = new Color();

			var i = (y * Width + x) * 4;

			c.r = Data[i + 0];
			c.g = Data[i + 1];
			c.b = Data[i + 2];
			c.a = Data[i + 3];

			return c;
		}

		/// <summary>
		/// Returns "{Width}x{Height} ({length of <see cref="Data"/>}bytes)", for logging.
		/// </summary>
		/// <returns>A short description of the image's dimensions and byte count.</returns>
		/// <exception cref="System.NullReferenceException">
		/// Thrown on a default-constructed <see cref="Image"/>, because <see cref="Data"/> is null
		/// until something populates it.
		/// </exception>
		public override string ToString()
		{
			return $"{Width}x{Height} ({Data.Length}bytes)";
		}
	}

	/// <summary>
	/// A single 8-bit-per-channel RGBA color, as returned by <see cref="Image.GetPixel"/>.
	/// </summary>
	/// <remarks>
	/// Deliberately minimal and independent of any engine's color type &#8212; convert to your own
	/// before doing arithmetic with it. Channels are raw bytes, not normalised floats, and are not
	/// premultiplied.
	/// </remarks>
	public struct Color
	{
		/// <summary>Red, green, blue and alpha channels, each 0-255.</summary>
		public byte r, g, b, a;
	}
}
