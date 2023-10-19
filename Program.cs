//LABORATOR 2 - ROMAN PETRICA - GRUPA 3133A
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Roman_Petrica_Grupa_3133A
{
    class SimpleWindow3D : GameWindow
    {
        //constante intializate
        //viteza de rotatie pe orizontala
        float rotation_speed_horizontal = 0.0f;
        //viteza de rotatie pe verticala
        float rotation_speed_vertical = 0.0f;
        //unghi de rotatie
        float angle_horizontal;
        float angle_vertical;
        bool showPyramid = true;

        MouseState lastMouseState;
        

        // Constructor.
        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        //Initializare fereastra si culoare
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Green);
            GL.Enable(EnableCap.DepthTest);
        }

        //Redimensionarea ferestrei
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //initializare orientare de vizualizare
            GL.Viewport(0, 0, Width, Height);
            //rata de aspect
            double aspect_ratio = Width / (double)Height;
            //creare perspectiva de vedere si rata de aspect
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }
        //reimprospatare frame
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            base.OnUpdateFrame(e);
            //initializare status tastatura
            var keyboard = OpenTK.Input.Keyboard.GetState();
            //status mouse
            var mouse = OpenTK.Input.Mouse.GetState();
            //daca se va apasa esc se va termina programul
            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            // Control obiect pe orizontala stanga si dreapta
            if (keyboard[Key.D])
            {
                rotation_speed_horizontal += 10.0f;
            }
            if (keyboard[Key.A])
            {
                rotation_speed_horizontal -= 10.0f;
            }
            //control obiect pe verticala sus si jos
            if (keyboard[Key.W])
            {
                rotation_speed_vertical += 10.0f;
            }
            if (keyboard[Key.S])
            {
                rotation_speed_vertical -= 10.0f;
            }


            // Control obiect prin mișcarea mouse-ului.
            //viteza de rotatie
            if (mouse.X != lastMouseState.X || mouse.Y != lastMouseState.Y)
            {
                rotation_speed_horizontal = (mouse.X - Width / 2) / 40.0f;
                rotation_speed_vertical = (mouse.Y - Height / 2) / 40.0f;
            }

            //status mouse
            lastMouseState = mouse;



        }

        //randare frame-uri
        //rotatia unghiului pe coordonate
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            angle_horizontal += rotation_speed_horizontal*(float)e.Time;

            GL.Rotate(angle_horizontal, 0.0f, 1.0f, 0.0f);
            angle_vertical += rotation_speed_vertical*(float)e.Time;

            GL.Rotate(angle_vertical, 1.0f, 0.0f, 0.0f);

            //afisare piramida
            if (showPyramid)
            {

                DrawPyramid();
            }

            SwapBuffers();
        }


        //desenare piramida
        private void DrawPyramid()
        {
            GL.Begin(PrimitiveType.Polygon);

            //culori si coordonate laturi piramida
            GL.Color3(Color.DarkBlue);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(-10.0f, -10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, 10.0f);

            GL.Color3(Color.Aquamarine);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, -10.0f);

            GL.Color3(Color.Yellow);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, -10.0f);
            GL.Vertex3(-10.0f, -10.0f, -10.0f);

            GL.Color3(Color.DarkRed);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(-10.0f, -10.0f, -10.0f);
            GL.Vertex3(-10.0f, -10.0f, 10.0f);


            GL.Color3(Color.DarkRed);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(-10.0f, -10.0f, -10.0f);
            GL.Vertex3(-10.0f, -10.0f, 10.0f);
            GL.End();
        }


        [STAThread]
        static void Main(string[] args)
        {

            //deschidere fereastra si nr de fps
            using (SimpleWindow3D example = new SimpleWindow3D())
            {


                example.Run(30.0, 0.0);
            }
        }
    }
}