#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2025 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

using System;
using static Microsoft.Xna.Framework.ThreadCheck;

namespace Microsoft.Xna.Framework.Graphics
{
	// tModLoader(thread-safety): Implement wrappers for thread safety.
	public static partial class FNA3D
	{
		public static uint FNA3D_PrepareWindowAttributes() { CheckThread(); return FNA3D_Impl.FNA3D_PrepareWindowAttributes(); }

		public static void FNA3D_GetDrawableSize(IntPtr window, out int w, out int h) { CheckThread(); FNA3D_Impl.FNA3D_GetDrawableSize(window, out w, out h); }

		public static IntPtr FNA3D_CreateDevice(ref FNA3D_PresentationParameters presentationParameters, byte debugMode) { CheckThread(); return FNA3D_Impl.FNA3D_CreateDevice(ref presentationParameters, debugMode); }

		public static void FNA3D_DestroyDevice(IntPtr device) { CheckThread(); FNA3D_Impl.FNA3D_DestroyDevice(device); }

		public static void FNA3D_SwapBuffers(IntPtr device, ref Rectangle sourceRectangle, ref Rectangle destinationRectangle, IntPtr overrideWindowHandle) { CheckThread(); FNA3D_Impl.FNA3D_SwapBuffers(device, ref sourceRectangle, ref destinationRectangle, overrideWindowHandle); }

		public static void FNA3D_SwapBuffers(IntPtr device, IntPtr sourceRectangle, IntPtr destinationRectangle, IntPtr overrideWindowHandle) { CheckThread(); FNA3D_Impl.FNA3D_SwapBuffers(device, sourceRectangle, destinationRectangle, overrideWindowHandle); }

		public static void FNA3D_SwapBuffers(IntPtr device, ref Rectangle sourceRectangle, IntPtr destinationRectangle, IntPtr overrideWindowHandle) { CheckThread(); FNA3D_Impl.FNA3D_SwapBuffers(device, ref sourceRectangle, destinationRectangle, overrideWindowHandle); }

		public static void FNA3D_SwapBuffers(IntPtr device, IntPtr sourceRectangle, ref Rectangle destinationRectangle, IntPtr overrideWindowHandle) { CheckThread(); FNA3D_Impl.FNA3D_SwapBuffers(device, sourceRectangle, ref destinationRectangle, overrideWindowHandle); }

		public static void FNA3D_Clear(IntPtr device, ClearOptions options, ref Vector4 color, float depth, int stencil) { CheckThread(); FNA3D_Impl.FNA3D_Clear(device, options, ref color, depth, stencil); }

		public static void FNA3D_DrawIndexedPrimitives(IntPtr device, PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, IntPtr indices, IndexElementSize indexElementSize) { CheckThread(); FNA3D_Impl.FNA3D_DrawIndexedPrimitives(device, primitiveType, baseVertex, minVertexIndex, numVertices, startIndex, primitiveCount, indices, indexElementSize); }

		public static void FNA3D_DrawInstancedPrimitives(IntPtr device, PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount, IntPtr indices, IndexElementSize indexElementSize) { CheckThread(); FNA3D_Impl.FNA3D_DrawInstancedPrimitives(device, primitiveType, baseVertex, minVertexIndex, numVertices, startIndex, primitiveCount, instanceCount, indices, indexElementSize); }

		public static void FNA3D_DrawPrimitives(IntPtr device, PrimitiveType primitiveType, int vertexStart, int primitiveCount) { CheckThread(); FNA3D_Impl.FNA3D_DrawPrimitives(device, primitiveType, vertexStart, primitiveCount); }

		public static void FNA3D_SetViewport(IntPtr device, ref FNA3D_Viewport viewport) { CheckThread(); FNA3D_Impl.FNA3D_SetViewport(device, ref viewport); }

		public static void FNA3D_SetScissorRect(IntPtr device, ref Rectangle scissor) { CheckThread(); FNA3D_Impl.FNA3D_SetScissorRect(device, ref scissor); }

		public static void FNA3D_GetBlendFactor(IntPtr device, out Color blendFactor) { CheckThread(); FNA3D_Impl.FNA3D_GetBlendFactor(device, out blendFactor); }

		public static void FNA3D_SetBlendFactor(IntPtr device, ref Color blendFactor) { CheckThread(); FNA3D_Impl.FNA3D_SetBlendFactor(device, ref blendFactor); }

		public static int FNA3D_GetMultiSampleMask(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_GetMultiSampleMask(device); }

