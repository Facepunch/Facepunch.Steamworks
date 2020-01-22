namespace Steamworks.Data
{
	internal enum NetScope : int
	{
		Global = 1,
		SocketsInterface = 2,
		ListenSocket = 3,
		Connection = 4,

		Force32Bit = 0x7fffffff
	}
}
