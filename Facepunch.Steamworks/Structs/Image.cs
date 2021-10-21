
namespace Steamworks.Data
{
	public struct Image
	{
		public uint Width;
		public uint Height;
		public byte[] Data;

		/// <summary>
		/// Returns the color of the pixel at the specified position.
		/// </summary>
		/// <param name="x">X-coordinate</param>
		/// <param name="y">Y-coordinate</param>
		/// <returns>The color.</returns>
		/// <exception cref="System.ArgumentException">If the X and Y or out of bounds.</exception>
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
		/// Returns "{Width}x{Height} ({length of <see cref="Data"/>}bytes)"
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{Width}x{Height} ({Data.Length}bytes)";
		}
	}

	/// <summary>
	/// Represents a color.
	/// </summary>
	public struct Color
	{
		public byte r, g, b, a;
	}
}
