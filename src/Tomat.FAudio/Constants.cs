namespace FAudio;

public static class Constants
{
#region FAudio
	// Targeting compatibility with XAudio 2.8.
	public const uint FAUDIO_TARGET_VERSION = 8;

	public const uint FAUDIO_ABI_VERSION   = 0;
	public const uint FAUDIO_MAJOR_VERSION = 24;
	public const uint FAUDIO_MINOR_VERSION = 11;
	public const uint FAUDIO_PATCH_VERSION = 0;

	public const uint FAUDIO_COMPILED_VERSION = FAUDIO_ABI_VERSION   * 100 * 100 * 100
	                                          + FAUDIO_MAJOR_VERSION * 100 * 100
	                                          + FAUDIO_MINOR_VERSION * 100
	                                          + FAUDIO_PATCH_VERSION;

	public const uint FAUDIO_E_OUT_OF_MEMORY      = 0x8007000e;
	public const uint FAUDIO_E_INVALID_ARG        = 0x80070057;
	public const uint FAUDIO_E_UNSUPPORTED_FORMAT = 0x88890008;
	public const uint FAUDIO_E_INVALID_CALL       = 0x88960001;
	public const uint FAUDIO_E_DEVICE_INVALIDATED = 0x88960004;
	public const uint FAPO_E_FORMAT_UNSUPPORTED   = 0x88970001;

	public const uint  FAUDIO_MAX_BUFFER_BYTES     = 0x80000000;
	public const uint  FAUDIO_MAX_QUEUED_BUFFERS   = 64;
	public const uint  FAUDIO_MAX_AUDIO_CHANNELS   = 64;
	public const uint  FAUDIO_MIN_SAMPLE_RATE      = 1000;
	public const uint  FAUDIO_MAX_SAMPLE_RATE      = 200000;
	public const float FAUDIO_MAX_VOLUME_LEVEL     = 16777216.0f;
	public const float FAUDIO_MIN_FREQ_RATIO       = (1.0f / 1024.0f);
	public const float FAUDIO_MAX_FREQ_RATIO       = 1024.0f;
	public const float FAUDIO_DEFAULT_FREQ_RATIO   = 2.0f;
	public const float FAUDIO_MAX_FILTER_ONEOVERQ  = 1.5f;
	public const float FAUDIO_MAX_FILTER_FREQUENCY = 1.0f;
	public const uint  FAUDIO_MAX_LOOP_COUNT       = 254;

	public const uint FAUDIO_COMMIT_NOW         = 0;
	public const uint FAUDIO_COMMIT_ALL         = 0;
	public const uint FAUDIO_INVALID_OPSET      = 0xFFFFFFFFU; // unchecked((uint) -1)
	public const uint FAUDIO_NO_LOOP_REGION     = 0;
	public const uint FAUDIO_LOOP_INFINITE      = 255;
	public const uint FAUDIO_DEFAULT_CHANNELS   = 0;
	public const uint FAUDIO_DEFAULT_SAMPLERATE = 0;

	public const uint FAUDIO_DEBUG_ENGINE          = 0x0001;
	public const uint FAUDIO_VOICE_NOPITCH         = 0x0002;
	public const uint FAUDIO_VOICE_NOSRC           = 0x0004;
	public const uint FAUDIO_VOICE_USEFILTER       = 0x0008;
	public const uint FAUDIO_VOICE_MUSIC           = 0x0010;
	public const uint FAUDIO_PLAY_TAILS            = 0x0020;
	public const uint FAUDIO_END_OF_STREAM         = 0x0040;
	public const uint FAUDIO_SEND_USEFILTER        = 0x0080;
	public const uint FAUDIO_VOICE_NOSAMPLESPLAYED = 0x0100;
	public const uint FAUDIO_1024_QUANTUM          = 0x8000;

