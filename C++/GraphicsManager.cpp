#include "stdafx.h"
#include <gl/gl.h>
#include <gl/glu.h>
#include "Shader.h"
#include "GraphicsManager.h"


HDC			_HDC = nullptr;		// Private GDI Device Context
HGLRC		_HDR = nullptr;		// Permanent Rendering Context
HWND		_HWnd = nullptr;		// Holds Our Window Handle
HINSTANCE	hInstance;		// Holds The Instance Of The Application
bool _HasFocus;

int _LastMouseX;
int _LastMouseY;

void(_stdcall *_CallBackMouseMove)(int32_t, int32_t) = nullptr;
void(_stdcall *_CallBackMousePress)(int32_t button) = nullptr;
void(_stdcall *_CallBackMouseRelease)(int32_t button) = nullptr;
void(_stdcall *_CallBackKeyDown)(int32_t) = nullptr;
void(_stdcall *_CallBackKeyUp)(int32_t) = nullptr;

LRESULT	CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

std::vector<Shader *> * _LoadedShaders;

//HFONT _Font = CreateFont(-24, 0, 0, 0, 0, ANSI_CHARSET, 0U, 0U, 0U, 0U, 0U, 0U, 0U, TEXT("Arial"));

void ErrorTest()
{
	int error = glGetError();
	if (error != 0)
	{
		printf("%s", gluErrorString(error));
		int g = 7;
		g = g*g;
	}
}

GraphicsManager::GraphicsManager()
{
	_LoadedShaders = new std::vector<Shader *>();
	_VertexList = new double[20000000];
	_NormalList = new double[20000000];
	_ColourList = new uint8_t[20000000];

	_TVertexList = new double[2000000];
	_TNormalList = new double[2000000];
	_TColourList = new uint8_t[2000000];

	_UIVertexList = new double[200000];
	_UIColourList = new uint8_t[200000];
}

GraphicsManager::~GraphicsManager()
{
	for (Shader * s : (*_LoadedShaders)) delete s;

	delete _LoadedShaders;
	delete[] _VertexList;
	delete[] _ColourList;
	delete[] _NormalList;

	delete[] _TVertexList;
	delete[] _TColourList;
	delete[] _TNormalList;

	delete[] _UIVertexList;
	delete[] _UIColourList;
}

void GraphicsManager::SetMouseMoveCallback(void(_stdcall *callBack)(int32_t, int32_t))
{
	_CallBackMouseMove = callBack;
}
void GraphicsManager::SetMousePressCallback(void(_stdcall *callBack)(int32_t))
{
	_CallBackMousePress = callBack;
}

void GraphicsManager::SetMouseReleaseCallback(void(_stdcall *callBack)(int32_t))
{
	_CallBackMouseRelease = callBack;
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
	_HWnd = CreateWindowEx(0, L"0", L"LD31", WS_OVERLAPPEDWINDOW, 0, 0, _Width, _Height, 0, 0, hInstance, 0);
	ShowWindow(_HWnd, SW_SHOW);
	ShowCursor(false);
}

void GraphicsManager::BeginDraw()
{
	if (!_GLStatesSetup)SetupGLStates();

	glLoadIdentity();

	glRotatef(_CameraRotX, 1, 0, 0);
	glRotatef(_CameraRotZ, 0, 1, 0);
	glTranslatef(-_CameraPosX, -_CameraPosY, -_CameraPosZ);
	_TriCount = 0;
	_UITriCount = 0;
	_TTriCount = 0;

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	//	glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);

}

