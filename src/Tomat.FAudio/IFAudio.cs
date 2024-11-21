using System;

namespace FAudio;

/// <summary>
///		Represents an FAudio implementation.
/// </summary>
public unsafe interface IFAudio
{
#region Version
	uint LinkedVersion();
#endregion

#region FAudio Interface
	/// <summary>
	///		This should be your first FAudio call.
	/// </summary>
	/// <param name="audio">The FAudio core context.</param>
	/// <param name="flags">
	///		Can be <c>0</c> or <see cref="Constants.FAUDIO_DEBUG_ENGINE"/>.
	/// </param>
	/// <param name="xaudio2Processor">
	///		Set this to <see cref="FAudioProcessor.DefaultProcessor"/>.
	/// </param>
	/// <returns><c>0</c> on success.</returns>
	uint Create(
		out FAudio*     audio,
		uint            flags,
		FAudioProcessor xaudio2Processor
	);

	/// <summary>
	///		TODO See "extensions/COMConstructEXT.txt" for more details
	/// </summary>
	/// <param name="faudio"></param>
	/// <param name="version"></param>
	/// <returns></returns>
	uint ComConstructExt(
		out FAudio* faudio,
		byte        version
	);

	/// <summary>
	///		Increments a reference counter.  When the counter is <c>0</c>,
	///		<paramref name="audio"/> is freed.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <returns>Returns the reference count after incrementing.</returns>
	uint AddRef(FAudio* audio);

	/// <summary>
	///		Decrements a reference counter.  When the counter is <c>0</c>,
	///		<paramref name="audio"/> is freed.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <returns>Returns the reference count after decrementing.</returns>
	uint Release(FAudio* audio);

	/// <summary>
	///		Queries the number of sound devices available for use.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="count">The number of available sound devices.</param>
	/// <returns><c>0</c> on success.</returns>
	uint GetDeviceCount(
		FAudio*  audio,
		out uint count
	);

	/// <summary>
	///		Gets basic information about a sound device.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="index">
	///		Can be between <c>0</c> and the result of
	///		<see cref="GetDeviceCount"/>.
	/// </param>
	/// <param name="deviceDetails">The device information.</param>
	/// <returns><c>0</c> on success.</returns>
	uint GetDeviceDetails(
		FAudio*                 audio,
		uint                    index,
		out FAudioDeviceDetails deviceDetails
	);

	/// <summary>
	///		You don't actually have to call this, unless you're using the COM
	///		APIs.  See the FAudioCreate API for parameter information.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="flags"></param>
	/// <param name="xaudio2Processor"></param>
	/// <returns></returns>
	uint Initialize(
		FAudio*         audio,
		uint            flags,
		FAudioProcessor xaudio2Processor
	);

	/// <summary>
	///		Register a new set of engine callbacks.
	///		<br />
	///		There is no limit to the number of sets, but expect performance to
	///		degrade if you have a bunch of these.  You most likely only need
	///		one.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="callback">
	///		The completely-initialized <see cref="FAudioEngineCallback"/>
	///		structure.
	/// </param>
	/// <returns><c>0</c> on success.</returns>
	uint RegisterForCallbacks(
		FAudio*               audio,
		FAudioEngineCallback* callback
	);

	/// <summary>
	///		Remove an active set of engine callbacks.
	///		<br />
	///		This checks the pointer value, NOT the callback values!
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="callback">
	///		An <see cref="FAudioEngineCallback"/> structure previously sent to
	///		<see cref="RegisterForCallbacks"/>.
	/// </param>
	void UnregisterForCallbacks(
		FAudio*               audio,
		FAudioEngineCallback* callback
	);

