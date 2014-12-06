#include "stdafx.h"
#include <gl/gl.h>
#include <gl/glu.h>
#include "GraphicsManager.h"

HDC			_HDC = nullptr;		// Private GDI Device Context
HGLRC		_HDR = nullptr;		// Permanent Rendering Context
HWND		_HWnd = nullptr;		// Holds Our Window Handle
HINSTANCE	hInstance;		// Holds The Instance Of The Application

float testRotate = 0;

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

void GraphicsManager::Init(int32_t width, int32_t height, int32_t handle)
{
	Width = width;
	Height = height;
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
	_HWnd = CreateWindowEx(0, L"0", L"LD31", WS_OVERLAPPEDWINDOW, 0, 0, Width, Height, 0, 0, hInstance, 0);
	ShowWindow(_HWnd, SW_SHOW);
}

void GraphicsManager::BeginDraw()
{
	if (!_GLStatesSetup)SetupGLStates();

	testRotate += 1.0f;
	glLoadIdentity();
	glTranslatef(0, 0, -1500);
	glRotatef(testRotate, 0, 0, 1);
	glRotatef(testRotate*0.3f, 0, 1, 0);
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
			pfd = { sizeof(PIXELFORMATDESCRIPTOR), 1, PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, PFD_MAIN_PLANE, 0, 0, 0, 0 };
			nPixelFormat = ChoosePixelFormat(_HDC, &pfd);
			SetPixelFormat(_HDC, nPixelFormat, &pfd);
			_HDR = wglCreateContext(_HDC);
			wglMakeCurrent(_HDC, _HDR);
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

void GraphicsManager::Destroy()
{

}

void GraphicsManager::SetupGLStates()
{
	glViewport(0, 0, Width, Height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(70, Width / (float) Height, 0.1, 5000);
	glClearColor(255, 0, 0, 255);
	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_COLOR_ARRAY);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_LIGHTING);
	glEnable(GL_BLEND);
	glEnable(GL_CULL_FACE);
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