void GraphicsManager::EndDraw()
{
	glVertexPointer(3, GL_DOUBLE, 0, _VertexList);
	glColorPointer(4, GL_UNSIGNED_BYTE, 0, _ColourList);
	glNormalPointer(GL_DOUBLE, 0, _NormalList);
	glDrawArrays(GL_TRIANGLES, 0, _TriCount * 3);

	if (_TTriCount > 0){
		glDepthMask(false);
		glVertexPointer(3, GL_DOUBLE, 0, _TVertexList);
		glColorPointer(4, GL_UNSIGNED_BYTE, 0, _TColourList);
		glNormalPointer(GL_DOUBLE, 0, _TNormalList);
		glDrawArrays(GL_TRIANGLES, 0, _TTriCount * 3);
		glDepthMask(true);
	}

	glPushMatrix();
	glLoadIdentity();

	if (_UITriCount > 0)
	{
		glClear(GL_DEPTH_BUFFER_BIT);
		glDisableClientState(GL_NORMAL_ARRAY);
		glVertexPointer(3, GL_DOUBLE, 0, _UIVertexList);
		glColorPointer(4, GL_UNSIGNED_BYTE, 0, _UIColourList);
		glDrawArrays(GL_TRIANGLES, 0, _UITriCount * 3);
		glEnableClientState(GL_NORMAL_ARRAY);
	}

	glPopMatrix();
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
		pfd = { sizeof(PIXELFORMATDESCRIPTOR), 1, PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, PFD_MAIN_PLANE, 0, 0, 0, 0 };
		pfd.iPixelType = PFD_TYPE_RGBA;
		pfd.cColorBits = static_cast<BYTE>(32);
		pfd.cDepthBits = static_cast<BYTE>(24);
		pfd.cStencilBits = static_cast<BYTE>(8);
		pfd.cAlphaBits = 8;
		nPixelFormat = ChoosePixelFormat(_HDC, &pfd);
		SetPixelFormat(_HDC, nPixelFormat, &pfd);
		_HDR = wglCreateContext(_HDC);
		wglMakeCurrent(_HDC, _HDR);

		glewInit();
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
			int a = (int)msg.wParam;
			if (_HasFocus) _CallBackKeyDown(a);
		}
			break;
		case WM_KEYUP:
		{
			int a = (int)msg.wParam;
			if (_HasFocus) _CallBackKeyUp(a);
		}
			break;

		case WM_LBUTTONDOWN: if (_HasFocus)_CallBackMousePress(0); break;
		case WM_LBUTTONUP: if (_HasFocus)_CallBackMouseRelease(0); break;
		case WM_MBUTTONDOWN: if (_HasFocus)_CallBackMousePress(2); break;
		case WM_MBUTTONUP: if (_HasFocus)_CallBackMouseRelease(2); break;
		case WM_RBUTTONDOWN: if (_HasFocus)_CallBackMousePress(1); break;
		case WM_RBUTTONUP: if (_HasFocus)_CallBackMouseRelease(1); break;

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
	gluPerspective(70, _Width / (float)_Height, 2, 2000);
	glClearColor(0.2, 0.6, 0.8, 1);
	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_COLOR_ARRAY);
	glEnableClientState(GL_NORMAL_ARRAY);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	// Somewhere in the initialization part of your program…
	glEnable(GL_LIGHTING);

	// Create light components
	GLfloat ambientLight [] = {0.2f, 0.2f, 0.2f, 1.0f};
	GLfloat diffuseLight [] = {0.8f, 0.8f, 0.8, 1.0f};
	GLfloat specularLight [] = {0.2f, 0.2f, 0.2f, 1.0f};
	GLfloat position [] = {0, 1, 0.5, 0};

	// Assign created components to GL_LIGHT0
	glLightfv(GL_LIGHT0, GL_AMBIENT, ambientLight);
	glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuseLight);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specularLight);
	glLightfv(GL_LIGHT0, GL_POSITION, position);
	glEnable(GL_LIGHT0);


	GLfloat fogColor[4] = {0.3, 0.7, 0.9, 1.0f}; // Fog Color


	glFogi(GL_FOG_MODE, GL_EXP); // Fog Mode
	glFogfv(GL_FOG_COLOR, fogColor); // Set Fog Color
	glFogf(GL_FOG_DENSITY, 0.0004f); // How Dense Will The Fog Be
	glHint(GL_FOG_HINT, GL_DONT_CARE); // Fog Hint Value
	glFogf(GL_FOG_START, 1.0f); // Fog Start Depth
	glFogf(GL_FOG_END, 4000); // Fog End Depth
	glEnable(GL_FOG); // Enables GL_FOG

	// enable color tracking
	glEnable(GL_COLOR_MATERIAL);
	// set material properties which will be assigned by glColor
	glColorMaterial(GL_FRONT, GL_AMBIENT_AND_DIFFUSE);

	float specReflection [] = {0.8f, 0.8f, 0.8f, 1.0f};
	glMaterialfv(GL_FRONT, GL_SPECULAR, specReflection);
	glMateriali(GL_FRONT, GL_SHININESS, 96);

	glDisable(GL_LIGHTING);

	glEnable(GL_BLEND);
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);
	glDepthFunc(GL_LESS);
	glMatrixMode(GL_MODELVIEW);
	glCullFace(GL_BACK);
	_GLStatesSetup = true;

}

