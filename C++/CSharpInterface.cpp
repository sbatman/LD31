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
}