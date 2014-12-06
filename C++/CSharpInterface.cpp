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
}