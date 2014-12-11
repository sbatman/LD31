#pragma once
class GraphicsManager
{
public:
	GraphicsManager();
	~GraphicsManager();
	void Init(int32_t width, int32_t height, int32_t handle);
	void BeginDraw();
	void EndDraw();
	void Update();
	void Destroy();
	void DrawVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t colourA, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ);
	void DrawUIVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t colourA, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ);

	void SerCameraPosition(double x, double y, double z);
	void SetCameraRotation(double z, double x);
	void SetMouseMoveCallback(void(_stdcall *callBack)(int32_t, int32_t));
	void SetMousePressCallback(void(_stdcall *callBack)(int32_t));
	void SetMouseReleaseCallback(void(_stdcall *callBack)(int32_t));
	void SetKeyDownCallback(void(_stdcall *callBack)(int32_t));
	void SetKeyUpCallback(void(_stdcall *callBack)(int32_t));

	void DrawTextToScreen(char* str, int strLength, int offsetX, int offsetY);

	int32_t CreateShader(std::string vertexSource, std::string fragementSource);
	bool CompileShader(int32_t id);
	void EnableShader(int32_t id);
	void DisableShader(int32_t id);
private:
	const int FACESPERCUBE = 6;
	const int TRISPERCUBE = FACESPERCUBE * 2;
	const int VERTSPERFACE = 6;

	bool _GLStatesSetup = false;
	double* _VertexList;
	uint8_t* _ColourList;
	double* _NormalList;

	double* _TVertexList;
	uint8_t* _TColourList;
	double* _TNormalList;

	double* _UIVertexList;
	uint8_t* _UIColourList;

	int _TriCount = 0;
	int _UITriCount = 0;
	int _TTriCount = 0;

	int _Width;
	int _Height;
	double _CameraPosX = 0, _CameraPosY = 0, _CameraPosZ = 0, _CameraRotZ = 0, _CameraRotX = 0;

	void SetupGLStates();
	void DrawTri(double* vertList, double* p1, double* p2, double* p3, int* arrayPosition);

};