using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// Which of the DualSense's two adaptive triggers an effect applies to.
	/// </summary>
	/// <remarks>
	/// These are the shoulder triggers on a PlayStation 5 controller. "L2" is the left
	/// trigger and "R2" is the right one — that is Sony's naming, and it is what the
	/// underlying API uses.
	/// </remarks>
	[Flags]
	public enum DualSenseTrigger : byte
	{
		/// <summary>Neither trigger. Sending an effect with this mask does nothing.</summary>
		None = 0x00,

		/// <summary>The left shoulder trigger (L2).</summary>
		Left = 0x01,

		/// <summary>The right shoulder trigger (R2).</summary>
		Right = 0x02,

		/// <summary>Both triggers, each receiving the same effect.</summary>
		Both = Left | Right
	}

	/// <summary>
	/// The kind of resistance a DualSense adaptive trigger produces.
	/// </summary>
	/// <remarks>
	/// You will not normally write this enum yourself — use the factory methods on
	/// <see cref="DualSenseTriggerEffect"/>, which pick the right mode and fill in the
	/// parameters for you.
	/// </remarks>
	public enum DualSenseTriggerEffectMode : int
	{
		/// <summary>No effect. The trigger moves freely, as on a normal gamepad.</summary>
		Off = 0,

		/// <summary>Constant resistance from a chosen point onwards.</summary>
		Feedback = 1,

		/// <summary>Gun-trigger feel: resistance, then a sudden release ("break").</summary>
		Weapon = 2,

		/// <summary>The trigger buzzes around a chosen point.</summary>
		Vibration = 3,

		/// <summary>Per-position resistance across all ten control points.</summary>
		MultiplePositionFeedback = 4,

		/// <summary>Resistance that ramps between two points.</summary>
		SlopeFeedback = 5,

		/// <summary>Per-position vibration amplitude across all ten control points.</summary>
		MultiplePositionVibration = 6
	}

	/// <summary>
	/// The 48-byte payload of a single trigger command. This is a C <c>union</c>: every
	/// mode reuses the same 48 bytes and interprets them differently.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Modelled with <see cref="LayoutKind.Explicit"/> and an explicit
	/// <see cref="StructLayoutAttribute.Size"/> so the managed layout matches the native
	/// union byte-for-byte. Every variant sits at offset 0, exactly as a C union does.
	/// </para>
	/// <para>
	/// The native structs each declare trailing <c>padding[]</c> bytes to reach 48. We do
	/// not declare those padding fields: <c>Size = 48</c> already reserves the space, and
	/// the CLR zero-initialises a struct, which is what the padding must contain.
	/// </para>
	/// </remarks>
	[StructLayout( LayoutKind.Explicit, Size = 48 )]
	internal unsafe struct ScePadTriggerEffectCommandData
	{
		/// <summary><c>ScePadTriggerEffectFeedbackParam</c>: position, strength.</summary>
		[FieldOffset( 0 )] public byte FeedbackPosition;
		[FieldOffset( 1 )] public byte FeedbackStrength;

		/// <summary><c>ScePadTriggerEffectWeaponParam</c>: startPosition, endPosition, strength.</summary>
		[FieldOffset( 0 )] public byte WeaponStartPosition;
		[FieldOffset( 1 )] public byte WeaponEndPosition;
		[FieldOffset( 2 )] public byte WeaponStrength;

		/// <summary><c>ScePadTriggerEffectVibrationParam</c>: position, amplitude, frequency.</summary>
		[FieldOffset( 0 )] public byte VibrationPosition;
		[FieldOffset( 1 )] public byte VibrationAmplitude;
		[FieldOffset( 2 )] public byte VibrationFrequency;

		/// <summary><c>ScePadTriggerEffectMultiplePositionFeedbackParam</c>: strength[10].</summary>
		[FieldOffset( 0 )] public fixed byte MultiPositionStrength[DualSenseTriggerEffect.ControlPointCount];

		/// <summary><c>ScePadTriggerEffectSlopeFeedbackParam</c>: start/end position and strength.</summary>
		[FieldOffset( 0 )] public byte SlopeStartPosition;
		[FieldOffset( 1 )] public byte SlopeEndPosition;
		[FieldOffset( 2 )] public byte SlopeStartStrength;
		[FieldOffset( 3 )] public byte SlopeEndStrength;

		/// <summary>
		/// <c>ScePadTriggerEffectMultiplePositionVibrationParam</c>: frequency, then
		/// amplitude[10]. Note the array starts at offset 1, after the frequency byte —
		/// unlike every other variant this one is not array-at-zero.
		/// </summary>
		[FieldOffset( 0 )] public byte MultiVibrationFrequency;
		[FieldOffset( 1 )] public fixed byte MultiVibrationAmplitude[DualSenseTriggerEffect.ControlPointCount];
	}

	/// <summary>
	/// One trigger's complete command: which mode, plus that mode's parameters.
	/// Native size is 56 bytes (4 mode + 4 padding + 48 union).
	/// </summary>
	[StructLayout( LayoutKind.Explicit, Size = 56 )]
	internal struct ScePadTriggerEffectCommand
	{
		/// <summary><c>ScePadTriggerEffectMode mode</c> — a C enum, so 4 bytes.</summary>
		[FieldOffset( 0 )] public DualSenseTriggerEffectMode Mode;

		// Offset 4..7 is the native `uint8_t padding[4]`, left implicit.

		/// <summary>The mode-specific parameters.</summary>
		[FieldOffset( 8 )] public ScePadTriggerEffectCommandData CommandData;
	}

	/// <summary>
	/// The complete argument to <c>SteamAPI_ISteamInput_SetDualSenseTriggerEffect</c>:
	/// a trigger mask plus one command per trigger.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <c>isteamdualsense.h</c> asserts <c>sizeof( ScePadTriggerEffectParam ) == 120</c>,
	/// so this managed struct must marshal to exactly 120 bytes, with
	/// <c>TriggerMask</c> at 0, <c>Left</c> at 8 and <c>Right</c> at 64. Getting this
	/// wrong corrupts memory across the P/Invoke boundary rather than failing cleanly.
	/// </para>
	/// <para>
	/// Verified via <c>Marshal.SizeOf</c> / <c>Marshal.OffsetOf</c> against those values.
	/// This belongs in the offline layout-assertion test suite; see
	/// <c>docs/audit/05-tests-and-docs.md</c>.
	/// </para>
	/// </remarks>
	[StructLayout( LayoutKind.Explicit, Size = 120 )]
	internal struct ScePadTriggerEffectParam
	{
		/// <summary>Bitmask of <see cref="DualSenseTrigger"/> selecting which commands apply.</summary>
		[FieldOffset( 0 )] public byte TriggerMask;

		// Offset 1..7 is the native `uint8_t padding[7]`, left implicit.

		/// <summary>Command for the left trigger (Sony index 0, "L2").</summary>
		[FieldOffset( 8 )] public ScePadTriggerEffectCommand Left;

		/// <summary>Command for the right trigger (Sony index 1, "R2").</summary>
		[FieldOffset( 64 )] public ScePadTriggerEffectCommand Right;
	}

	/// <summary>
	/// A haptic effect for the PlayStation 5 DualSense controller's adaptive triggers —
	/// the motorised resistance that lets a trigger feel like a gun, a bowstring, or a
	/// stiff brake pedal.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>What this actually is.</b> Inside each DualSense shoulder trigger there is a
	/// small motor with an arm that can push back against your finger. You do not control
	/// that motor directly — instead you pick one of seven <i>modes</i> and give it a few
	/// numbers, and the controller's firmware produces the sensation.
	/// </para>
	///
	/// <para>
	/// <b>Positions are 0–9, not millimetres.</b> Sony divides the trigger's travel into
	/// ten "control points" numbered 0 to 9. 0 is the resting position (trigger not
	/// pressed) and 9 is fully pressed. Every "position" argument below is one of those
	/// ten steps.
	/// </para>
	///
	/// <para>
	/// <b>Strength and amplitude are 0–8.</b> 0 means "no effect at all", which behaves
	/// the same as <see cref="Off"/>. 8 is the strongest the motor goes.
	/// </para>
	///
	/// <para>
	/// <b>Requirements.</b> This only does anything on a real DualSense connected over
	/// USB or Bluetooth with Steam Input enabled for your game. On any other controller
	/// the call is silently ignored — it is not an error, and there is no way to ask
	/// "did that work?". Check that the controller's input type is a PS5 controller if
	/// you need to branch on it.
	/// </para>
	///
	/// <para>
	/// <b>It is a state, not an event.</b> An effect stays applied until you replace it
	/// or send <see cref="Off"/>. Do not re-send the same effect every frame; set it when
	/// the game state changes (weapon equipped, bow drawn, vehicle entered) and clear it
	/// when that state ends. Always send <see cref="Off"/> when the player holsters or
	/// the game returns to a menu, otherwise the trigger stays stiff.
	/// </para>
	///
	/// <example>
	/// Give the right trigger a gun feel while a weapon is equipped, and clear it after:
	/// <code>
	/// // Resistance builds from control point 2, breaks at 7, at near-max strength.
	/// SteamInput.SetDualSenseTriggerEffect(
	///     controller,
	///     DualSenseTriggerEffect.Weapon( startPosition: 2, endPosition: 7, strength: 8 ),
	///     DualSenseTrigger.Right );
	///
	/// // ... later, when the weapon is holstered:
	/// SteamInput.SetDualSenseTriggerEffect( controller, DualSenseTriggerEffect.Off(), DualSenseTrigger.Both );
	/// </code>
	/// </example>
	/// </remarks>
	public readonly struct DualSenseTriggerEffect
	{
		/// <summary>
		/// The number of discrete positions along a trigger's travel, as defined by
		/// <c>SCE_PAD_TRIGGER_EFFECT_CONTROL_POINT_NUM</c>. Positions run from
		/// <c>0</c> (released) to <c>9</c> (fully pressed).
		/// </summary>
		public const int ControlPointCount = 10;

		/// <summary>The highest legal value for a strength or amplitude argument.</summary>
		public const byte MaxStrength = 8;

		/// <summary>The highest legal control-point index.</summary>
		public const byte MaxPosition = ControlPointCount - 1;

		internal readonly ScePadTriggerEffectCommand Command;

		private DualSenseTriggerEffect( ScePadTriggerEffectCommand command ) => Command = command;

		/// <summary>The mode this effect will apply.</summary>
		public DualSenseTriggerEffectMode Mode => Command.Mode;

		/// <summary>
		/// Turns the effect off, letting the trigger move freely again.
		/// Send this when the player holsters a weapon or returns to a menu — an effect
		/// otherwise stays applied indefinitely.
		/// </summary>
		public static DualSenseTriggerEffect Off() =>
			new DualSenseTriggerEffect( new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.Off } );

		/// <summary>
		/// Constant resistance: the trigger feels normal until <paramref name="position"/>,
		/// then pushes back with a steady force for the rest of its travel.
		/// </summary>
		/// <param name="position">Where resistance begins, <c>0</c>–<c>9</c>.</param>
		/// <param name="strength">How hard it pushes back, <c>0</c>–<c>8</c>. <c>0</c> behaves like <see cref="Off"/>.</param>
		/// <remarks>Good for a heavy brake pedal, a stiff lever, or a drawn bowstring.</remarks>
		/// <exception cref="ArgumentOutOfRangeException">An argument is outside its documented range.</exception>
		public static DualSenseTriggerEffect Feedback( byte position, byte strength )
		{
			ValidatePosition( position, nameof( position ) );
			ValidateStrength( strength, nameof( strength ) );

			var command = new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.Feedback };
			command.CommandData.FeedbackPosition = position;
			command.CommandData.FeedbackStrength = strength;
			return new DualSenseTriggerEffect( command );
		}

		/// <summary>
		/// Gun-trigger feel: resistance builds from <paramref name="startPosition"/>, and
		/// at <paramref name="endPosition"/> the trigger suddenly gives way — the "break"
		/// of a firearm trigger.
		/// </summary>
		/// <param name="startPosition">Where resistance starts, <c>2</c>–<c>7</c>.</param>
		/// <param name="endPosition">Where the trigger breaks, greater than <paramref name="startPosition"/> and at most <c>8</c>.</param>
		/// <param name="strength">Resistance before the break, <c>0</c>–<c>8</c>.</param>
		/// <remarks>
		/// Sony's documented range for <paramref name="startPosition"/> is <c>2</c>–<c>7</c>
		/// rather than the usual <c>0</c>–<c>9</c>; values outside it do not produce a
		/// convincing break.
		/// </remarks>
		/// <exception cref="ArgumentOutOfRangeException">An argument is outside its documented range.</exception>
		public static DualSenseTriggerEffect Weapon( byte startPosition, byte endPosition, byte strength )
		{
			if ( startPosition < 2 || startPosition > 7 )
				throw new ArgumentOutOfRangeException( nameof( startPosition ), startPosition, "Weapon startPosition must be between 2 and 7." );

			if ( endPosition <= startPosition || endPosition > MaxStrength )
				throw new ArgumentOutOfRangeException( nameof( endPosition ), endPosition, $"Weapon endPosition must be greater than startPosition ({startPosition}) and at most {MaxStrength}." );

			ValidateStrength( strength, nameof( strength ) );

			var command = new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.Weapon };
			command.CommandData.WeaponStartPosition = startPosition;
			command.CommandData.WeaponEndPosition = endPosition;
			command.CommandData.WeaponStrength = strength;
			return new DualSenseTriggerEffect( command );
		}

		/// <summary>
		/// Buzzes the trigger around <paramref name="position"/>.
		/// </summary>
		/// <param name="position">Where the vibration is centred, <c>0</c>–<c>9</c>.</param>
		/// <param name="amplitude">How far the motor moves, <c>0</c>–<c>8</c>. <c>0</c> behaves like <see cref="Off"/>.</param>
		/// <param name="frequency">Vibration rate in hertz, <c>0</c>–<c>255</c>. <c>0</c> behaves like <see cref="Off"/>.</param>
		/// <remarks>Good for an engine idling, a chainsaw, or a machine gun's cyclic rate.</remarks>
		/// <exception cref="ArgumentOutOfRangeException">An argument is outside its documented range.</exception>
		public static DualSenseTriggerEffect Vibration( byte position, byte amplitude, byte frequency )
		{
			ValidatePosition( position, nameof( position ) );
			ValidateStrength( amplitude, nameof( amplitude ) );

			var command = new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.Vibration };
			command.CommandData.VibrationPosition = position;
			command.CommandData.VibrationAmplitude = amplitude;
			command.CommandData.VibrationFrequency = frequency;   // full 0-255 range is legal
			return new DualSenseTriggerEffect( command );
		}

		/// <summary>
		/// Sets the resistance independently at each of the ten control points, giving a
		/// fully custom resistance curve.
		/// </summary>
		/// <param name="strengths">
		/// Exactly <see cref="ControlPointCount"/> values, each <c>0</c>–<c>8</c>.
		/// <c>strengths[0]</c> is the resistance at the released position,
		/// <c>strengths[9]</c> at fully pressed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="strengths"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="strengths"/> is not exactly ten elements long.</exception>
		/// <exception cref="ArgumentOutOfRangeException">An element is greater than <see cref="MaxStrength"/>.</exception>
		public static unsafe DualSenseTriggerEffect MultiplePositionFeedback( byte[] strengths )
		{
			ValidateCurve( strengths, nameof( strengths ) );

			var command = new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.MultiplePositionFeedback };
			for ( int i = 0; i < ControlPointCount; i++ )
				command.CommandData.MultiPositionStrength[i] = strengths[i];

			return new DualSenseTriggerEffect( command );
		}

		/// <summary>
		/// Resistance that ramps linearly from one control point to another — for example
		/// getting progressively harder to squeeze as a bow is drawn.
		/// </summary>
		/// <param name="startPosition">Where the ramp begins, <c>0</c>–<c>9</c>.</param>
		/// <param name="endPosition">Where the ramp ends, greater than <paramref name="startPosition"/> and at most <c>9</c>.</param>
		/// <param name="startStrength">Resistance at the start of the ramp, <c>1</c>–<c>8</c>.</param>
		/// <param name="endStrength">Resistance at the end of the ramp, <c>1</c>–<c>8</c>.</param>
		/// <remarks>
		/// Unlike the other modes, Sony documents the strengths here as <c>1</c>–<c>8</c>:
		/// <c>0</c> is not a meaningful ramp endpoint.
		/// </remarks>
		/// <exception cref="ArgumentOutOfRangeException">An argument is outside its documented range.</exception>
		public static DualSenseTriggerEffect SlopeFeedback( byte startPosition, byte endPosition, byte startStrength, byte endStrength )
		{
			ValidatePosition( startPosition, nameof( startPosition ) );

			if ( endPosition <= startPosition || endPosition > MaxPosition )
				throw new ArgumentOutOfRangeException( nameof( endPosition ), endPosition, $"SlopeFeedback endPosition must be greater than startPosition ({startPosition}) and at most {MaxPosition}." );

			if ( startStrength < 1 || startStrength > MaxStrength )
				throw new ArgumentOutOfRangeException( nameof( startStrength ), startStrength, $"SlopeFeedback startStrength must be between 1 and {MaxStrength}." );

			if ( endStrength < 1 || endStrength > MaxStrength )
				throw new ArgumentOutOfRangeException( nameof( endStrength ), endStrength, $"SlopeFeedback endStrength must be between 1 and {MaxStrength}." );

			var command = new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.SlopeFeedback };
			command.CommandData.SlopeStartPosition = startPosition;
			command.CommandData.SlopeEndPosition = endPosition;
			command.CommandData.SlopeStartStrength = startStrength;
			command.CommandData.SlopeEndStrength = endStrength;
			return new DualSenseTriggerEffect( command );
		}

		/// <summary>
		/// Vibrates the trigger with a different amplitude at each of the ten control
		/// points, all at one shared frequency.
		/// </summary>
		/// <param name="frequency">Vibration rate in hertz, <c>0</c>–<c>255</c>. <c>0</c> behaves like <see cref="Off"/>.</param>
		/// <param name="amplitudes">
		/// Exactly <see cref="ControlPointCount"/> values, each <c>0</c>–<c>8</c>.
		/// <c>amplitudes[0]</c> applies at the released position.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="amplitudes"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="amplitudes"/> is not exactly ten elements long.</exception>
		/// <exception cref="ArgumentOutOfRangeException">An element is greater than <see cref="MaxStrength"/>.</exception>
		public static unsafe DualSenseTriggerEffect MultiplePositionVibration( byte frequency, byte[] amplitudes )
		{
			ValidateCurve( amplitudes, nameof( amplitudes ) );

			var command = new ScePadTriggerEffectCommand { Mode = DualSenseTriggerEffectMode.MultiplePositionVibration };
			command.CommandData.MultiVibrationFrequency = frequency;
			for ( int i = 0; i < ControlPointCount; i++ )
				command.CommandData.MultiVibrationAmplitude[i] = amplitudes[i];

			return new DualSenseTriggerEffect( command );
		}

		private static void ValidatePosition( byte position, string name )
		{
			if ( position > MaxPosition )
				throw new ArgumentOutOfRangeException( name, position, $"Trigger positions must be between 0 and {MaxPosition}." );
		}

		private static void ValidateStrength( byte strength, string name )
		{
			if ( strength > MaxStrength )
				throw new ArgumentOutOfRangeException( name, strength, $"Strength and amplitude must be between 0 and {MaxStrength}." );
		}

		private static void ValidateCurve( byte[] values, string name )
		{
			if ( values == null )
				throw new ArgumentNullException( name );

			if ( values.Length != ControlPointCount )
				throw new ArgumentException( $"Expected exactly {ControlPointCount} values, one per trigger control point, but got {values.Length}.", name );

			for ( int i = 0; i < values.Length; i++ )
			{
				if ( values[i] > MaxStrength )
					throw new ArgumentOutOfRangeException( name, values[i], $"{name}[{i}] must be between 0 and {MaxStrength}." );
			}
		}
	}
}
