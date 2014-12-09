#pragma once
class OpenGLContext
{
public:
	OpenGLContext(int32_t width, int32_t height);
	~OpenGLContext();

	void Init();
	void StartDraw();
	void EndDraw();
private:
	int32_t _Width = 0;
	int32_t _Height = 0;
	bool _HasInit = false;
};