		public static void FNA3D_SetMultiSampleMask(IntPtr device, int mask) { CheckThread(); FNA3D_Impl.FNA3D_SetMultiSampleMask(device, mask); }

		public static int FNA3D_GetReferenceStencil(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_GetReferenceStencil(device); }

		public static void FNA3D_SetReferenceStencil(IntPtr device, int reference) { CheckThread(); FNA3D_Impl.FNA3D_SetReferenceStencil(device, reference); }

		public static void FNA3D_SetBlendState(IntPtr device, ref FNA3D_BlendState blendState) { CheckThread(); FNA3D_Impl.FNA3D_SetBlendState(device, ref blendState); }

		public static void FNA3D_SetDepthStencilState(IntPtr device, ref FNA3D_DepthStencilState depthStencilState) { CheckThread(); FNA3D_Impl.FNA3D_SetDepthStencilState(device, ref depthStencilState); }

		public static void FNA3D_ApplyRasterizerState(IntPtr device, ref FNA3D_RasterizerState rasterizerState) { CheckThread(); FNA3D_Impl.FNA3D_ApplyRasterizerState(device, ref rasterizerState); }

		public static void FNA3D_VerifySampler(IntPtr device, int index, IntPtr texture, ref FNA3D_SamplerState sampler) { CheckThread(); FNA3D_Impl.FNA3D_VerifySampler(device, index, texture, ref sampler); }

		public static void FNA3D_VerifyVertexSampler(IntPtr device, int index, IntPtr texture, ref FNA3D_SamplerState sampler) { CheckThread(); FNA3D_Impl.FNA3D_VerifyVertexSampler(device, index, texture, ref sampler); }

		public static unsafe void FNA3D_ApplyVertexBufferBindings(IntPtr device, FNA3D_VertexBufferBinding* bindings, int numBindings, byte bindingsUpdated, int baseVertex) { CheckThread(); FNA3D_Impl.FNA3D_ApplyVertexBufferBindings(device, bindings, numBindings, bindingsUpdated, baseVertex); }

		public static void FNA3D_SetRenderTargets(IntPtr device, IntPtr renderTargets, int numRenderTargets, IntPtr depthStencilBuffer, DepthFormat depthFormat, byte preserveDepthStencilContents) { CheckThread(); FNA3D_Impl.FNA3D_SetRenderTargets(device, renderTargets, numRenderTargets, depthStencilBuffer, depthFormat, preserveDepthStencilContents); }

		public static unsafe void FNA3D_SetRenderTargets(IntPtr device, FNA3D_RenderTargetBinding* renderTargets, int numRenderTargets, IntPtr depthStencilBuffer, DepthFormat depthFormat, byte preserveDepthStencilContents) { CheckThread(); FNA3D_Impl.FNA3D_SetRenderTargets(device, renderTargets, numRenderTargets, depthStencilBuffer, depthFormat, preserveDepthStencilContents); }

		public static void FNA3D_ResolveTarget(IntPtr device, ref FNA3D_RenderTargetBinding target) { CheckThread(); FNA3D_Impl.FNA3D_ResolveTarget(device, ref target); }

		public static void FNA3D_ResetBackbuffer(IntPtr device, ref FNA3D_PresentationParameters presentationParameters) { CheckThread(); FNA3D_Impl.FNA3D_ResetBackbuffer(device, ref presentationParameters); }

		public static void FNA3D_ReadBackbuffer(IntPtr device, int x, int y, int w, int h, IntPtr data, int dataLen) { CheckThread(); FNA3D_Impl.FNA3D_ReadBackbuffer(device, x, y, w, h, data, dataLen); }

		public static void FNA3D_GetBackbufferSize(IntPtr device, out int w, out int h) { CheckThread(); FNA3D_Impl.FNA3D_GetBackbufferSize(device, out w, out h); }

		public static SurfaceFormat FNA3D_GetBackbufferSurfaceFormat(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_GetBackbufferSurfaceFormat(device); }

		public static DepthFormat FNA3D_GetBackbufferDepthFormat(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_GetBackbufferDepthFormat(device); }

		public static int FNA3D_GetBackbufferMultiSampleCount(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_GetBackbufferMultiSampleCount(device); }

		public static IntPtr FNA3D_CreateTexture2D(IntPtr device, SurfaceFormat format, int width, int height, int levelCount, byte isRenderTarget) { CheckThread(); return FNA3D_Impl.FNA3D_CreateTexture2D(device, format, width, height, levelCount, isRenderTarget); }

