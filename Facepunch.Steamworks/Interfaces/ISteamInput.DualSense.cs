using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Hand-written binding for <c>SteamAPI_ISteamInput_SetDualSenseTriggerEffect</c>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This lives outside <c>Generated/</c> on purpose. The code generator reads
	/// <c>steam_api.json</c> and cannot emit this function, because its parameter
	/// <c>ScePadTriggerEffectParam</c> contains a C <c>union</c>
	/// (<c>ScePadTriggerEffectCommandData</c>) and the generator has no union support.
	/// The function is genuinely exported by <c>steam_api64.dll</c> and declared in
	/// <c>steam_api_flat.h</c>; it was simply being skipped.
	/// </para>
	/// <para>
	/// Because this is a hand-written <c>partial</c>, re-running the generator will not
	/// delete it.
	/// </para>
	/// </remarks>
	internal unsafe partial class ISteamInput
	{
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_SetDualSenseTriggerEffect", CallingConvention = Platform.CC )]
		private static extern void _SetDualSenseTriggerEffect( IntPtr self, InputHandle_t inputHandle, ref ScePadTriggerEffectParam pParam );

		/// <summary>
		/// Applies an adaptive-trigger effect to a DualSense controller.
		/// Silently does nothing for any other controller type.
		/// </summary>
		internal void SetDualSenseTriggerEffect( InputHandle_t inputHandle, ref ScePadTriggerEffectParam param )
		{
			_SetDualSenseTriggerEffect( Self, inputHandle, ref param );
		}
	}
}
