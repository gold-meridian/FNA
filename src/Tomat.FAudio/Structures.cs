using System.Runtime.InteropServices;

using wchar_t = short;

namespace FAudio;

#region Forward-declared structs
public struct FAudioVoice;

public struct Fapo;
#endregion

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioGuid
{
	public uint Data1 { get; set; }

	public ushort Data2 { get; set; }

	public ushort Data3 { get; set; }

	public fixed byte Data4[8];
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioWaveFormatEx
{
	public ushort FormatTag { get; set; }

	public ushort ChannelCount { get; set; }

	public uint SamplesPerSecCount { get; set; }

	public uint AvgBytesPerSecCount { get; set; }

	public ushort BlockAlignCount { get; set; }

	public ushort BitsPerSampleCount { get; set; }

	public ushort CbSize { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioWaveFormatExtensible
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = sizeof(ushort))]
	public struct SamplesUnion
	{
		[field: FieldOffset(0)]
		public ushort ValidBitsPerSample { get; set; }

		[field: FieldOffset(0)]
		public ushort SamplesPerBlock { get; set; }

		[field: FieldOffset(0)]
		public ushort Reserved { get; set; }
	}

	public FAudioWaveFormatEx Format { get; set; }

	public SamplesUnion Samples { get; set; }

	public uint ChannelMask { get; set; }

	public FAudioGuid SubFormat { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioAdpcmCoefSet
{
	public short Coef1 { get; set; }

	public short Coef2 { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioAdpcmWaveFormat
{
	public FAudioWaveFormatEx Wfx { get; set; }

	public ushort SamplesPerBlock { get; set; }

	public ushort CoefCount { get; set; }

	/* MSADPCM has 7 coefficient pairs:
	 * {
	 *	{ 256,    0 },
	 *	{ 512, -256 },
	 *	{   0,    0 },
	 *	{ 192,   64 },
	 *	{ 240,    0 },
	 *	{ 460, -208 },
	 *	{ 392, -232 }
	 * }
	 */
	public FAudioAdpcmCoefSet* Coefs { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioDeviceDetails
{
	public fixed wchar_t DeviceId[256];

	public fixed wchar_t DisplayName[256];

	public FAudioDeviceRole Role { get; set; }

	public FAudioWaveFormatExtensible OutputFormat { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioVoiceDetails
{
	public uint CreationFlags { get; set; }

	public uint ActiveFlags { get; set; }

	public uint InputChannels { get; set; }

	public uint InputSampleRate { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioSendDescriptor
{
	/// <summary>
	///		<c>0</c> or <see cref="Constants.FAUDIO_SEND_USEFILTER"/>.
	/// </summary>
	public uint Flags { get; set; }

	public FAudioVoice* OutputVoice { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioVoiceSends
{
	public uint SendCount { get; set; }

	public FAudioSendDescriptor* Sends { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioEffectDescriptor
{
	public Fapo* Effect { get; set; }

	/// <summary>
	///		<c>1</c> - Enabled, <c>0</c> - Disabled.
	/// </summary>
	/// <remarks>
	///		Treat as a <see langword="bool"/>.
	/// </remarks>
	public int InitialState { get; set; }

	public uint OutputChannels { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioEffectChain
{
	public uint EffectCount { get; set; }

	public FAudioEffectDescriptor* EffectDescriptors { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioFilterParameters
{
	public FAudioFilterType FilterType { get; set; }

	/// <summary>
	///		Range: [<c>0</c>, <see cref="Constants.FAUDIO_MAX_FILTER_FREQUENCY"/>].
	/// </summary>
	public float Frequency { get; set; }

	/// <summary>
	///		Range: [<c>0</c>, <see cref="Constants.FAUDIO_MAX_FILTER_ONEOVERQ"/>].
	/// </summary>
	public float OneOverQ { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioFilterParametersExt
{
	public FAudioFilterType Type { get; set; }

	/// <summary>
	///		Range: [<c>0</c>, <see cref="Constants.FAUDIO_MAX_FILTER_FREQUENCY"/>].
	/// </summary>
	public float Frequency { get; set; }

	/// <summary>
	///		Range: [<c>0</c>, <see cref="Constants.FAUDIO_MAX_FILTER_ONEOVERQ"/>].
	/// </summary>
	public float OneOverQ { get; set; }

	/// <summary>
	///		Range: [<c>0</c>, <c>1</c>].
	/// </summary>
	public float WetDryMix { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioBuffer
{
	/// <summary>
	///		Either <c>0</c> or <see cref="Constants.FAUDIO_END_OF_STREAM"/>.
	/// </summary>
	public uint Flags { get; set; }

	/// <summary>
	///		Pointer to wave data, memory block size.
	/// </summary>
	/// <remarks>
	///		Note that pAudioData is not copied; FAudio reads directly from your
	///		pointer!  This pointer must be valid until FAudio has finished using
	///		it, at which point an OnBufferEnd callback will be generated.
	/// </remarks>
	public uint AudioBytes { get; set; }

	public byte* AudioData { get; set; }

	/// <summary>
	///		Play region, in sample frames.
	/// </summary>
	public uint PlayBegin { get; set; }

	public uint PlayLength { get; set; }

	/// <summary>
	///		Loop region, in sample frames.
	/// </summary>
	/// <remarks>
	///		This can be used to loop a subregion of the wave instead of looping
	///		the whole thing, i.e. if you have an intro/outro you can set these
	///		to loop the middle sections instead.  If you don't need this, set
	///		both values to 0.
	/// </remarks>
	public uint LoopBegin { get; set; }

	public uint LoopLength { get; set; }

	/// <summary>
	///		Range: [<c>0</c>, <see cref="Constants.FAUDIO_LOOP_INFINITE"/>].
	/// </summary>
	public uint LoopCount { get; set; }

	/// <summary>
	///		This is sent to callbacks as <c>BufferContent</c>.
	/// </summary>
	public void* Context { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioBufferWma
{
	public uint* DecodedPacketCumulativeBytes { get; set; }

	public uint PacketCount { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioVoiceState
{
	public void* CurrentBufferContext { get; set; }

	public uint BuffersQueued { get; set; }

	public ulong SamplesPlayed { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioPerformanceData
{
	public ulong AudioCyclesSinceLastQuery { get; set; }

	public ulong TotalCyclesSinceLastQuery { get; set; }

	public uint MinimumCyclesPerQuantum { get; set; }

	public uint MaximumCyclesPerQuantum { get; set; }

	public uint MemoryUsageInBytes { get; set; }

	public uint CurrentLatencyInSamples { get; set; }

	public uint GlitchesSinceEngineStarted { get; set; }

	public uint ActiveSourceVoiceCount { get; set; }

	public uint TotalSourceVoiceCount { get; set; }

	public uint ActiveSubmixVoiceCount { get; set; }

	public uint ActiveResamplerCount { get; set; }

	public uint ActiveMatrixMixCount { get; set; }

	public uint ActiveXmaSourceVoices { get; set; }

	public uint ActiveXmaStreams { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioDebugConfiguration
{
	/// <seealso cref="Constants.FAUDIO_LOG_ERRORS"/>
	/// <seealso cref="Constants.FAUDIO_LOG_WARNINGS"/>
	/// <seealso cref="Constants.FAUDIO_LOG_INFO"/>
	/// <seealso cref="Constants.FAUDIO_LOG_DETAIL"/>
	/// <seealso cref="Constants.FAUDIO_LOG_API_CALLS"/>
	/// <seealso cref="Constants.FAUDIO_LOG_FUNC_CALLS"/>
	/// <seealso cref="Constants.FAUDIO_LOG_TIMING"/>
	/// <seealso cref="Constants.FAUDIO_LOG_LOCKS"/>
	/// <seealso cref="Constants.FAUDIO_LOG_MEMORY"/>
	public uint TraceMask { get; set; }

	public uint BreakMask { get; set; }

	/// <summary>
	///		<c>0</c> or <c>1</c>.
	/// </summary>
	public int LogThreadId { get; set; }

	public int LogFileLine { get; set; }

	public int LogFunctionName { get; set; }

	public int LogTiming { get; set; }
}

// NOTE: Explicitly not packed.
public struct FAudioXma2WaveFormatEx
{
	public FAudioWaveFormatEx Wfx { get; set; }

	public ushort StreamCount { get; set; }

	public uint ChannelMask { get; set; }

	public uint SamplesEncoded { get; set; }

	public uint BytesPerBlock { get; set; }

	public uint PlayBegin { get; set; }

	public uint PlayLength { get; set; }

	public uint LoopBegin { get; set; }

	public uint LoopLength { get; set; }

	public byte LoopCount { get; set; }

	public byte EncoderVersion { get; set; }

	public ushort BlockCount { get; set; }
}
