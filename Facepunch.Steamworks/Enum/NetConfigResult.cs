namespace Steamworks.Data
{
	enum NetConfigResult
	{
		BadValue = -1, // No such configuration value
		BadScopeObj = -2,  // Bad connection handle, etc
		BufferTooSmall = -3, // Couldn't fit the result in your buffer
		OK = 1,
		OKInherited = 2, // A value was not set at this level, but the effective (inherited) value was returned.

		Force32Bit = 0x7fffffff
	};
}