		public static IntPtr FNA3D_CreateTexture3D(IntPtr device, SurfaceFormat format, int width, int height, int depth, int levelCount) { CheckThread(); return FNA3D_Impl.FNA3D_CreateTexture3D(device, format, width, height, depth, levelCount); }

		public static IntPtr FNA3D_CreateTextureCube(IntPtr device, SurfaceFormat format, int size, int levelCount, byte isRenderTarget) { CheckThread(); return FNA3D_Impl.FNA3D_CreateTextureCube(device, format, size, levelCount, isRenderTarget); }

		public static void FNA3D_AddDisposeTexture(IntPtr device, IntPtr texture) { CheckThread(); FNA3D_Impl.FNA3D_AddDisposeTexture(device, texture); }

		public static void FNA3D_SetTextureData2D(IntPtr device, IntPtr texture, int x, int y, int w, int h, int level, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_SetTextureData2D(device, texture, x, y, w, h, level, data, dataLength); }

		public static void FNA3D_SetTextureData3D(IntPtr device, IntPtr texture, int x, int y, int z, int w, int h, int d, int level, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_SetTextureData3D(device, texture, x, y, z, w, h, d, level, data, dataLength); }

		public static void FNA3D_SetTextureDataCube(IntPtr device, IntPtr texture, int x, int y, int w, int h, CubeMapFace cubeMapFace, int level, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_SetTextureDataCube(device, texture, x, y, w, h, cubeMapFace, level, data, dataLength); }

		public static void FNA3D_SetTextureDataYUV(IntPtr device, IntPtr y, IntPtr u, IntPtr v, int yWidth, int yHeight, int uvWidth, int uvHeight, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_SetTextureDataYUV(device, y, u, v, yWidth, yHeight, uvWidth, uvHeight, data, dataLength); }

		public static void FNA3D_GetTextureData2D(IntPtr device, IntPtr texture, int x, int y, int w, int h, int level, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_GetTextureData2D(device, texture, x, y, w, h, level, data, dataLength); }

		public static void FNA3D_GetTextureData3D(IntPtr device, IntPtr texture, int x, int y, int z, int w, int h, int d, int level, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_GetTextureData3D(device, texture, x, y, z, w, h, d, level, data, dataLength); }

		public static void FNA3D_GetTextureDataCube(IntPtr device, IntPtr texture, int x, int y, int w, int h, CubeMapFace cubeMapFace, int level, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_GetTextureDataCube(device, texture, x, y, w, h, cubeMapFace, level, data, dataLength); }

		public static IntPtr FNA3D_GenColorRenderbuffer(IntPtr device, int width, int height, SurfaceFormat format, int multiSampleCount, IntPtr texture) { CheckThread(); return FNA3D_Impl.FNA3D_GenColorRenderbuffer(device, width, height, format, multiSampleCount, texture); }

		public static IntPtr FNA3D_GenDepthStencilRenderbuffer(IntPtr device, int width, int height, DepthFormat format, int multiSampleCount) { CheckThread(); return FNA3D_Impl.FNA3D_GenDepthStencilRenderbuffer(device, width, height, format, multiSampleCount); }

		public static void FNA3D_AddDisposeRenderbuffer(IntPtr device, IntPtr renderbuffer) { CheckThread(); FNA3D_Impl.FNA3D_AddDisposeRenderbuffer(device, renderbuffer); }

		public static IntPtr FNA3D_GenVertexBuffer(IntPtr device, byte dynamic, BufferUsage usage, int sizeInBytes) { CheckThread(); return FNA3D_Impl.FNA3D_GenVertexBuffer(device, dynamic, usage, sizeInBytes); }

		public static void FNA3D_AddDisposeVertexBuffer(IntPtr device, IntPtr buffer) { CheckThread(); FNA3D_Impl.FNA3D_AddDisposeVertexBuffer(device, buffer); }

		public static void FNA3D_SetVertexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int elementCount, int elementSizeInBytes, int vertexStride, SetDataOptions options) { CheckThread(); FNA3D_Impl.FNA3D_SetVertexBufferData(device, buffer, offsetInBytes, data, elementCount, elementSizeInBytes, vertexStride, options); }

