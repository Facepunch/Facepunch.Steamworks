
namespace Steamworks.Data
{
	public struct Image
	{
		public uint Width;
		public uint Height;
		public byte[] Data;

		public Color GetPixel( int x, int y )
		{
			if ( x < 0 || x >= Width ) throw new System.Exception( "x out of bounds" );
			if ( y < 0 || y >= Height ) throw new System.Exception( "y out of bounds" );

			Color c = new Color();

			var i = (y * Width + x) * 4;

			c.r = Data[i + 0];
			c.g = Data[i + 1];
			c.b = Data[i + 2];
			c.a = Data[i + 3];

			return c;
		}

		public override string ToString()
		{
			return $"{Width}x{Height} ({Data.Length}bytes)";
		}
	}

	public struct Color
	{
		public byte r, g, b, a;
	}
}