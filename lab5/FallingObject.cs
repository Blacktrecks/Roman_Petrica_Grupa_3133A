using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class FallingObject
    {
        private float _x;
        private float _y;
        private float _speed;

        public FallingObject(float x, float y, float speed)
        {
            _x = x;
            _y = y;
            _speed = speed;
        }

        public void Update()
        {
            _y += _speed;

            // Oprire la contactul cu planul Oxz
            if (_y < 0)
            {
                _y = 0;
                _speed = 0;
            }
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f, 0.0f, 1.0f); // Culoarea obiectului (alb)
            GL.Vertex2(_x, _y);
            GL.Vertex2(_x + 0, _y);
            GL.Vertex2(_x + 0, _y + 0);
            GL.Vertex2(_x, _y + 0);
            GL.End();
        }
    }

}
