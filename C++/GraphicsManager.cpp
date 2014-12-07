#include "stdafx.h"
#include <gl/gl.h>
#include <gl/glu.h>
#include "GraphicsManager.h"

HDC			_HDC = nullptr;		// Private GDI Device Context
HGLRC		_HDR = nullptr;		// Permanent Rendering Context
HWND		_HWnd = nullptr;		// Holds Our Window Handle
HINSTANCE	hInstance;		// Holds The Instance Of The Application
bool _HasFocus;

int _LastMouseX;
int _LastMouseY;

void(_stdcall *_CallBackMouseMove)(int32_t, int32_t) = nullptr;
void(_stdcall *_CallBackKeyDown)(int32_t) = nullptr;
void(_stdcall *_CallBackKeyUp)(int32_t) = nullptr;

LRESULT	CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

GraphicsManager::GraphicsManager()
{
	_VertexList = new double[20000000];
	_ColourList = new uint8_t[20000000];
}

GraphicsManager::~GraphicsManager()
{
	delete [] _VertexList;
	delete [] _ColourList;
}

void GraphicsManager::SetMouseMoveCallback(void(_stdcall *callBack)(int32_t, int32_t))
{
	_CallBackMouseMove = callBack;
}

void GraphicsManager::SetKeyDownCallback(void(_stdcall *callBack)(int32_t))
{
	_CallBackKeyDown = callBack;
}

void GraphicsManager::SetKeyUpCallback(void(_stdcall *callBack)(int32_t))
{
	_CallBackKeyUp = callBack;
}

void GraphicsManager::Init(int32_t width, int32_t height, int32_t handle)
{
	_Width = width;
	_Height = height;
	WNDCLASSEX windowClass = { 0 };
	hInstance = (HINSTANCE) handle;
	windowClass.cbSize = sizeof(WNDCLASSEX);
	windowClass.lpfnWndProc = WndProc;
	windowClass.style = CS_HREDRAW | CS_VREDRAW;
	windowClass.cbClsExtra = 0;
	windowClass.cbWndExtra = 0;
	windowClass.hInstance = hInstance;
	windowClass.hbrBackground = 0;
	windowClass.lpszClassName = L"0";
	RegisterClassEx(&windowClass);
	_HWnd = CreateWindowEx(0, L"0", L"LD31", WS_OVERLAPPEDWINDOW, 0, 0, _Width, _Height, 0, 0, hInstance, 0);
	ShowWindow(_HWnd, SW_SHOW);
}

void GraphicsManager::BeginDraw()
{
	if (!_GLStatesSetup)SetupGLStates();

	glLoadIdentity();

	glRotatef(_CameraRotX, 1, 0, 0);
	glRotatef(_CameraRotZ, 0, 1, 0);
	glTranslatef(-_CameraPosX, -_CameraPosY, -_CameraPosZ);
	_TriCount = 0;
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
	//	glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
}

void GraphicsManager::EndDraw()
{
	glVertexPointer(3, GL_DOUBLE, 0, _VertexList);
	glColorPointer(4, GL_UNSIGNED_BYTE, 0, _ColourList);
	glDrawArrays(GL_TRIANGLES, 0, _TriCount * 3);
	glEnd();
	SwapBuffers(_HDC);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PIXELFORMATDESCRIPTOR pfd;
	switch (message)
	{
		case WM_CREATE:
			_HDC = GetDC(hwnd);
			int nPixelFormat;
			pfd = { sizeof(PIXELFORMATDESCRIPTOR), 1, PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, PFD_MAIN_PLANE, 0, 0, 0, 0 };
			nPixelFormat = ChoosePixelFormat(_HDC, &pfd);
			SetPixelFormat(_HDC, nPixelFormat, &pfd);
			_HDR = wglCreateContext(_HDC);
			wglMakeCurrent(_HDC, _HDR);
			break;
		case WM_CLOSE:
			PostQuitMessage(0);
			return 0;
			break;
		case WM_SETFOCUS:
			_HasFocus = true;
			break;
		case WM_KILLFOCUS:
			_HasFocus = false;
			break;
		case WM_ACTIVATE:
			if (LOWORD(message) == WA_ACTIVE)
				_HasFocus = true;
			else
				_HasFocus = false;
			break;
	}
	return (DefWindowProc(hwnd, message, wParam, lParam));
}

