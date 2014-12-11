#include "stdafx.h"
#include "OpenGLContext.h"


OpenGLContext::OpenGLContext(int32_t width, int32_t height)
{
	_Width = width;
	_Height = height;
}


OpenGLContext::~OpenGLContext()
{
}

void OpenGLContext::Init()
{
	glShadeModel(GL_SMOOTH);
	glViewport(0, 0, _Width, _Height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(70, _Width / (float) _Height, 2, 2000);
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
	glFogf(GL_FOG_DENSITY, 0.0004f);              // How Dense Will The Fog Be
	glHint(GL_FOG_HINT, GL_DONT_CARE);          // Fog Hint Value
	glFogf(GL_FOG_START, 1.0f);             // Fog Start Depth
	glFogf(GL_FOG_END, 4000);               // Fog End Depth
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
	glDepthFunc(GL_LESS);
	glMatrixMode(GL_MODELVIEW);
	glCullFace(GL_BACK);
}

void OpenGLContext::StartDraw()
{
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

void OpenGLContext::EndDraw()
{

}