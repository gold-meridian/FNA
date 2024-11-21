using System.Runtime.InteropServices;

namespace FAudio;

#region FAudio
public struct FAudio;

public struct FAudioVoice;

public unsafe struct FAudioEngineCallback
{
	/// <summary>
	///		If something horrible happens, this will be called.
	///		<br />
	///		<c>error</c>:  The error code that spawned this callback.
	/// </summary>
	public delegate* unmanaged<FAudioEngineCallback*, uint, void> OnCriticalError { get; set; }

	/// <summary>
	///		This is called at the end of a processing update.
	/// </summary>
	public delegate* unmanaged<FAudioEngineCallback*, void> OnProcessingPassStart { get; set; }

	/// <summary>
	///		This is called at the beginning of a processing update.
	/// </summary>
	public delegate* unmanaged<FAudioEngineCallback*, void> OnProcessingPassEnd { get; set; }
}

public unsafe struct FAudioVoiceCallback
{
	/// <summary>
	///		When a buffer is no longer in use, this is called.
	///		<br />
	///		<c>bufferContext</c>:  The context for the
	///		<see cref="FAudioBuffer"/> in question.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, void*, void> OnBufferEnd { get; set; }

	/// <summary>
	///		When a buffer is now being used, this is called.
	///		<br />
	///		<c>bufferContext</c>:  The context for the
	///		<see cref="FAudioBuffer"/> in question.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, void*, void> OnBufferStart { get; set; }

	/// <summary>
	///		When a buffer completes a loop, this is called.
	///		<br />
	///		<c>bufferContext</c>:  The context for the
	///		<see cref="FAudioBuffer"/> in question.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, void*, void> OnLoopEnd { get; set; }

	/// <summary>
	///		Whe a buffer that has the
	///		<see cref="Constants.FAUDIO_END_OF_STREAM"/> flag is finished, this
	///		is called.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, void> OnStreamEnd { get; set; }

	/// <summary>
	///		If something horrible happens to a voice, this is called.
	///		<br />
	///		<c>bufferContext</c>:  The context for the
	///		<see cref="FAudioBuffer"/> in question.
	///		<br />
	///		<c>error</c>:  The error code that spawned this callback.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, void*, uint, void> OnVoiceError { get; set; }

	/// <summary>
	///		When this voice is done being processed, this is called.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, void> OnVoiceProcessingPassEnd { get; set; }

	/// <summary>
	///		When a voice is about to start being processed, this is called.
	///		<br />
	///		<c>bytesRequested</c>:  The number of bytes needed from the
	///		application to complete a full update.  For example, if we need
	///		<c>512</c> frames for a whole update, and the voice is a
	///		<see langword="float"/> stereo source, <c>bytesRequested</c> will be
	///		<c>4096</c>.
	/// </summary>
	public delegate* unmanaged<FAudioVoiceCallback*, uint, void> OnVoiceProcessingPassStart { get; set; }
}

public struct Fapo;

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
#endregion

#region FAudioFX
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct FAudioFxVolumeMeterLevels
{
	public float* PeakLevels { get; set; }

	public float* RmsLevels { get; set; }

	public uint ChannelCount { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioFxReverbParameters
{
	public float WetDryMix { get; set; }

	public uint ReflectionsDelay { get; set; }

	public byte ReverbDelay { get; set; }

	public byte RearDelay { get; set; }

	public byte PositionLeft { get; set; }

	public byte PositionRight { get; set; }

	public byte PositionMatrixLeft { get; set; }

	public byte PositionMatrixRight { get; set; }

	public byte EarlyDiffusion { get; set; }

	public byte LateDiffusion { get; set; }

	public byte LowEqGain { get; set; }

	public byte LowEqCutoff { get; set; }

	public byte HighEqGain { get; set; }

	public byte HighEqCutoff { get; set; }

	public float RoomFilterFreq { get; set; }

	public float RoomFilterMain { get; set; }

	public float RoomFilterHf { get; set; }

	public float ReflectionsGain { get; set; }

	public float ReverbGain { get; set; }

	public float DecayTime { get; set; }

	public float Density { get; set; }

	public float RoomSize { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioFxReverbParameters9
{
	public float WetDryMix { get; set; }

	public uint ReflectionsDelay { get; set; }

	public byte ReverbDelay { get; set; }

	public byte RearDelay { get; set; }

	public byte SideDelay { get; set; }

	public byte PositionLeft { get; set; }

	public byte PositionRight { get; set; }

	public byte PositionMatrixLeft { get; set; }

	public byte PositionMatrixRight { get; set; }

	public byte EarlyDiffusion { get; set; }

	public byte LateDiffusion { get; set; }

	public byte LowEqGain { get; set; }

	public byte LowEqCutoff { get; set; }

	public byte HighEqGain { get; set; }

	public byte HighEqCutoff { get; set; }

	public float RoomFilterFreq { get; set; }

	public float RoomFilterMain { get; set; }

	public float RoomFilterHf { get; set; }

	public float ReflectionsGain { get; set; }

	public float ReverbGain { get; set; }

	public float DecayTime { get; set; }

	public float Density { get; set; }

	public float RoomSize { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FAudioFxReverbI3Dl2Parameters
{
	public float WetDryMix { get; set; }

	public int Room { get; set; }

	public int RoomHf { get; set; }

	public float RoomRolloffFactor { get; set; }

	public float DecayTime { get; set; }

	public float DecayHfRatio { get; set; }

	public int Reflections { get; set; }

	public float ReflectionsDelay { get; set; }

	public int Reverb { get; set; }

	public float ReverbDelay { get; set; }

	public float Diffusion { get; set; }

	public float Density { get; set; }

	public float HfReference { get; set; }
}
#endregion

#region FAPOBase
// TODO
public struct FapoBase;
#endregion