void GraphicsManager::DrawTri(double * vertList, double* p1, double* p2, double* p3, int* arrayPosition)
{
	memcpy_s(vertList + (*arrayPosition), sizeof(double) * 3, p1, sizeof(double) * 3);	(*arrayPosition) += 3;
	memcpy_s(vertList + (*arrayPosition), sizeof(double) * 3, p2, sizeof(double) * 3);	(*arrayPosition) += 3;
	memcpy_s(vertList + (*arrayPosition), sizeof(double) * 3, p3, sizeof(double) * 3);	(*arrayPosition) += 3;
}

void GraphicsManager::DrawUIVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t colourA, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ)
{
	int vertexArrayStart = _UITriCount * 3 * 3;
	int colourArrayStart = _UITriCount * 3 * 4;
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

	for (int i = 0; i < FACESPERCUBE* VERTSPERFACE; i++)memcpy_s(_UIColourList + colourArrayStart + (i * 4), sizeof(uint8_t) * 4, colourArray, sizeof(uint8_t) * 4);

	//FRONT
	DrawTri(_UIVertexList, _tlf, _blf, _trf, &verpos);
	DrawTri(_UIVertexList, _blf, _brf, _trf, &verpos);

	//LEFT	
	DrawTri(_UIVertexList, _tlb, _blb, _tlf, &verpos);
	DrawTri(_UIVertexList, _blb, _blf, _tlf, &verpos);

	//RIGHT	
	DrawTri(_UIVertexList, _trb, _trf, _brb, &verpos);
	DrawTri(_UIVertexList, _brf, _brb, _trf, &verpos);

	//Back	
	DrawTri(_UIVertexList, _blb, _tlb, _trb, &verpos);
	DrawTri(_UIVertexList, _blb, _trb, _brb, &verpos);

	//Top	
	DrawTri(_UIVertexList, _tlb, _tlf, _trb, &verpos);
	DrawTri(_UIVertexList, _tlf, _trf, _trb, &verpos);

	//Bottom	
	DrawTri(_UIVertexList, _blb, _brb, _blf, &verpos);
	DrawTri(_UIVertexList, _blf, _brb, _brf, &verpos);

	_UITriCount += 12;
}