	/// <summary>
	///		Creates a "source" voice, used to play back wavedata.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="sourceVoice">Filled with the source voice pointer.</param>
	/// <param name="sourceFormat">
	///		The input wavedata format, see the documentation for
	///		<see cref="FAudioWaveFormatEx"/>.
	/// </param>
	/// <param name="flags">
	///		Can be <c>0</c> or a mix of the following <c>FAUDIO_VOICE_*</c>
	///		flags:
	///		<br />
	///		<c>NOPITCH/NOSRC</c>: Resampling is disabled.  If you set this, the
	///		source format sample rate MUST match the output voices' input sample
	///		rates. Also, SetFrequencyRatio will fail.
	///		<br />
	///		<c>USEFILTER</c>: Enables the use of
	///		<see cref="SetFilterParameters"/>.
	///		<br />
	///		<c>MUSIC</c>: Unsupported.
	/// </param>
	/// <param name="maxFrequencyRatio">
	///		AKA your max pitch. This allows us to optimize the size of the
	///		decode/resample cache sizes.  For example, if you only expect to
	///		raise pitch by a single octave, you can set this value to 2.0f. 2.0f
	///		is the default value.
	///		<br />
	///		Bounds: [<see cref="Constants.FAUDIO_MIN_FREQ_RATIO"/>,
	///		<see cref="Constants.FAUDIO_MAX_FREQ_RATIO"/>].
	/// </param>
	/// <param name="callback">
	///		Voice callbacks, see FAudioVoiceCallback documentation.
	/// </param>
	/// <param name="sendList">
	///		List of output voices. If <see langword="null"/>, defaults to master.  All output
	///		voices must have the same sample rate!
	/// </param>
	/// <param name="effectChain">
	///		List of FAPO effects.  This value can be <see langword="null"/>.
	/// </param>
	/// <returns></returns>
	uint CreateSourceVoice(
		FAudio*                audio,
		out FAudioSourceVoice* sourceVoice,
		ref FAudioWaveFormatEx sourceFormat,
		uint                   flags,
		float                  maxFrequencyRatio,
		FAudioVoiceCallback*   callback,
		FAudioVoiceSends*      sendList,
		FAudioEffectChain*     effectChain
	);

	/// <summary>
	///		Creates a "submix" voice, used to mix/process input voices. The
	///		typical use case for this is to perform CPU-intensive tasks on large
	///		groups of voices all at once.  Examples include resampling and FAPO
	///		effects.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="submixVoice">Filled with the submix voice pointer.</param>
	/// <param name="inputChannels">
	///		Input voices will convert to this channel count.
	/// </param>
	/// <param name="inputSampleRate">
	///		Input voices will convert to this sample rate.
	/// </param>
	/// <param name="flags">
	///		Can be <c>0</c> or <see cref="Constants.FAUDIO_VOICE_USEFILTER"/>.
	/// </param>
	/// <param name="processingStage">
	///		If you have multiple submixes that depend on a specific order of
	///		processing, you can sort them by setting this value to prioritize
	///		them.  For example, submixes with stage 0 will process first, then
	///		stage 1, 2, and so on.
	/// </param>
	/// <param name="sendList">
	///		List of output voices. If <see langword="null"/>, defaults to
	///		master.  All output voices must have the same sample rate!
	/// </param>
	/// <param name="effectChain">
	///		List of FAPO effects. This value can be <see langword="null"/>.
	/// </param>
	/// <returns><c>0</c> on success.</returns>
	uint CreateSubmixVoice(
		FAudio*                audio,
		out FAudioSubmixVoice* submixVoice,
		uint                   inputChannels,
		uint                   inputSampleRate,
		uint                   flags,
		uint                   processingStage,
		FAudioVoiceSends*      sendList,
		FAudioEffectChain*     effectChain
	);

