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
private:
	const int FACESPERCUBE = 6;
	const int TRISPERCUBE = FACESPERCUBE * 2;
	const int VERTSPERFACE = 6;

	bool _GLStatesSetup = false;
	double* _VertexList;
	uint8_t* _ColourList;
	int _TriCount;

	int Width;
	int Height;
	

	void SetupGLStates();
	void DrawTri(double* p1, double* p2, double* p3, int* arrayPosition);
};