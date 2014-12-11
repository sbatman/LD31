#include "stdafx.h"
#include "Shader.h"

static int _LastAllocatedID = 0;

bool ShaderCompileSuccessful(int obj)
{
	int status;
	glGetShaderiv(obj, GL_COMPILE_STATUS, &status);
	return status == GL_TRUE;
}

bool ShaderLinkSuccessful(int obj)
{
	int status;
	glGetProgramiv(obj, GL_LINK_STATUS, &status);
	return status == GL_TRUE;
}

Shader::Shader(std::string vertexShader, std::string fragmentShader)
{
	_ID = _LastAllocatedID++;
	_FragementShaderLength = fragmentShader.length() + 1;
	_VertexShaderLength = vertexShader.length() + 1;

	_VertexSource = new char[_VertexShaderLength];
	_FragementSource = new char[_FragementShaderLength];

	strcpy_s(_VertexSource, vertexShader.length() + 1, vertexShader.c_str());
	strcpy_s(_FragementSource, fragmentShader.length() + 1, fragmentShader.c_str());
}

Shader::~Shader()
{
}

bool Shader::IsCompiled()
{
	return _Compiled;
}
int Shader::GetID()
{
	return _ID;
}

void Shader::Enable()
{
	if (_Enabled)return;

	_Enabled = true;
	glLinkProgram(_Program);
	glUseProgram(_Program);
}

void Shader::Disable()
{
	if (!_Enabled)return;
	_Enabled = false;
	glUseProgram(NULL);
}

bool Shader::Compile()
{
	_Program = glCreateProgram();
	_VertexShader = glCreateShader(GL_VERTEX_SHADER);
	_FragementShader = glCreateShader(GL_FRAGMENT_SHADER);

	glShaderSource(_VertexShader, 1, &_VertexSource, NULL);
	glShaderSource(_FragementShader, 1, &_FragementSource, NULL);
	glCompileShader(_VertexShader);
	glCompileShader(_FragementShader);
	glAttachShader(_Program, _VertexShader);
	glAttachShader(_Program, _FragementShader);
	
	bool vCompileOK = ShaderCompileSuccessful(_VertexShader);
	if (!vCompileOK)
	{
		GLsizei logLength = 0;
		glGetShaderiv(_VertexShader, GL_INFO_LOG_LENGTH, &logLength);
		GLchar* log_string = new GLchar[logLength + 1];
		glGetShaderInfoLog(_Program, logLength, 0, log_string);
		printf("Vertex shader failure :%s", log_string);
	}
	bool fCompileOK = ShaderCompileSuccessful(_FragementShader);
	if (!fCompileOK)
	{
		GLsizei logLength = 0;
		glGetShaderiv(_FragementShader, GL_INFO_LOG_LENGTH, &logLength);
		GLchar* log_string = new char[logLength + 1];
		glGetShaderInfoLog(_Program, logLength, 0, log_string);
		printf("Fragment shader failure :%s", log_string);
	}
	_Compiled = vCompileOK && fCompileOK;
	return _Compiled;
}
