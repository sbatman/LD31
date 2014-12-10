#pragma once
class Shader
{
public:
	Shader(std::string vertexShader, std::string fragmentShader);
	~Shader();

	bool virtual Compile();
	bool virtual IsCompiled();
	int virtual GetID();
	void virtual Enable();
	void virtual Disable();

private:
	char * _FragementSource;
	char * _VertexSource;
	int _FragementShaderLength;
	int _VertexShaderLength;
	GLenum _Program;
	GLenum _VertexShader;
	GLenum _FragementShader;
	bool _Compiled = false;
	int _ID;
	bool _Enabled;
};

