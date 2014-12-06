#include "stdafx.h"
#include "GraphicsManager.h"

GraphicsManager * _GMInstance = nullptr;

extern "C" {
	__declspec(dllexport) void __cdecl GraphicsManagerInit(int32_t width, int32_t height, int32_t handle)
	{
		if (_GMInstance == nullptr)_GMInstance = new GraphicsManager();

		_GMInstance->Init(width, height, handle);
	}
	__declspec(dllexport) void __cdecl GraphicsManagerUpdate()
	{
		_GMInstance->Update();
	}

	__declspec(dllexport) void __cdecl GraphicsManagerBeginDraw()
	{
		_GMInstance->BeginDraw();
	}

	__declspec(dllexport) void __cdecl GraphicsManagerEndDraw()
	{
		_GMInstance->EndDraw();
	}

	__declspec(dllexport) void __cdecl GraphicsManagerDestroy()
	{
		_GMInstance->Destroy();
		if (_GMInstance != nullptr)
		{
			delete _GMInstance;
			_GMInstance = nullptr;
		}
	}

	__declspec(dllexport) void __cdecl GraphicsManagerDrawVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t colourA, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ)
	{
		_GMInstance->DrawVoxel(x, y, z, colourR, colourG, colourB, colourA, sizeX, sizeY, sizeZ);
	}
}