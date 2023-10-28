//LABORATOR 3
//ROMAN PETRICA GRUPA 3133A

using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Roman_Petrica_3133A
{
    class SimpleWindow3D : GameWindow
    {
        float rotation_speed_horizontal = 0.0f;
        float rotation_speed_vertical = 0.0f;
        float angle_horizontal;
        float angle_vertical;
        bool showPyramid = true;
        MouseState lastMouseState;
        private int ok = 1;
        private int alpha = 0;
        private int[] vec = new int[10];

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Green);
            GL.Enable(EnableCap.DepthTest);

            // Citeste datele din "date.txt"
            string linie;
            char[] sep = { ',' };
            int i = 0;

            using (System.IO.StreamReader f = new System.IO.StreamReader("date.txt"))
            {
                while ((linie = f.ReadLine()) != null)
                {
                    string[] numere = linie.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string x in numere)
                        vec[i++] = int.Parse(x);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        /*Sectiunea pentru "game logic" ce va fi randat automat pe ecran in pasul urmator - control utilizator
         pentru actualizarea pozitiei obiectelor
         */

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var keyboard = OpenTK.Input.Keyboard.GetState();
            var mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            if (keyboard[Key.D])
            {
                rotation_speed_horizontal += 10.0f;
            }
            if (keyboard[Key.A])
            {
                rotation_speed_horizontal -= 10.0f;
            }

            if (keyboard[Key.W])
            {
                rotation_speed_vertical += 10.0f;
            }
            if (keyboard[Key.S])
            {
                rotation_speed_vertical -= 10.0f;
            }

            if (mouse.X != lastMouseState.X || mouse.Y != lastMouseState.Y)
            {
                rotation_speed_horizontal = (mouse.X - Width / 2) / 40.0f;
                rotation_speed_vertical = (mouse.Y - Height / 2) / 40.0f;
            }

            lastMouseState = mouse;
        }

        /** Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME().
        Parametrul de intrare "e" conține informatii de timing pentru randare. */
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //se pun lucrurile pe care dorim sa le desenam in acest camp
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            angle_horizontal += rotation_speed_horizontal * (float)e.Time;
            GL.Rotate(angle_horizontal, 0.0f, 1.0f, 0.0f);
            angle_vertical += rotation_speed_vertical * (float)e.Time;
            GL.Rotate(angle_vertical, 1.0f, 0.0f, 0.0f);

            if (showPyramid)
            {
                DrawPyramid();
            }

            DrawObjects();

            SwapBuffers();
        }


        // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
        private void DrawObjects()
        {
            KeyboardState keyboard = Keyboard.GetState();
            GL.Begin(PrimitiveType.Triangles);

            if (keyboard[Key.Up])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(ok, 0, 0));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[0], vec[1], vec[2]);

            if (keyboard[Key.Down])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(0, ok, 0));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[3], vec[4], vec[5]);

            if (keyboard[Key.Left])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(0, 0, ok));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[6], vec[7], vec[8]);

            if (keyboard[Key.Right])
            {
                if (alpha == 255 || ok == 255)
                    alpha = ok = 0;
                alpha++;
                ok++;
                GL.Color3(Color.FromArgb(alpha, 0, 255, 0));
                Console.WriteLine(alpha);
            }

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(0.0, 10, 0.0f); //vertex 1
            GL.Color3(0.0, 0.0, 0.0);
            GL.Vertex3(0.0f, 0.0f, 0.0f); //vertex 2
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10, 10, 0.0f); //vertex 3
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(15, 0.0f, 0.0f); //vertex 4
            GL.End();
        }

        private void DrawPyramid()
        {
            GL.Begin(PrimitiveType.Polygon);

            //deseneaza  cu albastru inchis
            GL.Color3(Color.DarkBlue);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(-10.0f, -10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, 10.0f);
            //deseneaza cu aquamarin
            GL.Color3(Color.Aquamarine);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, -10.0f);
            //deseneaza Oz cu galbem
            GL.Color3(Color.Yellow);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, -10.0f);
            GL.Vertex3(-10.0f, -10.0f, -10.0f);
            //deseneaza cu rosu inchis
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
        /**Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
          în uz (vezi metoda "Dispose()").
          Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
          și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
          grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
          Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
          în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
          în final pe ecran). */
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
