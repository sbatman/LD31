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
	void DrawVoxel(int32_t x, int32_t y, int32_t z, uint8_t colourR, uint8_t colourG, uint8_t colourB, uint8_t alpha, uint8_t size);
private:
	const int VERTSPERFACE = 12;
	const int COLOURPERFACE = 24;
};