	/// <summary>
	///		This should be your second FAudio call, unless you care about which
	///		device you want to use. In that case, see
	///		<see cref="GetDeviceDetails"/>.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="masteringVoice">
	///		Filled with the mastering voice pointer.
	/// </param>
	/// <param name="inputChannels">
	///		Device channel count.  Can be
	///		<see cref="Constants.FAUDIO_DEFAULT_CHANNELS"/>.
	/// </param>
	/// <param name="inputSampleRate">
	///		Device sample rate.  Can be
	///		<see cref="Constants.FAUDIO_DEFAULT_SAMPLERATE"/>.
	/// </param>
	/// <param name="flags">This value must be <c>0</c>.</param>
	/// <param name="deviceIndex">
	///		<c>0</c> for the default device.  See <see cref="GetDeviceCount"/>.
	/// </param>
	/// <param name="effectChain">
	///		List of FAPO effects.  This value can be <see langword="null"/>.
	/// </param>
	/// <returns><c>0</c> on success.</returns>
	uint CreateMasteringVoice(
		FAudio*                   audio,
		out FAudioMasteringVoice* masteringVoice,
		uint                      inputChannels,
		uint                      inputSampleRate,
		uint                      flags,
		uint                      deviceIndex,
		FAudioEffectChain*        effectChain
	);

	/// <summary>
	///		Starts the engine, begins processing the audio graph.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <returns><c>0</c> on success.</returns>
	uint StartEngine(FAudio* audio);

	/// <summary>
	///		Stops the engine and halts all processing.
	///		<br />
	///		The audio device will continue to run, but will produce silence.
	///		The graph will be frozen until you call StartEngine, where it will
	///		then resume all processing exactly as it would have had this never
	///		been called.
	/// </summary>
	/// <param name="audio">The audio.</param>
	void StopEngine(FAudio* audio);

	/// <summary>
	///		Flushes a batch of FAudio calls compiled with a given
	///		<paramref name="operationSet"/> tag.  This function is based on
	///		<c>IXAudio2::CommitChanges</c> from the XAudio2 spec.  This is
	///		useful for pushing calls that need to be done perfectly in sync.
	///		For example, if you want to play two separate sources at the exact
	///		same time, you can call <c>FAudioSourceVoice_Start</c> with an
	///		<paramref name="operationSet"/> value of your choice, then call
	///		<see cref="CommitOperationSet"/> with that same value to start the
	///		sources together.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="operationSet">
	///		Either a value known by you or
	///		<see cref="Constants.FAUDIO_COMMIT_ALL"/>.</param>
	/// <returns><c>0</c> on success.</returns>
	uint CommitOperationSet(
		FAudio* audio,
		uint    operationSet
	);

	/// <summary>
	///		DO NOT USE THIS FUNCTION OR I SWEAR TO GOD
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <returns></returns>
	[Obsolete("This function will break your program! Use CommitOperationSet instead!")]
	uint CommitChanges(FAudio* audio);

	/// <summary>
	///		Requests various bits of performance information from the engine.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="perfData">
	///		The data.  See <see cref="FAudioPerformanceData"/> for details.
	/// </param>
	void GetPerformanceData(
		FAudio*                   audio,
		out FAudioPerformanceData perfData
	);

	/// <summary>
	///		When using a Debug binary, this lets you configure what information
	///		gets logged to output.  Be careful, this can spit out a LOT of text.
	/// </summary>
	/// <param name="audio">The audio.</param>
	/// <param name="debugConfiguration">
	///		See <see cref="FAudioDebugConfiguration"/> for details.
	/// </param>
	/// <param name="reserved">Set this to <see langword="null"/>.</param>
	void SetDebugConfiguration(
		FAudio*                      audio,
		ref FAudioDebugConfiguration debugConfiguration,
		void*                        reserved
	);