	public const FAudioFilterType FAUDIO_DEFAULT_FILTER_TYPE          = FAudioFilterType.LowPassFilter;
	public const float            FAUDIO_DEFAULT_FILTER_FREQUENCY     = FAUDIO_MAX_FILTER_FREQUENCY;
	public const float            FAUDIO_DEFAULT_FILTER_ONEOVERQ      = 1.0f;
	public const float            FAUDIO_DEFAULT_FILTER_WETDRYMIX_EXT = 1.0f;

	public const uint FAUDIO_LOG_ERRORS     = 0x0001;
	public const uint FAUDIO_LOG_WARNINGS   = 0x0002;
	public const uint FAUDIO_LOG_INFO       = 0x0004;
	public const uint FAUDIO_LOG_DETAIL     = 0x0008;
	public const uint FAUDIO_LOG_API_CALLS  = 0x0010;
	public const uint FAUDIO_LOG_FUNC_CALLS = 0x0020;
	public const uint FAUDIO_LOG_TIMING     = 0x0040;
	public const uint FAUDIO_LOG_LOCKS      = 0x0080;
	public const uint FAUDIO_LOG_MEMORY     = 0x0100;
	public const uint FAUDIO_LOG_STREAMING  = 0x1000;

	public const uint FAUDIO_FORMAT_PCM              = 1;
	public const uint FAUDIO_FORMAT_MSADPCM          = 2;
	public const uint FAUDIO_FORMAT_IEEE_FLOAT       = 3;
	public const uint FAUDIO_FORMAT_WMAUDIO2         = 0x0161;
	public const uint FAUDIO_FORMAT_WMAUDIO3         = 0x0162;
	public const uint FAUDIO_FORMAT_WMAUDIO_LOSSLESS = 0x0163;
	public const uint FAUDIO_FORMAT_XMAUDIO2         = 0x0166;
	public const uint FAUDIO_FORMAT_EXTENSIBLE       = 0xFFFE;
#endregion

#region FAudioFX
	public const uint FAUDIOFX_REVERB_MIN_FRAMERATE = 20000;
	public const uint FAUDIOFX_REVERB_MAX_FRAMERATE = 48000;

	public const float FAUDIOFX_REVERB_MIN_WET_DRY_MIX        = 0.0f;
	public const uint  FAUDIOFX_REVERB_MIN_REFLECTIONS_DELAY  = 0;
	public const uint  FAUDIOFX_REVERB_MIN_REVERB_DELAY       = 0;
	public const uint  FAUDIOFX_REVERB_MIN_REAR_DELAY         = 0;
	public const uint  FAUDIOFX_REVERB_MIN_7POINT1_SIDE_DELAY = 0;
	public const uint  FAUDIOFX_REVERB_MIN_7POINT1_REAR_DELAY = 0;
	public const uint  FAUDIOFX_REVERB_MIN_POSITION           = 0;
	public const uint  FAUDIOFX_REVERB_MIN_DIFFUSION          = 0;
	public const uint  FAUDIOFX_REVERB_MIN_LOW_EQ_GAIN        = 0;
	public const uint  FAUDIOFX_REVERB_MIN_LOW_EQ_CUTOFF      = 0;
	public const uint  FAUDIOFX_REVERB_MIN_HIGH_EQ_GAIN       = 0;
	public const uint  FAUDIOFX_REVERB_MIN_HIGH_EQ_CUTOFF     = 0;
	public const float FAUDIOFX_REVERB_MIN_ROOM_FILTER_FREQ   = 20.0f;
	public const float FAUDIOFX_REVERB_MIN_ROOM_FILTER_MAIN   = -100.0f;
	public const float FAUDIOFX_REVERB_MIN_ROOM_FILTER_HF     = -100.0f;
	public const float FAUDIOFX_REVERB_MIN_REFLECTIONS_GAIN   = -100.0f;
	public const float FAUDIOFX_REVERB_MIN_REVERB_GAIN        = -100.0f;
	public const float FAUDIOFX_REVERB_MIN_DECAY_TIME         = 0.1f;
	public const float FAUDIOFX_REVERB_MIN_DENSITY            = 0.0f;
	public const float FAUDIOFX_REVERB_MIN_ROOM_SIZE          = 0.0f;