		public static void FNA3D_GetVertexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int elementCount, int elementSizeInBytes, int vertexStride) { CheckThread(); FNA3D_Impl.FNA3D_GetVertexBufferData(device, buffer, offsetInBytes, data, elementCount, elementSizeInBytes, vertexStride); }

		public static IntPtr FNA3D_GenIndexBuffer(IntPtr device, byte dynamic, BufferUsage usage, int sizeInBytes) { CheckThread(); return FNA3D_Impl.FNA3D_GenIndexBuffer(device, dynamic, usage, sizeInBytes); }

		public static void FNA3D_AddDisposeIndexBuffer(IntPtr device, IntPtr buffer) { CheckThread(); FNA3D_Impl.FNA3D_AddDisposeIndexBuffer(device, buffer); }

		public static void FNA3D_SetIndexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int dataLength, SetDataOptions options) { CheckThread(); FNA3D_Impl.FNA3D_SetIndexBufferData(device, buffer, offsetInBytes, data, dataLength, options); }

		public static void FNA3D_GetIndexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int dataLength) { CheckThread(); FNA3D_Impl.FNA3D_GetIndexBufferData(device, buffer, offsetInBytes, data, dataLength); }

		public static void FNA3D_CreateEffect(IntPtr device, byte[] effectCode, int length, out IntPtr effect, out IntPtr effectData) { CheckThread(); FNA3D_Impl.FNA3D_CreateEffect(device, effectCode, length, out effect, out effectData); }

		public static void FNA3D_CloneEffect(IntPtr device, IntPtr cloneSource, out IntPtr effect, out IntPtr effectData) { CheckThread(); FNA3D_Impl.FNA3D_CloneEffect(device, cloneSource, out effect, out effectData); }

		public static void FNA3D_AddDisposeEffect(IntPtr device, IntPtr effect) { CheckThread(); FNA3D_Impl.FNA3D_AddDisposeEffect(device, effect); }

		public static void FNA3D_SetEffectTechnique(IntPtr device, IntPtr effect, IntPtr technique) { CheckThread(); FNA3D_Impl.FNA3D_SetEffectTechnique(device, effect, technique); }

		public static void FNA3D_ApplyEffect(IntPtr device, IntPtr effect, uint pass, IntPtr stateChanges) { CheckThread(); FNA3D_Impl.FNA3D_ApplyEffect(device, effect, pass, stateChanges); }

		public static void FNA3D_BeginPassRestore(IntPtr device, IntPtr effect, IntPtr stateChanges) { CheckThread(); FNA3D_Impl.FNA3D_BeginPassRestore(device, effect, stateChanges); }

		public static void FNA3D_EndPassRestore(IntPtr device, IntPtr effect) { CheckThread(); FNA3D_Impl.FNA3D_EndPassRestore(device, effect); }

		public static IntPtr FNA3D_CreateQuery(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_CreateQuery(device); }

		public static void FNA3D_AddDisposeQuery(IntPtr device, IntPtr query) { CheckThread(); FNA3D_Impl.FNA3D_AddDisposeQuery(device, query); }

		public static void FNA3D_QueryBegin(IntPtr device, IntPtr query) { CheckThread(); FNA3D_Impl.FNA3D_QueryBegin(device, query); }

		public static void FNA3D_QueryEnd(IntPtr device, IntPtr query) { CheckThread(); FNA3D_Impl.FNA3D_QueryEnd(device, query); }

		public static byte FNA3D_QueryComplete(IntPtr device, IntPtr query) { CheckThread(); return FNA3D_Impl.FNA3D_QueryComplete(device, query); }

		public static int FNA3D_QueryPixelCount(IntPtr device, IntPtr query) { CheckThread(); return FNA3D_Impl.FNA3D_QueryPixelCount(device, query); }

		public static byte FNA3D_SupportsDXT1(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_SupportsDXT1(device); }

		public static byte FNA3D_SupportsS3TC(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_SupportsS3TC(device); }

		public static byte FNA3D_SupportsHardwareInstancing(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_SupportsHardwareInstancing(device); }

		public static byte FNA3D_SupportsNoOverwrite(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_SupportsNoOverwrite(device); }

		public static void FNA3D_GetMaxTextureSlots(IntPtr device, out int textures, out int vertexTextures) { CheckThread(); FNA3D_Impl.FNA3D_GetMaxTextureSlots(device, out textures, out vertexTextures); }

		public static int FNA3D_GetMaxMultiSampleCount(IntPtr device, SurfaceFormat format, int preferredMultiSampleCount) { CheckThread(); return FNA3D_Impl.FNA3D_GetMaxMultiSampleCount(device, format, preferredMultiSampleCount); }

		public static byte FNA3D_SupportsSRGBRenderTargets(IntPtr device) { CheckThread(); return FNA3D_Impl.FNA3D_SupportsSRGBRenderTargets(device); }
	}
}
