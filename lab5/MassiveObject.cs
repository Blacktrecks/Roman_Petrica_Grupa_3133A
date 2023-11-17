using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ConsoleApp3
{
    public class MassiveObject
    {
        private List<Vector3> coordsList;
        private bool visibility;
        private Color meshColor;
        private bool hasError;

        public MassiveObject(Color col, string fileName, float scale)
        {
            try
            {
                coordsList = LoadFromObjFile(fileName, scale);

                if (coordsList.Count == 0)
                {
                    Console.WriteLine($"Crearea obiectului a eșuat: obiect negăsit/coordonate lipsă pentru fișierul {fileName}!");
                    return;
                }

                visibility = true;
                meshColor = col;
                hasError = false;
                Console.WriteLine($"Obiect 3D încărcat - {coordsList.Count} vertexuri disponibile!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Fișierul {fileName} lipsește sau are un format greșit!!!");
                hasError = true;
            }
        }

        private List<Vector3> LoadFromObjFile(string fileName, float scale)
        {
            List<Vector3> vertices = new List<Vector3>();

            try
            {
                var lines = File.ReadLines(fileName);
                foreach (var line in lines)
                {
                    if (line.Trim().Length > 2 && line.Trim().StartsWith("v "))
                    {
                        string[] block = line.Trim().Split(' ');
                        if (block.Length == 4)
                        {
                            float x = float.Parse(block[1].Trim()) * scale;
                            float y = float.Parse(block[2].Trim()) * scale;
                            float z = float.Parse(block[3].Trim()) * scale;

                            vertices.Add(new Vector3(x, y, z));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Fișierul {fileName} nu a putut fi citit!");
            }

            return vertices;
        }


        public void Draw()
        {
            if (visibility)
            {
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Triangles);

                foreach (var vertex in coordsList)
                {
                    GL.Vertex3(vertex);
                }

                GL.End();
            }
        }
    }
}
