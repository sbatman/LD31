#pragma once
class Camera
{
public:
	Camera();
	~Camera();
private:
	double _CameraPosX = 0;
	double _CameraPosY = 0;
	double _CameraPosZ = 0;
	double _CameraRotZ = 0;
	double _CameraRotX = 0;
};

