#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Microsoft.Xna.Framework.Graphics;

public static class FNA3D
{
	[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
	private static class Impl
	{
#region Driver Functions
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint FNA3D_PrepareWindowAttributes();

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetDrawableSize(
			IntPtr  window,
			out int w,
			out int h
		);
#endregion

#region Init/Quit
		/* IntPtr refers to an FNA3D_Device* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateDevice(
			ref FNA3D.PresentationParameters presentationParameters,
			byte                             debugMode
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DestroyDevice(IntPtr device);
#endregion

#region Presentation
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(
			IntPtr        device,
			ref Rectangle sourceRectangle,
			ref Rectangle destinationRectangle,
			IntPtr        overrideWindowHandle
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(
			IntPtr device,
			IntPtr sourceRectangle,      /* null Rectangle */
			IntPtr destinationRectangle, /* null Rectangle */
			IntPtr overrideWindowHandle
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(
			IntPtr        device,
			ref Rectangle sourceRectangle,
			IntPtr        destinationRectangle, /* null Rectangle */
			IntPtr        overrideWindowHandle
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(
			IntPtr        device,
			IntPtr        sourceRectangle, /* null Rectangle */
			ref Rectangle destinationRectangle,
			IntPtr        overrideWindowHandle
		);
#endregion

#region Drawing
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_Clear(
			IntPtr       device,
			ClearOptions options,
			ref Vector4  color,
			float        depth,
			int          stencil
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DrawIndexedPrimitives(
			IntPtr           device,
			PrimitiveType    primitiveType,
			int              baseVertex,
			int              minVertexIndex,
			int              numVertices,
			int              startIndex,
			int              primitiveCount,
			IntPtr           indices, /* FNA3D_Buffer* */
			IndexElementSize indexElementSize
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DrawInstancedPrimitives(
			IntPtr           device,
			PrimitiveType    primitiveType,
			int              baseVertex,
			int              minVertexIndex,
			int              numVertices,
			int              startIndex,
			int              primitiveCount,
			int              instanceCount,
			IntPtr           indices, /* FNA3D_Buffer* */
			IndexElementSize indexElementSize
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DrawPrimitives(
			IntPtr        device,
			PrimitiveType primitiveType,
			int           vertexStart,
			int           primitiveCount
		);
#endregion

#region Mutable Render States
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetViewport(
			IntPtr             device,
			ref FNA3D.Viewport viewport
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetScissorRect(
			IntPtr        device,
			ref Rectangle scissor
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetBlendFactor(
			IntPtr    device,
			out Color blendFactor
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetBlendFactor(
			IntPtr    device,
			ref Color blendFactor
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetMultiSampleMask(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetMultiSampleMask(
			IntPtr device,
			int    mask
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetReferenceStencil(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetReferenceStencil(
			IntPtr device,
			int    reference
		);
#endregion

#region Immutable Render States
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetBlendState(
			IntPtr               device,
			ref FNA3D.BlendState blendState
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetDepthStencilState(
			IntPtr                      device,
			ref FNA3D.DepthStencilState depthStencilState
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ApplyRasterizerState(
			IntPtr                    device,
			ref FNA3D.RasterizerState rasterizerState
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_VerifySampler(
			IntPtr                 device,
			int                    index,
			IntPtr                 texture, /* FNA3D_Texture* */
			ref FNA3D.SamplerState sampler
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_VerifyVertexSampler(
			IntPtr                 device,
			int                    index,
			IntPtr                 texture, /* FNA3D_Texture* */
			ref FNA3D.SamplerState sampler
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void FNA3D_ApplyVertexBufferBindings(
			IntPtr                     device,
			FNA3D.VertexBufferBinding* bindings,
			int                        numBindings,
			byte                       bindingsUpdated,
			int                        baseVertex
		);
#endregion

#region Render Targets
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetRenderTargets(
			IntPtr      device,
			IntPtr      renderTargets, /* FNA3D_RenderTargetBinding* */
			int         numRenderTargets,
			IntPtr      depthStencilBuffer, /* FNA3D_Renderbuffer */
			DepthFormat depthFormat,
			byte        preserveDepthStencilContents
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void FNA3D_SetRenderTargets(
			IntPtr                     device,
			FNA3D.RenderTargetBinding* renderTargets,
			int                        numRenderTargets,
			IntPtr                     depthStencilBuffer, /* FNA3D_Renderbuffer */
			DepthFormat                depthFormat,
			byte                       preserveDepthStencilContents
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ResolveTarget(
			IntPtr                        device,
			ref FNA3D.RenderTargetBinding target
		);
#endregion

#region Backbuffer Functions
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ResetBackbuffer(
			IntPtr                           device,
			ref FNA3D.PresentationParameters presentationParameters
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ReadBackbuffer(
			IntPtr device,
			int    x,
			int    y,
			int    w,
			int    h,
			IntPtr data,
			int    dataLen
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetBackbufferSize(
			IntPtr  device,
			out int w,
			out int h
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern SurfaceFormat FNA3D_GetBackbufferSurfaceFormat(
			IntPtr device
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern DepthFormat FNA3D_GetBackbufferDepthFormat(
			IntPtr device
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetBackbufferMultiSampleCount(
			IntPtr device
		);
#endregion

#region Textures
		/* IntPtr refers to an FNA3D_Texture* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateTexture2D(
			IntPtr        device,
			SurfaceFormat format,
			int           width,
			int           height,
			int           levelCount,
			byte          isRenderTarget
		);

		/* IntPtr refers to an FNA3D_Texture* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateTexture3D(
			IntPtr        device,
			SurfaceFormat format,
			int           width,
			int           height,
			int           depth,
			int           levelCount
		);

		/* IntPtr refers to an FNA3D_Texture* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateTextureCube(
			IntPtr        device,
			SurfaceFormat format,
			int           size,
			int           levelCount,
			byte          isRenderTarget
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeTexture(
			IntPtr device,
			IntPtr texture /* FNA3D_Texture* */
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureData2D(
			IntPtr device,
			IntPtr texture,
			int    x,
			int    y,
			int    w,
			int    h,
			int    level,
			IntPtr data,
			int    dataLength
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureData3D(
			IntPtr device,
			IntPtr texture,
			int    x,
			int    y,
			int    z,
			int    w,
			int    h,
			int    d,
			int    level,
			IntPtr data,
			int    dataLength
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureDataCube(
			IntPtr      device,
			IntPtr      texture,
			int         x,
			int         y,
			int         w,
			int         h,
			CubeMapFace cubeMapFace,
			int         level,
			IntPtr      data,
			int         dataLength
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureDataYUV(
			IntPtr device,
			IntPtr y,
			IntPtr u,
			IntPtr v,
			int    yWidth,
			int    yHeight,
			int    uvWidth,
			int    uvHeight,
			IntPtr data,
			int    dataLength
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetTextureData2D(
			IntPtr device,
			IntPtr texture,
			int    x,
			int    y,
			int    w,
			int    h,
			int    level,
			IntPtr data,
			int    dataLength
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetTextureData3D(
			IntPtr device,
			IntPtr texture,
			int    x,
			int    y,
			int    z,
			int    w,
			int    h,
			int    d,
			int    level,
			IntPtr data,
			int    dataLength
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetTextureDataCube(
			IntPtr      device,
			IntPtr      texture,
			int         x,
			int         y,
			int         w,
			int         h,
			CubeMapFace cubeMapFace,
			int         level,
			IntPtr      data,
			int         dataLength
		);
#endregion

#region Renderbuffers
		/* IntPtr refers to an FNA3D_Renderbuffer* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenColorRenderbuffer(
			IntPtr        device,
			int           width,
			int           height,
			SurfaceFormat format,
			int           multiSampleCount,
			IntPtr        texture /* FNA3D_Texture* */
		);

		/* IntPtr refers to an FNA3D_Renderbuffer* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenDepthStencilRenderbuffer(
			IntPtr      device,
			int         width,
			int         height,
			DepthFormat format,
			int         multiSampleCount
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeRenderbuffer(
			IntPtr device,
			IntPtr renderbuffer
		);
#endregion

#region Vertex Buffers
		/* IntPtr refers to an FNA3D_Buffer* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenVertexBuffer(
			IntPtr      device,
			byte        dynamic,
			BufferUsage usage,
			int         sizeInBytes
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeVertexBuffer(
			IntPtr device,
			IntPtr buffer
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetVertexBufferData(
			IntPtr         device,
			IntPtr         buffer,
			int            offsetInBytes,
			IntPtr         data,
			int            elementCount,
			int            elementSizeInBytes,
			int            vertexStride,
			SetDataOptions options
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetVertexBufferData(
			IntPtr device,
			IntPtr buffer,
			int    offsetInBytes,
			IntPtr data,
			int    elementCount,
			int    elementSizeInBytes,
			int    vertexStride
		);
#endregion

#region Index Buffers
		/* IntPtr refers to an FNA3D_Buffer* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenIndexBuffer(
			IntPtr      device,
			byte        dynamic,
			BufferUsage usage,
			int         sizeInBytes
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeIndexBuffer(
			IntPtr device,
			IntPtr buffer
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetIndexBufferData(
			IntPtr         device,
			IntPtr         buffer,
			int            offsetInBytes,
			IntPtr         data,
			int            dataLength,
			SetDataOptions options
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetIndexBufferData(
			IntPtr device,
			IntPtr buffer,
			int    offsetInBytes,
			IntPtr data,
			int    dataLength
		);
#endregion

#region Effects
		/* IntPtr refers to an FNA3D_Effect* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_CreateEffect(
			IntPtr     device,
			byte[]     effectCode,
			int        length,
			out IntPtr effect,
			out IntPtr effectData
		);

		/* IntPtr refers to an FNA3D_Effect* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_CloneEffect(
			IntPtr     device,
			IntPtr     cloneSource,
			out IntPtr effect,
			out IntPtr effectData
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeEffect(
			IntPtr device,
			IntPtr effect
		);

		/* effect refers to a MOJOSHADER_effect*, technique to a MOJOSHADER_effectTechnique* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetEffectTechnique(
			IntPtr device,
			IntPtr effect,
			IntPtr technique
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ApplyEffect(
			IntPtr device,
			IntPtr effect,
			uint   pass,
			IntPtr stateChanges /* MOJOSHADER_effectStateChanges* */
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_BeginPassRestore(
			IntPtr device,
			IntPtr effect,
			IntPtr stateChanges /* MOJOSHADER_effectStateChanges* */
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_EndPassRestore(
			IntPtr device,
			IntPtr effect
		);
#endregion

#region Queries
		/* IntPtr refers to an FNA3D_Query* */
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateQuery(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeQuery(
			IntPtr device,
			IntPtr query
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_QueryBegin(
			IntPtr device,
			IntPtr query
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_QueryEnd(
			IntPtr device,
			IntPtr query
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_QueryComplete(
			IntPtr device,
			IntPtr query
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_QueryPixelCount(
			IntPtr device,
			IntPtr query
		);
#endregion

#region Feature Queries
		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsDXT1(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsS3TC(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsBC7(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsHardwareInstancing(
			IntPtr device
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsNoOverwrite(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsSRGBRenderTargets(IntPtr device);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetMaxTextureSlots(
			IntPtr  device,
			out int textures,
			out int vertexTextures
		);

		[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetMaxMultiSampleCount(
			IntPtr        device,
			SurfaceFormat format,
			int           preferredMultiSampleCount
		);
#endregion
	}

	internal const string native_lib_name = "FNA3D";

	[StructLayout(LayoutKind.Sequential)]
	public struct Viewport
	{
		public int   x;
		public int   y;
		public int   w;
		public int   h;
		public float minDepth;
		public float maxDepth;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct BlendState
	{
		public Blend              colorSourceBlend;
		public Blend              colorDestinationBlend;
		public BlendFunction      colorBlendFunction;
		public Blend              alphaSourceBlend;
		public Blend              alphaDestinationBlend;
		public BlendFunction      alphaBlendFunction;
		public ColorWriteChannels colorWriteEnable;
		public ColorWriteChannels colorWriteEnable1;
		public ColorWriteChannels colorWriteEnable2;
		public ColorWriteChannels colorWriteEnable3;
		public Color              blendFactor;
		public int                multiSampleMask;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DepthStencilState
	{
		public byte             depthBufferEnable;
		public byte             depthBufferWriteEnable;
		public CompareFunction  depthBufferFunction;
		public byte             stencilEnable;
		public int              stencilMask;
		public int              stencilWriteMask;
		public byte             twoSidedStencilMode;
		public StencilOperation stencilFail;
		public StencilOperation stencilDepthBufferFail;
		public StencilOperation stencilPass;
		public CompareFunction  stencilFunction;
		public StencilOperation ccwStencilFail;
		public StencilOperation ccwStencilDepthBufferFail;
		public StencilOperation ccwStencilPass;
		public CompareFunction  ccwStencilFunction;
		public int              referenceStencil;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RasterizerState
	{
		public FillMode fillMode;
		public CullMode cullMode;
		public float    depthBias;
		public float    slopeScaleDepthBias;
		public byte     scissorTestEnable;
		public byte     multiSampleAntiAlias;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SamplerState
	{
		public TextureFilter      filter;
		public TextureAddressMode addressU;
		public TextureAddressMode addressV;
		public TextureAddressMode addressW;
		public float              mipMapLevelOfDetailBias;
		public int                maxAnisotropy;
		public int                maxMipLevel;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct VertexDeclaration
	{
		public int    vertexStride;
		public int    elementCount;
		public IntPtr elements; /* FNA3D_VertexElement* */
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct VertexBufferBinding
	{
		public IntPtr            vertexBuffer; /* FNA3D_Buffer* */
		public VertexDeclaration vertexDeclaration;
		public int               vertexOffset;
		public int               instanceFrequency;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RenderTargetBinding
	{
		public byte   type;
		public int    data1; /* width for 2D, size for Cube */
		public int    data2; /* height for 2D, face for Cube */
		public int    levelCount;
		public int    multiSampleCount;
		public IntPtr texture;
		public IntPtr colorBuffer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct PresentationParameters
	{
		public int                backBufferWidth;
		public int                backBufferHeight;
		public SurfaceFormat      backBufferFormat;
		public int                multiSampleCount;
		public IntPtr             deviceWindowHandle;
		public byte               isFullScreen;
		public DepthFormat        depthStencilFormat;
		public PresentInterval    presentationInterval;
		public DisplayOrientation displayOrientation;
		public RenderTargetUsage  renderTargetUsage;
	}

#region Logging
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void LogFunc(IntPtr msg);

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	public static extern void FNA3D_HookLogFunctions(
		LogFunc info,
		LogFunc warn,
		LogFunc error
	);

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FNA3D_LinkedVersion();
#endregion

#region Driver Functions
	public static uint FNA3D_PrepareWindowAttributes()
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_PrepareWindowAttributes();
	}

	public static void FNA3D_GetDrawableSize(
		IntPtr  window,
		out int w,
		out int h
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetDrawableSize(window, out w, out h);
	}
#endregion

#region Init/Quit
	/* IntPtr refers to an FNA3D_Device* */
	public static IntPtr FNA3D_CreateDevice(
		ref FNA3D.PresentationParameters presentationParameters,
		byte                             debugMode
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_CreateDevice(ref presentationParameters, debugMode);
	}

	public static void FNA3D_DestroyDevice(IntPtr device)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_DestroyDevice(device);
	}
#endregion

#region Presentation
	public static void FNA3D_SwapBuffers(
		IntPtr        device,
		ref Rectangle sourceRectangle,
		ref Rectangle destinationRectangle,
		IntPtr        overrideWindowHandle
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SwapBuffers(
			device,
			ref sourceRectangle,
			ref destinationRectangle,
			overrideWindowHandle
		);
	}

	public static void FNA3D_SwapBuffers(
		IntPtr device,
		IntPtr sourceRectangle,      /* null Rectangle */
		IntPtr destinationRectangle, /* null Rectangle */
		IntPtr overrideWindowHandle
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SwapBuffers(
			device,
			sourceRectangle,
			destinationRectangle,
			overrideWindowHandle
		);
	}

	public static void FNA3D_SwapBuffers(
		IntPtr        device,
		ref Rectangle sourceRectangle,
		IntPtr        destinationRectangle, /* null Rectangle */
		IntPtr        overrideWindowHandle
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SwapBuffers(
			device,
			ref sourceRectangle,
			destinationRectangle,
			overrideWindowHandle
		);
	}

	public static void FNA3D_SwapBuffers(
		IntPtr        device,
		IntPtr        sourceRectangle, /* null Rectangle */
		ref Rectangle destinationRectangle,
		IntPtr        overrideWindowHandle
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SwapBuffers(
			device,
			sourceRectangle,
			ref destinationRectangle,
			overrideWindowHandle
		);
	}
#endregion

#region Drawing
	public static void FNA3D_Clear(
		IntPtr       device,
		ClearOptions options,
		ref Vector4  color,
		float        depth,
		int          stencil
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_Clear(device, options, ref color, depth, stencil);
	}

	public static void FNA3D_DrawIndexedPrimitives(
		IntPtr           device,
		PrimitiveType    primitiveType,
		int              baseVertex,
		int              minVertexIndex,
		int              numVertices,
		int              startIndex,
		int              primitiveCount,
		IntPtr           indices, /* FNA3D_Buffer* */
		IndexElementSize indexElementSize
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_DrawIndexedPrimitives(
			device,
			primitiveType,
			baseVertex,
			minVertexIndex,
			numVertices,
			startIndex,
			primitiveCount,
			indices,
			indexElementSize
		);
	}

	public static void FNA3D_DrawInstancedPrimitives(
		IntPtr           device,
		PrimitiveType    primitiveType,
		int              baseVertex,
		int              minVertexIndex,
		int              numVertices,
		int              startIndex,
		int              primitiveCount,
		int              instanceCount,
		IntPtr           indices, /* FNA3D_Buffer* */
		IndexElementSize indexElementSize
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_DrawInstancedPrimitives(
			device,
			primitiveType,
			baseVertex,
			minVertexIndex,
			numVertices,
			startIndex,
			primitiveCount,
			instanceCount,
			indices,
			indexElementSize
		);
	}

	public static void FNA3D_DrawPrimitives(
		IntPtr        device,
		PrimitiveType primitiveType,
		int           vertexStart,
		int           primitiveCount
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_DrawPrimitives(device, primitiveType, vertexStart, primitiveCount);
	}
#endregion

#region Mutable Render States
	public static void FNA3D_SetViewport(
		IntPtr             device,
		ref FNA3D.Viewport viewport
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetViewport(device, ref viewport);
	}

	public static void FNA3D_SetScissorRect(
		IntPtr        device,
		ref Rectangle scissor
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetScissorRect(device, ref scissor);
	}

	public static void FNA3D_GetBlendFactor(
		IntPtr    device,
		out Color blendFactor
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetBlendFactor(device, out blendFactor);
	}

	public static void FNA3D_SetBlendFactor(
		IntPtr    device,
		ref Color blendFactor
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetBlendFactor(device, ref blendFactor);
	}

	public static int FNA3D_GetMultiSampleMask(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GetMultiSampleMask(device);
	}

	public static void FNA3D_SetMultiSampleMask(
		IntPtr device,
		int    mask
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetMultiSampleMask(device, mask);
	}

	public static int FNA3D_GetReferenceStencil(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GetReferenceStencil(device);
	}

	public static void FNA3D_SetReferenceStencil(
		IntPtr device,
		int    reference
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetReferenceStencil(device, reference);
	}
#endregion

#region Immutable Render States
	public static void FNA3D_SetBlendState(
		IntPtr               device,
		ref FNA3D.BlendState blendState
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetBlendState(device, ref blendState);
	}

	public static void FNA3D_SetDepthStencilState(
		IntPtr                      device,
		ref FNA3D.DepthStencilState depthStencilState
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetDepthStencilState(device, ref depthStencilState);
	}

	public static void FNA3D_ApplyRasterizerState(
		IntPtr                    device,
		ref FNA3D.RasterizerState rasterizerState
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_ApplyRasterizerState(device, ref rasterizerState);
	}

	public static void FNA3D_VerifySampler(
		IntPtr                 device,
		int                    index,
		IntPtr                 texture, /* FNA3D_Texture* */
		ref FNA3D.SamplerState sampler
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_VerifySampler(device, index, texture, ref sampler);
	}

	public static void FNA3D_VerifyVertexSampler(
		IntPtr                 device,
		int                    index,
		IntPtr                 texture, /* FNA3D_Texture* */
		ref FNA3D.SamplerState sampler
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_VerifyVertexSampler(device, index, texture, ref sampler);
	}

	public static unsafe void FNA3D_ApplyVertexBufferBindings(
		IntPtr                     device,
		FNA3D.VertexBufferBinding* bindings,
		int                        numBindings,
		byte                       bindingsUpdated,
		int                        baseVertex
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_ApplyVertexBufferBindings(
			device,
			bindings,
			numBindings,
			bindingsUpdated,
			baseVertex
		);
	}
#endregion

#region Render Targets
	public static void FNA3D_SetRenderTargets(
		IntPtr      device,
		IntPtr      renderTargets, /* FNA3D_RenderTargetBinding* */
		int         numRenderTargets,
		IntPtr      depthStencilBuffer, /* FNA3D_Renderbuffer */
		DepthFormat depthFormat,
		byte        preserveDepthStencilContents
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetRenderTargets(
			device,
			renderTargets,
			numRenderTargets,
			depthStencilBuffer,
			depthFormat,
			preserveDepthStencilContents
		);
	}

	public static unsafe void FNA3D_SetRenderTargets(
		IntPtr                     device,
		FNA3D.RenderTargetBinding* renderTargets,
		int                        numRenderTargets,
		IntPtr                     depthStencilBuffer, /* FNA3D_Renderbuffer */
		DepthFormat                depthFormat,
		byte                       preserveDepthStencilContents
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetRenderTargets(
			device,
			renderTargets,
			numRenderTargets,
			depthStencilBuffer,
			depthFormat,
			preserveDepthStencilContents
		);
	}

	public static void FNA3D_ResolveTarget(
		IntPtr                        device,
		ref FNA3D.RenderTargetBinding target
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_ResolveTarget(device, ref target);
	}
#endregion

#region Backbuffer Functions
	public static void FNA3D_ResetBackbuffer(
		IntPtr                           device,
		ref FNA3D.PresentationParameters presentationParameters
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_ResetBackbuffer(device, ref presentationParameters);
	}

	public static void FNA3D_ReadBackbuffer(
		IntPtr device,
		int    x,
		int    y,
		int    w,
		int    h,
		IntPtr data,
		int    dataLen
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_ReadBackbuffer(device, x, y, w, h, data, dataLen);
	}

	public static void FNA3D_GetBackbufferSize(
		IntPtr  device,
		out int w,
		out int h
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetBackbufferSize(device, out w, out h);
	}

	public static SurfaceFormat FNA3D_GetBackbufferSurfaceFormat(
		IntPtr device
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GetBackbufferSurfaceFormat(device);
	}

	public static DepthFormat FNA3D_GetBackbufferDepthFormat(
		IntPtr device
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GetBackbufferDepthFormat(device);
	}

	public static int FNA3D_GetBackbufferMultiSampleCount(
		IntPtr device
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GetBackbufferMultiSampleCount(device);
	}
#endregion

#region Textures
	/* IntPtr refers to an FNA3D_Texture* */
	public static IntPtr FNA3D_CreateTexture2D(
		IntPtr        device,
		SurfaceFormat format,
		int           width,
		int           height,
		int           levelCount,
		byte          isRenderTarget
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_CreateTexture2D(
			device,
			format,
			width,
			height,
			levelCount,
			isRenderTarget
		);
	}

	/* IntPtr refers to an FNA3D_Texture* */
	public static IntPtr FNA3D_CreateTexture3D(
		IntPtr        device,
		SurfaceFormat format,
		int           width,
		int           height,
		int           depth,
		int           levelCount
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_CreateTexture3D(
			device,
			format,
			width,
			height,
			depth,
			levelCount
		);
	}

	/* IntPtr refers to an FNA3D_Texture* */
	public static IntPtr FNA3D_CreateTextureCube(
		IntPtr        device,
		SurfaceFormat format,
		int           size,
		int           levelCount,
		byte          isRenderTarget
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_CreateTextureCube(
			device,
			format,
			size,
			levelCount,
			isRenderTarget
		);
	}

	public static void FNA3D_AddDisposeTexture(
		IntPtr device,
		IntPtr texture /* FNA3D_Texture* */
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_AddDisposeTexture(device, texture);
	}

	public static void FNA3D_SetTextureData2D(
		IntPtr device,
		IntPtr texture,
		int    x,
		int    y,
		int    w,
		int    h,
		int    level,
		IntPtr data,
		int    dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetTextureData2D(
			device,
			texture,
			x,
			y,
			w,
			h,
			level,
			data,
			dataLength
		);
	}

	public static void FNA3D_SetTextureData3D(
		IntPtr device,
		IntPtr texture,
		int    x,
		int    y,
		int    z,
		int    w,
		int    h,
		int    d,
		int    level,
		IntPtr data,
		int    dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetTextureData3D(
			device,
			texture,
			x,
			y,
			z,
			w,
			h,
			d,
			level,
			data,
			dataLength
		);
	}

	public static void FNA3D_SetTextureDataCube(
		IntPtr      device,
		IntPtr      texture,
		int         x,
		int         y,
		int         w,
		int         h,
		CubeMapFace cubeMapFace,
		int         level,
		IntPtr      data,
		int         dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetTextureDataCube(
			device,
			texture,
			x,
			y,
			w,
			h,
			cubeMapFace,
			level,
			data,
			dataLength
		);
	}

	public static void FNA3D_SetTextureDataYUV(
		IntPtr device,
		IntPtr y,
		IntPtr u,
		IntPtr v,
		int    yWidth,
		int    yHeight,
		int    uvWidth,
		int    uvHeight,
		IntPtr data,
		int    dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetTextureDataYUV(
			device,
			y,
			u,
			v,
			yWidth,
			yHeight,
			uvWidth,
			uvHeight,
			data,
			dataLength
		);
	}

	public static void FNA3D_GetTextureData2D(
		IntPtr device,
		IntPtr texture,
		int    x,
		int    y,
		int    w,
		int    h,
		int    level,
		IntPtr data,
		int    dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetTextureData2D(
			device,
			texture,
			x,
			y,
			w,
			h,
			level,
			data,
			dataLength
		);
	}

	public static void FNA3D_GetTextureData3D(
		IntPtr device,
		IntPtr texture,
		int    x,
		int    y,
		int    z,
		int    w,
		int    h,
		int    d,
		int    level,
		IntPtr data,
		int    dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetTextureData3D(
			device,
			texture,
			x,
			y,
			z,
			w,
			h,
			d,
			level,
			data,
			dataLength
		);
	}

	public static void FNA3D_GetTextureDataCube(
		IntPtr      device,
		IntPtr      texture,
		int         x,
		int         y,
		int         w,
		int         h,
		CubeMapFace cubeMapFace,
		int         level,
		IntPtr      data,
		int         dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetTextureDataCube(
			device,
			texture,
			x,
			y,
			w,
			h,
			cubeMapFace,
			level,
			data,
			dataLength
		);
	}
#endregion

#region Renderbuffers
	/* IntPtr refers to an FNA3D_Renderbuffer* */
	public static IntPtr FNA3D_GenColorRenderbuffer(
		IntPtr        device,
		int           width,
		int           height,
		SurfaceFormat format,
		int           multiSampleCount,
		IntPtr        texture /* FNA3D_Texture* */
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GenColorRenderbuffer(
			device,
			width,
			height,
			format,
			multiSampleCount,
			texture
		);
	}

	/* IntPtr refers to an FNA3D_Renderbuffer* */
	public static IntPtr FNA3D_GenDepthStencilRenderbuffer(
		IntPtr      device,
		int         width,
		int         height,
		DepthFormat format,
		int         multiSampleCount
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GenDepthStencilRenderbuffer(
			device,
			width,
			height,
			format,
			multiSampleCount
		);
	}

	public static void FNA3D_AddDisposeRenderbuffer(
		IntPtr device,
		IntPtr renderbuffer
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_AddDisposeRenderbuffer(device, renderbuffer);
	}
#endregion

#region Vertex Buffers
	/* IntPtr refers to an FNA3D_Buffer* */
	public static IntPtr FNA3D_GenVertexBuffer(
		IntPtr      device,
		byte        dynamic,
		BufferUsage usage,
		int         sizeInBytes
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GenVertexBuffer(device, dynamic, usage, sizeInBytes);
	}

	public static void FNA3D_AddDisposeVertexBuffer(
		IntPtr device,
		IntPtr buffer
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_AddDisposeVertexBuffer(device, buffer);
	}

	public static void FNA3D_SetVertexBufferData(
		IntPtr         device,
		IntPtr         buffer,
		int            offsetInBytes,
		IntPtr         data,
		int            elementCount,
		int            elementSizeInBytes,
		int            vertexStride,
		SetDataOptions options
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetVertexBufferData(
			device,
			buffer,
			offsetInBytes,
			data,
			elementCount,
			elementSizeInBytes,
			vertexStride,
			options
		);
	}

	public static void FNA3D_GetVertexBufferData(
		IntPtr device,
		IntPtr buffer,
		int    offsetInBytes,
		IntPtr data,
		int    elementCount,
		int    elementSizeInBytes,
		int    vertexStride
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetVertexBufferData(
			device,
			buffer,
			offsetInBytes,
			data,
			elementCount,
			elementSizeInBytes,
			vertexStride
		);
	}
#endregion

#region Index Buffers
	/* IntPtr refers to an FNA3D_Buffer* */
	public static IntPtr FNA3D_GenIndexBuffer(
		IntPtr      device,
		byte        dynamic,
		BufferUsage usage,
		int         sizeInBytes
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GenIndexBuffer(device, dynamic, usage, sizeInBytes);
	}

	public static void FNA3D_AddDisposeIndexBuffer(
		IntPtr device,
		IntPtr buffer
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_AddDisposeIndexBuffer(device, buffer);
	}

	public static void FNA3D_SetIndexBufferData(
		IntPtr         device,
		IntPtr         buffer,
		int            offsetInBytes,
		IntPtr         data,
		int            dataLength,
		SetDataOptions options
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetIndexBufferData(
			device,
			buffer,
			offsetInBytes,
			data,
			dataLength,
			options
		);
	}

	public static void FNA3D_GetIndexBufferData(
		IntPtr device,
		IntPtr buffer,
		int    offsetInBytes,
		IntPtr data,
		int    dataLength
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetIndexBufferData(
			device,
			buffer,
			offsetInBytes,
			data,
			dataLength
		);
	}
#endregion

#region Effects
	/* IntPtr refers to an FNA3D_Effect* */
	public static void FNA3D_CreateEffect(
		IntPtr     device,
		byte[]     effectCode,
		int        length,
		out IntPtr effect,
		out IntPtr effectData
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_CreateEffect(
			device,
			effectCode,
			length,
			out effect,
			out effectData
		);
	}

	/* IntPtr refers to an FNA3D_Effect* */
	public static void FNA3D_CloneEffect(
		IntPtr     device,
		IntPtr     cloneSource,
		out IntPtr effect,
		out IntPtr effectData
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_CloneEffect(device, cloneSource, out effect, out effectData);
	}

	public static void FNA3D_AddDisposeEffect(
		IntPtr device,
		IntPtr effect
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_AddDisposeEffect(device, effect);
	}

	/* effect refers to a MOJOSHADER_effect*, technique to a MOJOSHADER_effectTechnique* */
	public static void FNA3D_SetEffectTechnique(
		IntPtr device,
		IntPtr effect,
		IntPtr technique
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_SetEffectTechnique(device, effect, technique);
	}

	public static void FNA3D_ApplyEffect(
		IntPtr device,
		IntPtr effect,
		uint   pass,
		IntPtr stateChanges /* MOJOSHADER_effectStateChanges* */
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_ApplyEffect(device, effect, pass, stateChanges);
	}

	public static void FNA3D_BeginPassRestore(
		IntPtr device,
		IntPtr effect,
		IntPtr stateChanges /* MOJOSHADER_effectStateChanges* */
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_BeginPassRestore(device, effect, stateChanges);
	}

	public static void FNA3D_EndPassRestore(
		IntPtr device,
		IntPtr effect
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_EndPassRestore(device, effect);
	}
#endregion

#region Queries
	/* IntPtr refers to an FNA3D_Query* */
	public static IntPtr FNA3D_CreateQuery(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_CreateQuery(device);
	}

	public static void FNA3D_AddDisposeQuery(
		IntPtr device,
		IntPtr query
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_AddDisposeQuery(device, query);
	}

	public static void FNA3D_QueryBegin(
		IntPtr device,
		IntPtr query
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_QueryBegin(device, query);
	}

	public static void FNA3D_QueryEnd(
		IntPtr device,
		IntPtr query
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_QueryEnd(device, query);
	}

	public static byte FNA3D_QueryComplete(
		IntPtr device,
		IntPtr query
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_QueryComplete(device, query);
	}

	public static int FNA3D_QueryPixelCount(
		IntPtr device,
		IntPtr query
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_QueryPixelCount(device, query);
	}
#endregion

#region Feature Queries
	public static byte FNA3D_SupportsDXT1(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_SupportsDXT1(device);
	}

	public static byte FNA3D_SupportsS3TC(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_SupportsS3TC(device);
	}

	public static byte FNA3D_SupportsBC7(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_SupportsBC7(device);
	}

	public static byte FNA3D_SupportsHardwareInstancing(
		IntPtr device
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_SupportsHardwareInstancing(device);
	}

	public static byte FNA3D_SupportsNoOverwrite(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_SupportsNoOverwrite(device);
	}

	public static byte FNA3D_SupportsSRGBRenderTargets(IntPtr device)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_SupportsSRGBRenderTargets(device);
	}

	public static void FNA3D_GetMaxTextureSlots(
		IntPtr  device,
		out int textures,
		out int vertexTextures
	)
	{
		ThreadCheck.CheckThread();
		Impl.FNA3D_GetMaxTextureSlots(device, out textures, out vertexTextures);
	}

	public static int FNA3D_GetMaxMultiSampleCount(
		IntPtr        device,
		SurfaceFormat format,
		int           preferredMultiSampleCount
	)
	{
		ThreadCheck.CheckThread();
		return Impl.FNA3D_GetMaxMultiSampleCount(
			device,
			format,
			preferredMultiSampleCount
		);
	}
#endregion

#region Debugging
	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	private static extern unsafe void FNA3D_SetStringMarker(
		IntPtr device,
		byte*  text
	);

	public static unsafe void FNA3D_SetStringMarker(
		IntPtr device,
		string text
	)
	{
		byte* utf8Text = SDL2.SDL.Utf8EncodeHeap(text);
		FNA3D_SetStringMarker(device, utf8Text);
		Marshal.FreeHGlobal((IntPtr) utf8Text);
	}

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	private static extern unsafe void FNA3D_SetTextureName(
		IntPtr device,
		IntPtr texture,
		byte*  text
	);

	public static unsafe void FNA3D_SetTextureName(
		IntPtr device,
		IntPtr texture,
		string text
	)
	{
		byte* utf8Text = SDL2.SDL.Utf8EncodeHeap(text);
		FNA3D_SetTextureName(device, texture, utf8Text);
		Marshal.FreeHGlobal((IntPtr) utf8Text);
	}
#endregion

#region Image Read API
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int FNA3D_Image_ReadFunc(
		IntPtr context,
		IntPtr data,
		int    size
	);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void FNA3D_Image_SkipFunc(
		IntPtr context,
		int    n
	);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int FNA3D_Image_EOFFunc(IntPtr context);

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr FNA3D_Image_Load(
		FNA3D_Image_ReadFunc readFunc,
		FNA3D_Image_SkipFunc skipFunc,
		FNA3D_Image_EOFFunc  eofFunc,
		IntPtr               context,
		out int              width,
		out int              height,
		out int              len,
		int                  forceW,
		int                  forceH,
		byte                 zoom
	);

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	public static extern void FNA3D_Image_Free(IntPtr mem);

	[ObjCRuntime.MonoPInvokeCallback(typeof(FNA3D_Image_ReadFunc))]
	private static int INTERNAL_Read(
		IntPtr context,
		IntPtr data,
		int    size
	)
	{
		Stream stream;
		lock (readStreams)
		{
			stream = readStreams[context];
		}
		byte[] buf    = new byte[size]; // FIXME: Preallocate!
		int    result = stream.Read(buf, 0, size);
		Marshal.Copy(buf, 0, data, result);
		return result;
	}

	[ObjCRuntime.MonoPInvokeCallback(typeof(FNA3D_Image_SkipFunc))]
	private static void INTERNAL_Skip(IntPtr context, int n)
	{
		Stream stream;
		lock (readStreams)
		{
			stream = readStreams[context];
		}
		stream.Seek(n, SeekOrigin.Current);
	}

	[ObjCRuntime.MonoPInvokeCallback(typeof(FNA3D_Image_EOFFunc))]
	private static int INTERNAL_EOF(IntPtr context)
	{
		Stream stream;
		lock (readStreams)
		{
			stream = readStreams[context];
		}
		return (stream.Position == stream.Length) ? 1 : 0;
	}

	private static FNA3D_Image_ReadFunc readFunc = INTERNAL_Read;
	private static FNA3D_Image_SkipFunc skipFunc = INTERNAL_Skip;
	private static FNA3D_Image_EOFFunc  eofFunc  = INTERNAL_EOF;

	private static int readGlobal = 0;

	private static System.Collections.Generic.Dictionary<IntPtr, Stream> readStreams =
		new System.Collections.Generic.Dictionary<IntPtr, Stream>();

	public static IntPtr ReadImageStream(
		Stream  stream,
		out int width,
		out int height,
		out int len,
		int     forceW = -1,
		int     forceH = -1,
		bool    zoom   = false
	)
	{
		IntPtr context;
		lock (readStreams)
		{
			context = (IntPtr) readGlobal++;
			readStreams.Add(context, stream);
		}
		IntPtr pixels = FNA3D_Image_Load(
			readFunc,
			skipFunc,
			eofFunc,
			context,
			out width,
			out height,
			out len,
			forceW,
			forceH,
			(byte) (zoom ? 1 : 0)
		);
		lock (readStreams)
		{
			readStreams.Remove(context);
		}
		return pixels;
	}
#endregion

#region Image Write API
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void FNA3D_Image_WriteFunc(
		IntPtr context,
		IntPtr data,
		int    size
	);

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	private static extern void FNA3D_Image_SavePNG(
		FNA3D_Image_WriteFunc writeFunc,
		IntPtr                context,
		int                   srcW,
		int                   srcH,
		int                   dstW,
		int                   dstH,
		IntPtr                data
	);

	[DllImport(native_lib_name, CallingConvention = CallingConvention.Cdecl)]
	private static extern void FNA3D_Image_SaveJPG(
		FNA3D_Image_WriteFunc writeFunc,
		IntPtr                context,
		int                   srcW,
		int                   srcH,
		int                   dstW,
		int                   dstH,
		IntPtr                data,
		int                   quality
	);

	[ObjCRuntime.MonoPInvokeCallback(typeof(FNA3D_Image_WriteFunc))]
	private static void INTERNAL_Write(
		IntPtr context,
		IntPtr data,
		int    size
	)
	{
		Stream stream;
		lock (writeStreams)
		{
			stream = writeStreams[context];
		}
		byte[] buf = new byte[size]; // FIXME: Preallocate!
		Marshal.Copy(data, buf, 0, size);
		stream.Write(buf, 0, size);
	}

	private static FNA3D_Image_WriteFunc writeFunc = INTERNAL_Write;

	private static int writeGlobal = 0;

	private static System.Collections.Generic.Dictionary<IntPtr, Stream> writeStreams =
		new System.Collections.Generic.Dictionary<IntPtr, Stream>();

	public static void WritePNGStream(
		Stream stream,
		int    srcW,
		int    srcH,
		int    dstW,
		int    dstH,
		IntPtr data
	)
	{
		IntPtr context;
		lock (writeStreams)
		{
			context = (IntPtr) writeGlobal++;
			writeStreams.Add(context, stream);
		}
		FNA3D_Image_SavePNG(
			writeFunc,
			context,
			srcW,
			srcH,
			dstW,
			dstH,
			data
		);
		lock (writeStreams)
		{
			writeStreams.Remove(context);
		}
	}

	public static void WriteJPGStream(
		Stream stream,
		int    srcW,
		int    srcH,
		int    dstW,
		int    dstH,
		IntPtr data,
		int    quality
	)
	{
		IntPtr context;
		lock (writeStreams)
		{
			context = (IntPtr) writeGlobal++;
			writeStreams.Add(context, stream);
		}
		FNA3D_Image_SaveJPG(
			writeFunc,
			context,
			srcW,
			srcH,
			dstW,
			dstH,
			data,
			quality
		);
		lock (writeStreams)
		{
			writeStreams.Remove(context);
		}
	}
#endregion
}