void GraphicsManager::DrawVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t colourA, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ)
{
	if (colourA == 0)return;
	double * vertArray = (colourA == 255) ? _VertexList : _TVertexList;
	uint8_t * colArray = (colourA == 255) ? _ColourList : _TColourList;
	double * normArray = (colourA == 255) ? _NormalList : _TNormalList;
	int * triCount = (colourA == 255) ? (&_TriCount) : (&_TTriCount);

	int vertexArrayStart = (*triCount) * 3 * 3;
	int colourArrayStart = (*triCount) * 3 * 4;
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

	for (int i = 0; i < FACESPERCUBE* VERTSPERFACE; i++)memcpy_s(colArray + colourArrayStart + (i * 4), sizeof(uint8_t) * 4, colourArray, sizeof(uint8_t) * 4);


	double _FrontNormals[9] = { 0, 0, -1, 0, 0, -1, 0, 0, -1 };
	double _LeftNormals[9] = { -1, 0, 0, -1, 0, 0, -1, 0, 0 };
	double _RightNormals[9] = { 1, 0, 0, 1, 0, 0, 1, 0, 0 };
	double _BackNormals[9] = { 0, 0, 1, 0, 0, 1, 0, 0, 1 };
	double _TopNormals[9] = { 0, -1, 0, 0, -1, 0, 0, -1, 0 };
	double _BottomNormals[9] = { 0, 1, 0, 0, 1, 0, 0, 1, 0 };

	//FRONT
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _FrontNormals, sizeof(double) * 9);
	DrawTri(vertArray, _tlf, _blf, _trf, &verpos);
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _FrontNormals, sizeof(double) * 9);
	DrawTri(vertArray, _blf, _brf, _trf, &verpos);

	//LEFT	
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _LeftNormals, sizeof(double) * 9);
	DrawTri(vertArray, _tlb, _blb, _tlf, &verpos);
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _LeftNormals, sizeof(double) * 9);
	DrawTri(vertArray, _blb, _blf, _tlf, &verpos);

	//RIGHT	
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _RightNormals, sizeof(double) * 9);
	DrawTri(vertArray, _trb, _trf, _brb, &verpos);
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _RightNormals, sizeof(double) * 9);
	DrawTri(vertArray, _brf, _brb, _trf, &verpos);

	//Back	
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _BackNormals, sizeof(double) * 9);
	DrawTri(vertArray, _blb, _tlb, _trb, &verpos);
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _BackNormals, sizeof(double) * 9);
	DrawTri(vertArray, _blb, _trb, _brb, &verpos);

	//Top	
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _TopNormals, sizeof(double) * 9);
	DrawTri(vertArray, _tlb, _tlf, _trb, &verpos);
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _TopNormals, sizeof(double) * 9);
	DrawTri(vertArray, _tlf, _trf, _trb, &verpos);

	//Bottom	
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _BottomNormals, sizeof(double) * 9);
	DrawTri(vertArray, _blb, _brb, _blf, &verpos);
	memcpy_s(normArray + (verpos), sizeof(double) * 9, _BottomNormals, sizeof(double) * 9);
	DrawTri(vertArray, _blf, _brb, _brf, &verpos);

	(*triCount) += 12;
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


void GraphicsManager::DrawTextToScreen(char* str, int strLength, int offsetX, int offsetY)
{
	//globally defined
	int font_list_base_2d = 2000; // set the start of the display lists for the 2d font

	wglUseFontBitmaps(_HDC, 0, 127, font_list_base_2d); // create a display list for each character (0-127)
	// start numbering the display lists at font_list_base_2d
	glColor3d(1.0, 1.0, 1.0);// white

	//glViewport(0, 0, _Width, _Height);
	glRasterPos3f(_CameraPosX + offsetX, _CameraPosY + offsetY, _CameraPosZ + 200); // set start position

	glListBase(font_list_base_2d); //start of our font display list numbers 

	glCallLists(strLength * 2, GL_UNSIGNED_BYTE, str);
}

int GraphicsManager::CreateShader(std::string vertexSource, std::string fragementSource)
{
	Shader * newShader = new Shader(vertexSource, fragementSource);
	_LoadedShaders->push_back(newShader);
	return newShader->GetID();
}

Shader * FindShader(int32_t id)
{
	for (Shader * s : *_LoadedShaders)
	{
		if (s->GetID() == id)
		{			
			return s;
		}
	}
	return nullptr;
}

bool GraphicsManager::CompileShader(int32_t id)
{
	Shader * s = FindShader(id);
	return (s != nullptr)? s->Compile():false;
}

void GraphicsManager::EnableShader(int32_t id)
{
	Shader * s = FindShader(id);
	if(s != nullptr)  s->Enable();
}

void GraphicsManager::DisableShader(int32_t id)
{
	Shader * s = FindShader(id);
	if (s != nullptr)  s->Disable();

}