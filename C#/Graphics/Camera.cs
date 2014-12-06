using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Graphics
{
    class Camera
    {
        private Double _PositionX;
        private Double _PositionY;
        private Double _PositionZ;

        private Double _RotationZ;
        private Double _RotationX;

        private bool _Dirty = false;

        public void Update()
        {
            if (_Dirty)
            {
                GraphicsManager.NativeMethods.GraphicsManagerSerCameraPosition(_PositionX, _PositionY, _PositionZ);
                GraphicsManager.NativeMethods.GraphicsManagerSetCameraRotation(_RotationZ, _RotationX);
            }
        }

        public void SetPosition(Double x, Double y, Double z)
        {
            _PositionX = x;
            _PositionY = y;
            _PositionZ = z;
            _Dirty = true;
        }

        public void Move(Double x, Double y, Double z)
        {
            _PositionX += x;
            _PositionY += y;
            _PositionZ += z;
            _Dirty = true;
        }

        public void SetRotation(Double z, Double x)
        {
            _RotationX = x;
            _RotationZ = z;
            _Dirty = true;
        }

        public void Rotatate(Double z, Double x)
        {
            _RotationX += x;
            if (_RotationX < -80) _RotationX = -80;
            if (_RotationX > 80) _RotationX = 80;
            _RotationZ += z;
            _Dirty = true;
        }
    }
}
