#include "stdafx.h"
#include <gl/gl.h>
#include "GraphicsManager.h"

HDC			hDC = NULL;		// Private GDI Device Context
HGLRC		hRC = NULL;		// Permanent Rendering Context
HWND		hWnd = NULL;		// Holds Our Window Handle
HINSTANCE	hInstance;		// Holds The Instance Of The Application

int Width;
int Height;

LRESULT	CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

GraphicsManager::GraphicsManager()
{
}


GraphicsManager::~GraphicsManager()
{
}

void GraphicsManager::Init(int32_t width, int32_t height, int32_t handle)
{
	Width=width;
	Height=height;
	WNDCLASSEX windowClass;
	hInstance = (HINSTANCE)handle;
	windowClass.cbSize = sizeof(WNDCLASSEX);
	windowClass.lpfnWndProc = WndProc;
	windowClass.style = CS_HREDRAW | CS_VREDRAW;
	windowClass.cbClsExtra = 0;
	windowClass.cbWndExtra = 0;
	windowClass.hInstance = hInstance;
	windowClass.hbrBackground = 0;
	windowClass.lpszClassName = L"0";
	RegisterClassEx(&windowClass);
	hWnd = CreateWindowEx(0, L"0", L"0", WS_OVERLAPPEDWINDOW, 0, 0, Width, Height, 0, 0, hInstance, 0);
	ShowWindow(hWnd, SW_SHOW);
}

void GraphicsManager::BeginDraw()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT		);
}

void GraphicsManager::EndDraw()
{
	glEnd();
	SwapBuffers(hDC);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PIXELFORMATDESCRIPTOR pfd;
	switch (message)
	{
		case WM_CREATE:
			hDC = GetDC(hwnd);
			int nPixelFormat;
			pfd = { sizeof(PIXELFORMATDESCRIPTOR), 1, PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, PFD_MAIN_PLANE, 0, 0, 0, 0 };
			nPixelFormat = ChoosePixelFormat(hDC, &pfd);
			SetPixelFormat(hDC, nPixelFormat, &pfd);
			hRC = wglCreateContext(hDC);
			wglMakeCurrent(hDC, hRC);
			glViewport(0, 0, Width, Height);
			glClearColor(255,0,0,255);
			break;
		case WM_CLOSE:
			PostQuitMessage(0);
			return 0;
			break;
	}
	return (DefWindowProc(hwnd, message, wParam, lParam));
}

void GraphicsManager::Update()
{
	MSG		msg;
	while (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}