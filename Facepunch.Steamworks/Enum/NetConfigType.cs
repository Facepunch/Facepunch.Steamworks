namespace Steamworks.Data
{
	enum NetConfigType
	{
		Int32 = 1,
		Int64 = 2,
		Float = 3,
		String = 4,
		FunctionPtr = 5, // NOTE: When setting	callbacks, you should put the pointer into a variable and pass a pointer to that variable.

		Force32Bit = 0x7fffffff
	};
}
