using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK_winforms_z02
{
    public class LightSource
    {
        public Vector3 Position { get; set; }
        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }

        public LightSource(Vector3 position, Vector3 ambient, Vector3 diffuse, Vector3 specular)
        {
            Position = position;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
        }
    }
}
