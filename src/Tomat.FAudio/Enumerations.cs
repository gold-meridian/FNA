namespace FAudio;

public enum FAudioDeviceRole
{
	NotDefaultDevice            = 0x0,
	DefaultConsoleDevice        = 0x1,
	DefaultMultimediaDevice     = 0x2,
	DefaultCommunicationsDevice = 0x4,
	DefaultGameDevice           = 0x8,
	GlobalDefaultDevice         = 0xF,
	InvalidDeviceRole           = ~GlobalDefaultDevice,
}

public enum FAudioFilterType
{
	LowPassFilter,
	BandPassFilter,
	HighPassFilter,
	NotchFilter,
}

public enum FAudioStreamCategory
{
	Other,
	ForegroundOnlyMedia,
	BackgroundCapableMedia,
	Communications,
	Alerts,
	SoundEffects,
	GameEffects,
	GameMedia,
	GameChat,
	Speech,
	Movie,
	Media,
}

public enum Processor : uint
{
	DefaultProcessor = 0xFFFFFFFF,
}
