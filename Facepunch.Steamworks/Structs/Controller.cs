using Steamworks.Data;

namespace Steamworks
{
	public struct Controller
	{
		private InputHandle_t inputHandle;

		internal Controller( InputHandle_t inputHandle_t )
		{
			this.inputHandle = inputHandle_t;
		}

		public InputType InputType => SteamInput.Internal.GetInputTypeForHandle( inputHandle );

		public override string ToString() => $"{InputType}.{inputHandle.Value}";
	}
}