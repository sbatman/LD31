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
	void DrawVoxel(double x, double y, double z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t alpha, uint16_t sizeX, uint16_t sizeY, uint16_t sizeZ);
private:
	const int VERTSPERFACE = 12;
	const int COLOURPERFACE = 24;
};