void GraphicsManager::Update()
{
	MSG		msg;
	while (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
	{
		switch (msg.message)
		{
			case WM_MOUSEMOVE:
			{
				POINT point = POINT();
				GetCursorPos(&point);

				int32_t xPos = point.x;
				int32_t yPos = point.y;

				if (xPos != _LastMouseX || yPos != _LastMouseY)
				{
					_LastMouseX = xPos;
					_LastMouseY = yPos;
					if (_HasFocus)_CallBackMouseMove(xPos - (_Width*0.5f), yPos - (_Height*0.5f));
				}
			}
				break;
			case WM_KEYDOWN:
			{
				int a = (int) msg.wParam;
				if (_HasFocus) _CallBackKeyDown(a);
			}
				break;
			case WM_KEYUP:
			{
				int a = (int) msg.wParam;
				if (_HasFocus) _CallBackKeyUp(a);
			}
				break;
		}
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	if (_HasFocus)SetCursorPos(static_cast<int>(_Width*0.5f), static_cast<int>(_Height*0.5f));
}

void GraphicsManager::Destroy()
{

}

void GraphicsManager::SetupGLStates()
{
	glViewport(0, 0, _Width, _Height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(70, _Width / (float) _Height, 2, 2300);
	glClearColor(0, 0, 0, 1);
	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_COLOR_ARRAY);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_LIGHTING);
	glEnable(GL_BLEND);
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);
	glDepthFunc(GL_LEQUAL);
	glMatrixMode(GL_MODELVIEW);
	glCullFace(GL_FRONT);
	_GLStatesSetup = true;
}

void GraphicsManager::DrawTri(double* p1, double* p2, double* p3, int* arrayPosition)
{
	memcpy_s(_VertexList + (*arrayPosition), sizeof(double) * 3, p1, sizeof(double) * 3);	(*arrayPosition) += 3;
	memcpy_s(_VertexList + (*arrayPosition), sizeof(double) * 3, p2, sizeof(double) * 3);	(*arrayPosition) += 3;
	memcpy_s(_VertexList + (*arrayPosition), sizeof(double) * 3, p3, sizeof(double) * 3);	(*arrayPosition) += 3;
}

void GraphicsManager::DrawVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t colourA, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ)
{
	int vertexArrayStart = _TriCount * 3 * 3;
	int colourArrayStart = _TriCount * 3 * 4;
	int verpos = vertexArrayStart;

	double halfSizeX = sizeX*0.5f;
	double halfSizeY = sizeY*0.5f;
	double halfSizeZ = sizeZ*0.5f;

	double _tlf[3] = { x - halfSizeX, y - halfSizeY, z - halfSizeZ };
	double _trf[3] = { x + halfSizeX, y - halfSizeY, z - halfSizeZ };
	double _blf[3] = { x - halfSizeX, y + halfSizeY, z - halfSizeZ };
	double _brf[3] = { x + halfSizeX, y + halfSizeY, z - halfSizeZ };
	double _tlb[3] = { x - halfSizeX, y - halfSizeY, z + halfSizeZ };
	double _trb[3] = { x + halfSizeX, y - halfSizeY, z + halfSizeZ };
	double _blb[3] = { x - halfSizeX, y + halfSizeY, z + halfSizeZ };
	double _brb[3] = { x + halfSizeX, y + halfSizeY, z + halfSizeZ };

	uint8_t colourArray[4] = { colourR, colourG, colourB, colourA };

	for (int i = 0; i < FACESPERCUBE* VERTSPERFACE; i++)memcpy_s(_ColourList + colourArrayStart + (i * 4), sizeof(uint8_t) * 4, colourArray, sizeof(uint8_t) * 4);

	//FRONT
	DrawTri(_tlf, _blf, _trf, &verpos);
	DrawTri(_blf, _brf, _trf, &verpos);

	//LEFT
	DrawTri(_tlb, _blb, _tlf, &verpos);
	DrawTri(_blb, _blf, _tlf, &verpos);

	//RIGHT
	DrawTri(_trb, _trf, _brb, &verpos);
	DrawTri(_brf, _brb, _trf, &verpos);

	//Back
	DrawTri(_blb, _tlb, _trb, &verpos);
	DrawTri(_blb, _trb, _brb, &verpos);

	//Top
	DrawTri(_tlb, _tlf, _trb, &verpos);
	DrawTri(_tlf, _trf, _trb, &verpos);

	//Bottom
	DrawTri(_blb, _brb, _blf, &verpos);
	DrawTri(_blf, _brb, _brf, &verpos);

	_TriCount += 12;
}

void GraphicsManager::SerCameraPosition(double x, double y, double z)
{
	_CameraPosX = x;
	_CameraPosY = y;
	_CameraPosZ = z;
}
void GraphicsManager::SetCameraRotation(double z, double x)
{
	_CameraRotZ = z;
	_CameraRotX = x;
}
