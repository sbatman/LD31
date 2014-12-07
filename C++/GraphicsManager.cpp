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
	_NormalList = new double[20000000];
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
	glNormalPointer(GL_DOUBLE, 0, _NormalList);
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
	glShadeModel(GL_SMOOTH);
	glViewport(0, 0, _Width, _Height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(70, _Width / (float) _Height, 3, 2500);
	glClearColor(0.2, 0.6, 0.8, 1);
	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_COLOR_ARRAY);
	glEnableClientState(GL_NORMAL_ARRAY);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	// Somewhere in the initialization part of your program…
	glEnable(GL_LIGHTING);

	// Create light components
	GLfloat ambientLight [] = { 0.2f, 0.2f, 0.2f, 1.0f };
	GLfloat diffuseLight [] = { 0.8f, 0.8f, 0.8, 1.0f };
	GLfloat specularLight [] = { 0.2f, 0.2f, 0.2f, 1.0f };
	GLfloat position [] = { 0, 1, 0.5, 0 };

	// Assign created components to GL_LIGHT0
	glLightfv(GL_LIGHT0, GL_AMBIENT, ambientLight);
	glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuseLight);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specularLight);
	glLightfv(GL_LIGHT0, GL_POSITION, position);
	glEnable(GL_LIGHT0);


	GLfloat fogColor[4] = { 0.3, 0.7, 0.9, 1.0f };      // Fog Color


	glFogi(GL_FOG_MODE, GL_EXP);        // Fog Mode
	glFogfv(GL_FOG_COLOR, fogColor);            // Set Fog Color
	glFogf(GL_FOG_DENSITY, 0.004f);              // How Dense Will The Fog Be
	glHint(GL_FOG_HINT, GL_DONT_CARE);          // Fog Hint Value
	glFogf(GL_FOG_START, 1.0f);             // Fog Start Depth
	glFogf(GL_FOG_END, 2500);               // Fog End Depth
	glEnable(GL_FOG);                   // Enables GL_FOG

	// enable color tracking
	glEnable(GL_COLOR_MATERIAL);
	// set material properties which will be assigned by glColor
	glColorMaterial(GL_FRONT, GL_AMBIENT_AND_DIFFUSE);

	float specReflection [] = { 0.8f, 0.8f, 0.8f, 1.0f };
	glMaterialfv(GL_FRONT, GL_SPECULAR, specReflection);
	glMateriali(GL_FRONT, GL_SHININESS, 96);

	glEnable(GL_BLEND);
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);
	glDepthFunc(GL_LEQUAL);
	glMatrixMode(GL_MODELVIEW);
	glCullFace(GL_BACK);
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

	
	double _FrontNormals[9] = { 0, 0, -1, 0, 0, -1, 0, 0, -1 };
	double _LeftNormals[9] = { -1, 0, 0, -1, 0, 0, -1, 0, 0 };
	double _RightNormals[9] = { 1, 0, 0, 1, 0, 0, 1, 0, 0 };
	double _BackNormals[9] = { 0, 0, 1, 0, 0, 1, 0, 0, 1 };
	double _TopNormals[9] = { 0, -1, 0, 0, -1, 0, 0, -1, 0 };
	double _BottomNormals[9] = { 0, 1, 0, 0, 1, 0, 0, 1, 0 };

	//FRONT
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _FrontNormals, sizeof(double) * 9);
	DrawTri(_tlf, _blf, _trf, &verpos);
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _FrontNormals, sizeof(double) * 9);
	DrawTri(_blf, _brf, _trf, &verpos);

	//LEFT	
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _LeftNormals, sizeof(double) * 9);
	DrawTri(_tlb, _blb, _tlf, &verpos);
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _LeftNormals, sizeof(double) * 9);
	DrawTri(_blb, _blf, _tlf, &verpos);

	//RIGHT	
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _RightNormals, sizeof(double) * 9);
	DrawTri(_trb, _trf, _brb, &verpos);
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _RightNormals, sizeof(double) * 9);
	DrawTri(_brf, _brb, _trf, &verpos);

	//Back	
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _BackNormals, sizeof(double) * 9);
	DrawTri(_blb, _tlb, _trb, &verpos);
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _BackNormals, sizeof(double) * 9);
	DrawTri(_blb, _trb, _brb, &verpos);

	//Top	
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _TopNormals, sizeof(double) * 9);
	DrawTri(_tlb, _tlf, _trb, &verpos);
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _TopNormals, sizeof(double) * 9);
	DrawTri(_tlf, _trf, _trb, &verpos);

	//Bottom	
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _BottomNormals, sizeof(double) * 9);
	DrawTri(_blb, _brb, _blf, &verpos);
	memcpy_s(_NormalList + (verpos), sizeof(double) * 9, _BottomNormals, sizeof(double) * 9);
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