	// /* Requests the values that determine's the engine's update size.
	//  * For example, a 48KHz engine with a 1024-sample update period would return
	//  * 1024 for the numerator and 48000 for the denominator. With this information,
	//  * you can determine the precise update size in milliseconds.
	//  *
	//  * quantumNumerator - The engine's update size, in sample frames.
	//  * quantumDenominator - The engine's sample rate, in Hz
	//  */
	// FAUDIOAPI void FAudio_GetProcessingQuantum(
	// 	FAudio *audio,
	// 	uint32_t *quantumNumerator,
	// 	uint32_t *quantumDenominator
	// );
	void GetProcessingQuantum(
		FAudio*  audio,
		out uint quantumNumerator,
		out uint quantumDenominator
	);
#endregion

#region FAudioVoice Interface
	// /* Requests basic information about a voice.
	//  *
	//  * pVoiceDetails: See FAudioVoiceDetails for details.
	//  */
	// FAUDIOAPI void FAudioVoice_GetVoiceDetails(
	// 	FAudioVoice *voice,
	// 	FAudioVoiceDetails *pVoiceDetails
	// );
	void GetVoiceDetails(
		FAudioVoice*           voice,
		out FAudioVoiceDetails voiceDetails
	);

	// /* Change the output voices for this voice.
	//  * This function is invalid for mastering voices.
	//  *
	//  * pSendList:	List of output voices. If NULL, defaults to master.
	//  *		All output voices must have the same sample rate!
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetOutputVoices(
	// 	FAudioVoice *voice,
	// 	const FAudioVoiceSends *pSendList
	// );
	uint SetOutputVoices(
		FAudioVoice*         voice,
		ref FAudioVoiceSends sendList
	);

	// /* Change/Remove the effect chain for this voice.
	//  *
	//  * pEffectChain:	List of FAPO effects. This value can be NULL.
	//  *			Note that the final channel counts for this chain MUST
	//  *			match the input/output channel count that was
	//  *			determined at voice creation time!
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetEffectChain(
	// 	FAudioVoice *voice,
	// 	const FAudioEffectChain *pEffectChain
	// );
	uint SetEffectChain(
		FAudioVoice*          voice,
		ref FAudioEffectChain effectChain
	);

	// /* Enables an effect in the effect chain.
	//  *
	//  * EffectIndex:		The index of the effect (based on the chain order).
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_EnableEffect(
	// 	FAudioVoice *voice,
	// 	uint32_t EffectIndex,
	// 	uint32_t OperationSet
	// );
	uint EnableEffect(
		FAudioVoice* voice,
		uint         effectIndex,
		uint         operationSet
	);

	// /* Disables an effect in the effect chain.
	//  *
	//  * EffectIndex:		The index of the effect (based on the chain order).
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_DisableEffect(
	// 	FAudioVoice *voice,
	// 	uint32_t EffectIndex,
	// 	uint32_t OperationSet
	// );
	uint DisableEffect(
		FAudioVoice* voice,
		uint         effectIndex,
		uint         operationSet
	);

	// /* Queries the enabled/disabled state of an effect in the effect chain.
	//  *
	//  * EffectIndex:	The index of the effect (based on the chain order).
	//  * pEnabled:	Filled with either 1 (Enabled) or 0 (Disabled).
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI void FAudioVoice_GetEffectState(
	// 	FAudioVoice *voice,
	// 	uint32_t EffectIndex,
	// 	int32_t *pEnabled
	// );
	void GetEffectState(
		FAudioVoice* voice,
		uint         effectIndex,
		out int      enabled
	);

	// /* Submits a block of memory to be sent to FAPO::SetParameters.
	//  *
	//  * EffectIndex:		The index of the effect (based on the chain order).
	//  * pParameters:		The values to be copied and submitted to the FAPO.
	//  * ParametersByteSize:	This should match what the FAPO expects!
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetEffectParameters(
	// 	FAudioVoice *voice,
	// 	uint32_t EffectIndex,
	// 	const void *pParameters,
	// 	uint32_t ParametersByteSize,
	// 	uint32_t OperationSet
	// );
	uint SetEffectParameters(
		FAudioVoice* voice,
		uint         effectIndex,
		void*        parameters,
		uint         parametersByteSize,
		uint         operationSet
	);

	// /* Requests the latest parameters from FAPO::GetParameters.
	//  *
	//  * EffectIndex:		The index of the effect (based on the chain order).
	//  * pParameters:		Filled with the latest parameter values from the FAPO.
	//  * ParametersByteSize:	This should match what the FAPO expects!
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_GetEffectParameters(
	// 	FAudioVoice *voice,
	// 	uint32_t EffectIndex,
	// 	void *pParameters,
	// 	uint32_t ParametersByteSize
	// );
	uint GetEffectParameters(
		FAudioVoice* voice,
		uint         effectIndex,
		void*        parameters,
		uint         parametersByteSize
	);

	// /* Sets the filter variables for a voice.
	//  * This is only valid on voices with the USEFILTER flag.
	//  *
	//  * pParameters:		See FAudioFilterParameters for details.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetFilterParameters(
	// 	FAudioVoice *voice,
	// 	const FAudioFilterParameters *pParameters,
	// 	uint32_t OperationSet
	// );
	uint SetFilterParameters(
		FAudioVoice*               voice,
		ref FAudioFilterParameters parameters,
		uint                       operationSet
	);

	// /* Requests the filter variables for a voice.
	//  * This is only valid on voices with the USEFILTER flag.
	//  *
	//  * pParameters: See FAudioFilterParameters for details.
	//  */
	// FAUDIOAPI void FAudioVoice_GetFilterParameters(
	// 	FAudioVoice *voice,
	// 	FAudioFilterParameters *pParameters
	// );
	void GetFilterParameters(
		FAudioVoice*               voice,
		out FAudioFilterParameters parameters
	);

	// /* Sets the filter variables for a voice's output voice.
	//  * This is only valid on sends with the USEFILTER flag.
	//  *
	//  * pDestinationVoice:	An output voice from the voice's send list.
	//  * pParameters:		See FAudioFilterParameters for details.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetOutputFilterParameters(
	// 	FAudioVoice *voice,
	// 	FAudioVoice *pDestinationVoice,
	// 	const FAudioFilterParameters *pParameters,
	// 	uint32_t OperationSet
	// );
	uint SetOutputFilterParameters(
		FAudioVoice*               voice,
		FAudioVoice*               destinationVoice,
		ref FAudioFilterParameters parameters,
		uint                       operationSet
	);

	// /* Requests the filter variables for a voice's output voice.
	//  * This is only valid on sends with the USEFILTER flag.
	//  *
	//  * pDestinationVoice:	An output voice from the voice's send list.
	//  * pParameters:		See FAudioFilterParameters for details.
	//  */
	// FAUDIOAPI void FAudioVoice_GetOutputFilterParameters(
	// 	FAudioVoice *voice,
	// 	FAudioVoice *pDestinationVoice,
	// 	FAudioFilterParameters *pParameters
	// );
	void GetOutputFilterParameters(
		FAudioVoice*               voice,
		FAudioVoice*               destinationVoice,
		out FAudioFilterParameters parameters
	);

	// /* Sets the filter variables for a voice.
	//  * This is only valid on voices with the USEFILTER flag.
	//  *
	//  * pParameters:		See FAudioFilterParametersEXT for details.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetFilterParametersEXT(
	// 	FAudioVoice* voice,
	// 	const FAudioFilterParametersEXT* pParameters,
	// 	uint32_t OperationSet
	// );
	uint SetFilterParametersExt(
		FAudioVoice*                  voice,
		ref FAudioFilterParametersExt parameters,
		uint                          operationSet
	);

	// /* Requests the filter variables for a voice.
	//  * This is only valid on voices with the USEFILTER flag.
	//  *
	//  * pParameters: See FAudioFilterParametersEXT for details.
	//  */
	// FAUDIOAPI void FAudioVoice_GetFilterParametersEXT(
	// 	FAudioVoice* voice,
	// 	FAudioFilterParametersEXT* pParameters
	// );
	void GetFilterParametersExt(
		FAudioVoice*                  voice,
		out FAudioFilterParametersExt parameters
	);

	// /* Sets the filter variables for a voice's output voice.
	//  * This is only valid on sends with the USEFILTER flag.
	//  *
	//  * pDestinationVoice:	An output voice from the voice's send list.
	//  * pParameters:		See FAudioFilterParametersEXT for details.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetOutputFilterParametersEXT(
	// 	FAudioVoice* voice,
	// 	FAudioVoice* pDestinationVoice,
	// 	const FAudioFilterParametersEXT* pParameters,
	// 	uint32_t OperationSet
	// );
	uint SetOutputFilterParametersExt(
		FAudioVoice*                  voice,
		FAudioVoice*                  destinationVoice,
		ref FAudioFilterParametersExt parameters,
		uint                          operationSet
	);

	// /* Requests the filter variables for a voice's output voice.
	//  * This is only valid on sends with the USEFILTER flag.
	//  *
	//  * pDestinationVoice:	An output voice from the voice's send list.
	//  * pParameters:		See FAudioFilterParametersEXT for details.
	//  */
	// FAUDIOAPI void FAudioVoice_GetOutputFilterParametersEXT(
	// 	FAudioVoice* voice,
	// 	FAudioVoice* pDestinationVoice,
	// 	FAudioFilterParametersEXT* pParameters
	// );
	void GetOutputFilterParametersExt(
		FAudioVoice*                  voice,
		FAudioVoice*                  destinationVoice,
		out FAudioFilterParametersExt parameters
	);

	// /* Sets the global volume of a voice.
	//  *
	//  * Volume:		Amplitude ratio. 1.0f is default, 0.0f is silence.
	//  *			Note that you can actually set volume < 0.0f!
	//  *			Bounds: [-FAUDIO_MAX_VOLUME_LEVEL, FAUDIO_MAX_VOLUME_LEVEL]
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetVolume(
	// 	FAudioVoice *voice,
	// 	float Volume,
	// 	uint32_t OperationSet
	// );
	uint SetVolume(
		FAudioVoice* voice,
		float        volume,
		uint         operationSet
	);

	// /* Requests the global volume of a voice.
	//  *
	//  * pVolume: Filled with the current voice amplitude ratio.
	//  */
	// FAUDIOAPI void FAudioVoice_GetVolume(
	// 	FAudioVoice *voice,
	// 	float *pVolume
	// );
	void GetVolume(
		FAudioVoice* voice,
		out float    volume
	);

	// /* Sets the per-channel volumes of a voice.
	//  *
	//  * Channels:		Must match the channel count of this voice!
	//  * pVolumes:		Amplitude ratios for each channel. Same as SetVolume.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetChannelVolumes(
	// 	FAudioVoice *voice,
	// 	uint32_t Channels,
	// 	const float *pVolumes,
	// 	uint32_t OperationSet
	// );
	uint SetChannelVolumes(
		FAudioVoice* voice,
		uint         channels,
		float[]      volumes,
		uint         operationSet
	);

	// /* Requests the per-channel volumes of a voice.
	//  *
	//  * Channels:	Must match the channel count of this voice!
	//  * pVolumes:	Filled with the current channel amplitude ratios.
	//  */
	// FAUDIOAPI void FAudioVoice_GetChannelVolumes(
	// 	FAudioVoice *voice,
	// 	uint32_t Channels,
	// 	float *pVolumes
	// );
	void GetChannelVolumes(
		FAudioVoice* voice,
		uint         channels,
		float[]      volumes
	);

	// /* Sets the volumes of a send's output channels. The matrix is based on the
	//  * voice's input channels. For example, the default matrix for a 2-channel
	//  * source and a 2-channel output voice is as follows:
	//  * [0] = 1.0f; <- Left input, left output
	//  * [1] = 0.0f; <- Right input, left output
	//  * [2] = 0.0f; <- Left input, right output
	//  * [3] = 1.0f; <- Right input, right output
	//  * This is typically only used for panning or 3D sound (via F3DAudio).
	//  *
	//  * pDestinationVoice:	An output voice from the voice's send list.
	//  * SourceChannels:	Must match the voice's input channel count!
	//  * DestinationChannels:	Must match the destination's input channel count!
	//  * pLevelMatrix:	A float[SourceChannels * DestinationChannels].
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_SetOutputMatrix(
	// 	FAudioVoice *voice,
	// 	FAudioVoice *pDestinationVoice,
	// 	uint32_t SourceChannels,
	// 	uint32_t DestinationChannels,
	// 	const float *pLevelMatrix,
	// 	uint32_t OperationSet
	// );
	uint SetOutputMatrix(
		FAudioVoice* voice,
		FAudioVoice* destinationVoice,
		uint         sourceChannels,
		uint         destinationChannels,
		float*       levelMatrix,
		uint         operationSet
	);

	// /* Gets the volumes of a send's output channels. See SetOutputMatrix.
	//  *
	//  * pDestinationVoice:	An output voice from the voice's send list.
	//  * SourceChannels:	Must match the voice's input channel count!
	//  * DestinationChannels:	Must match the voice's output channel count!
	//  * pLevelMatrix:	A float[SourceChannels * DestinationChannels].
	//  */
	// FAUDIOAPI void FAudioVoice_GetOutputMatrix(
	// 	FAudioVoice *voice,
	// 	FAudioVoice *pDestinationVoice,
	// 	uint32_t SourceChannels,
	// 	uint32_t DestinationChannels,
	// 	float *pLevelMatrix
	// );
	void GetOutputMatrix(
		FAudioVoice* voice,
		FAudioVoice* destinationVoice,
		uint         sourceChannels,
		uint         destinationChannels,
		float[]      levelMatrix
	);

	// /* Removes this voice from the audio graph and frees memory. */
	// FAUDIOAPI void FAudioVoice_DestroyVoice(FAudioVoice *voice);
	// 
	// /*
	//  * Returns S_OK on success and E_FAIL if voice could not be destroyed (e. g., because it is in use).
	//  */
	// FAUDIOAPI uint32_t FAudioVoice_DestroyVoiceSafeEXT(FAudioVoice *voice);
	uint DestroyVoiceSafeExt(FAudioVoice* voice);
#endregion

#region FAudioSourceVoice Interface
	// /* Starts processing for a source voice.
	//  *
	//  * Flags:		Must be 0.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_Start(
	// 	FAudioSourceVoice *voice,
	// 	uint32_t Flags,
	// 	uint32_t OperationSet
	// );
	uint Start(
		FAudioSourceVoice* voice,
		uint               flags,
		uint               operationSet
	);

	// /* Pauses processing for a source voice. Yes, I said pausing.
	//  * If you want to _actually_ stop, call FlushSourceBuffers next.
	//  *
	//  * Flags:		Can be 0 or FAUDIO_PLAY_TAILS, which allows effects to
	//  *			keep emitting output even after processing has stopped.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_Stop(
	// 	FAudioSourceVoice *voice,
	// 	uint32_t Flags,
	// 	uint32_t OperationSet
	// );
	uint Stop(
		FAudioSourceVoice* voice,
		uint               flags,
		uint               operationSet
	);

	// /* Submits a block of wavedata for the source to process.
	//  *
	//  * pBuffer:	See FAudioBuffer for details.
	//  * pBufferWMA:	See FAudioBufferWMA for details. (Also, don't use WMA.)
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_SubmitSourceBuffer(
	// 	FAudioSourceVoice *voice,
	// 	const FAudioBuffer *pBuffer,
	// 	const FAudioBufferWMA *pBufferWMA
	// );
	uint SubmitSourceBuffer(
		FAudioSourceVoice*  voice,
		ref FAudioBuffer    buffer,
		ref FAudioBufferWma bufferWma
	);

	// /* Removes all buffers from a source, with a minor exception.
	//  * If the voice is still playing, the active buffer is left alone.
	//  * All buffers that are removed will spawn an OnBufferEnd callback.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_FlushSourceBuffers(
	// 	FAudioSourceVoice *voice
	// );
	uint FlushSourceBuffers(FAudioSourceVoice* voice);

	// /* Takes the last buffer currently queued and sets the END_OF_STREAM flag.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_Discontinuity(
	// 	FAudioSourceVoice *voice
	// );
	uint Discontinuity(FAudioSourceVoice* voice);

	// /* Sets the loop count of the active buffer to 0.
	//  *
	//  * OperationSet: See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_ExitLoop(
	// 	FAudioSourceVoice *voice,
	// 	uint32_t OperationSet
	// );
	uint ExitLoop(
		FAudioSourceVoice* voice,
		uint               operationSet
	);

	// /* Requests the state and some basic statistics for this source.
	//  *
	//  * pVoiceState:	See FAudioVoiceState for details.
	//  * Flags:	Can be 0 or FAUDIO_VOICE_NOSAMPLESPLAYED.
	//  */
	// FAUDIOAPI void FAudioSourceVoice_GetState(
	// 	FAudioSourceVoice *voice,
	// 	FAudioVoiceState *pVoiceState,
	// 	uint32_t Flags
	// );
	void GetState(
		FAudioSourceVoice*   voice,
		out FAudioVoiceState voiceState,
		uint                 flags
	);

	// /* Sets the frequency ratio (fancy phrase for pitch) of this source.
	//  *
	//  * Ratio:		The frequency ratio, must be <= MaxFrequencyRatio.
	//  * OperationSet:	See CommitChanges. Default is FAUDIO_COMMIT_NOW.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_SetFrequencyRatio(
	// 	FAudioSourceVoice *voice,
	// 	float Ratio,
	// 	uint32_t OperationSet
	// );
	uint SetFrequencyRatio(
		FAudioSourceVoice* voice,
		float              ratio,
		uint               operationSet
	);

	// /* Requests the frequency ratio (fancy phrase for pitch) of this source.
	//  *
	//  * pRatio: Filled with the frequency ratio.
	//  */
	// FAUDIOAPI void FAudioSourceVoice_GetFrequencyRatio(
	// 	FAudioSourceVoice *voice,
	// 	float *pRatio
	// );
	void GetFrequencyRatio(
		FAudioSourceVoice* voice,
		out float          ratio
	);

	// /* Resets the core sample rate of this source.
	//  * You probably don't want this, it's more likely you want SetFrequencyRatio.
	//  * This is used to recycle voices without having to constantly reallocate them.
	//  * For example, if you have wavedata that's all float32 mono, but the sample
	//  * rates are different, you can take a source that was being used for a 48KHz
	//  * wave and call this so it can be used for a 44.1KHz wave.
	//  *
	//  * NewSourceSampleRate: The new sample rate for this source.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioSourceVoice_SetSourceSampleRate(
	// 	FAudioSourceVoice *voice,
	// 	uint32_t NewSourceSampleRate
	// );
	uint SetSourceSampleRate(
		FAudioSourceVoice* voice,
		uint               newSourceSampleRate
	);
#endregion

#region FAudioMasteringVoice Interface
	// /* Requests the channel mask for the mastering voice.
	//  * This is typically used with F3DAudioInitialize, but you may find it
	//  * interesting if you want to see the user's basic speaker layout.
	//  *
	//  * pChannelMask: Filled with the channel mask.
	//  *
	//  * Returns 0 on success.
	//  */
	// FAUDIOAPI uint32_t FAudioMasteringVoice_GetChannelMask(
	// 	FAudioMasteringVoice *voice,
	// 	uint32_t *pChannelMask
	// );
	uint GetChannelMask(
		FAudioMasteringVoice* voice,
		uint*                 channelMask
	);
#endregion
}