	public const float FAUDIOFX_REVERB_MAX_WET_DRY_MIX        = 100.0f;
	public const uint  FAUDIOFX_REVERB_MAX_REFLECTIONS_DELAY  = 300;
	public const uint  FAUDIOFX_REVERB_MAX_REVERB_DELAY       = 85;
	public const uint  FAUDIOFX_REVERB_MAX_REAR_DELAY         = 5;
	public const uint  FAUDIOFX_REVERB_MAX_7POINT1_SIDE_DELAY = 5;
	public const uint  FAUDIOFX_REVERB_MAX_7POINT1_REAR_DELAY = 20;
	public const uint  FAUDIOFX_REVERB_MAX_POSITION           = 30;
	public const uint  FAUDIOFX_REVERB_MAX_DIFFUSION          = 15;
	public const uint  FAUDIOFX_REVERB_MAX_LOW_EQ_GAIN        = 12;
	public const uint  FAUDIOFX_REVERB_MAX_LOW_EQ_CUTOFF      = 9;
	public const uint  FAUDIOFX_REVERB_MAX_HIGH_EQ_GAIN       = 8;
	public const uint  FAUDIOFX_REVERB_MAX_HIGH_EQ_CUTOFF     = 14;
	public const float FAUDIOFX_REVERB_MAX_ROOM_FILTER_FREQ   = 20000.0f;
	public const float FAUDIOFX_REVERB_MAX_ROOM_FILTER_MAIN   = 0.0f;
	public const float FAUDIOFX_REVERB_MAX_ROOM_FILTER_HF     = 0.0f;
	public const float FAUDIOFX_REVERB_MAX_REFLECTIONS_GAIN   = 20.0f;
	public const float FAUDIOFX_REVERB_MAX_REVERB_GAIN        = 20.0f;
	public const float FAUDIOFX_REVERB_MAX_DENSITY            = 100.0f;
	public const float FAUDIOFX_REVERB_MAX_ROOM_SIZE          = 100.0f;

	public const float FAUDIOFX_REVERB_DEFAULT_WET_DRY_MIX        = 100.0f;
	public const uint  FAUDIOFX_REVERB_DEFAULT_REFLECTIONS_DELAY  = 5;
	public const uint  FAUDIOFX_REVERB_DEFAULT_REVERB_DELAY       = 5;
	public const uint  FAUDIOFX_REVERB_DEFAULT_REAR_DELAY         = 5;
	public const uint  FAUDIOFX_REVERB_DEFAULT_7POINT1_SIDE_DELAY = 5;
	public const uint  FAUDIOFX_REVERB_DEFAULT_7POINT1_REAR_DELAY = 20;
	public const uint  FAUDIOFX_REVERB_DEFAULT_POSITION           = 6;
	public const uint  FAUDIOFX_REVERB_DEFAULT_POSITION_MATRIX    = 27;
	public const uint  FAUDIOFX_REVERB_DEFAULT_EARLY_DIFFUSION    = 8;
	public const uint  FAUDIOFX_REVERB_DEFAULT_LATE_DIFFUSION     = 8;
	public const uint  FAUDIOFX_REVERB_DEFAULT_LOW_EQ_GAIN        = 8;
	public const uint  FAUDIOFX_REVERB_DEFAULT_LOW_EQ_CUTOFF      = 4;
	public const uint  FAUDIOFX_REVERB_DEFAULT_HIGH_EQ_GAIN       = 8;
	public const uint  FAUDIOFX_REVERB_DEFAULT_HIGH_EQ_CUTOFF     = 4;
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_FILTER_FREQ   = 5000.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_FILTER_MAIN   = 0.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_FILTER_HF     = 0.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_REFLECTIONS_GAIN   = 0.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_REVERB_GAIN        = 0.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_DECAY_TIME         = 1.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_DENSITY            = 100.0f;
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_SIZE          = 100.0f;
#endregion
}
