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
};

