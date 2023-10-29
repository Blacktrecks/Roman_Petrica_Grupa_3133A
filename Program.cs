//ROMAN PETRICA
//GRUPA 3133A - CALCULATOARE
//TEMA LAB 3

//Import librarii specifice OpenTk
using System;
using System.Drawing; //import bibleoteca standard pentru lucrul cu imagini si culori
using System.IO;
using OpenTK; //import bibleoteca OpenTk pentru grafica 3D
using OpenTK.Graphics; //subcomponenta pentru grafica
using OpenTK.Graphics.OpenGL; //import functionalitati grafice OpenGL pentru OpenTk
using OpenTK.Input; //functionalitati de intrare pentru tastatura si mouse

namespace Roman_Petrica_Grupa_3133A
{
    //declararea clasei principale SimpleWindow3D care mosteneste din GameWindow
    class SimpleWindow3D : GameWindow  //Initiaalizare fereastra
    {
        float rotation_speed_horizontal = 0.0f;  //valoare pentru viteza de rotatie pe verticala
        float rotation_speed_vertical = 0.0f;   //valoare pentru voteza de rotatie pe orizontala
        float angle_horizontal; //declarare unghi pe orizontala
        float angle_vertical; //declarare unghi pe verticala
        bool showPyramid = true; //starea de vizibilitate a piramidei
        MouseState lastMouseState; //preia ultima stare a mouse-ului
        Color triangleColor = Color.White; // Adăugăm o variabilă pentru culoarea triunghiului

        private int ok = 1; //initializare variabila ok
        private int alpha = 0; //initializare variabila alfa
        //private int[] vec = new int[50];  //initializare tablou de vectori
        private float[] vec = new float[12]; // Schimbați dimensiunea tabloului la 12 pentru a stoca coordonatele a 4 vârfuri ale tetraedrului.

        public SimpleWindow3D() : base(800, 600){ //initializam fereastra cu dimensiuni        {
            VSync = VSyncMode.On;  //sincronizare ecran verticala
        }


        //Daca nu exista un fisier date.txt il vom crea si initializa cu coordonatele unui triunghi
        private void CreateAndSaveTriangleFile()
        {
            using (StreamWriter sw = new StreamWriter("date.txt"))
            {
                sw.WriteLine("0.0, 1.0, 0.0");
                sw.WriteLine("-1.0, -1.0, 0.0");
                sw.WriteLine("1.0, -1.0, 0.0");
            }
        }

        //1.Metoda Onload folosita pentru incarcarea initiala
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Seteaza culoarea de fundal a ferestrei
            GL.ClearColor(Color.Green);
            GL.Enable(EnableCap.DepthTest);

            //conditie care verifica data fisierul date.txt exista

            if (!File.Exists("date.txt"))
            {
                CreateAndSaveTriangleFile();
            }

            string linie;
            char[] sep = { ',' }; //definire separator pentru citirea din fisier
            int i = 0;

            //citeste datele din fisier "date.txt"
            using (System.IO.StreamReader f = new System.IO.StreamReader("date.txt"))
            {
                while ((linie = f.ReadLine()) != null)
                {
                    string[] numere = linie.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string x in numere)
                    {
                        if (i < vec.Length)
                        {
                            float val;
                            if (float.TryParse(x, out val))
                            {
                                vec[i++] = val;
                            }
                            else
                            {
                                Console.WriteLine("Eroare la parsarea valorii: " + x);
                            }
                        }
                        else
                        {
                            // cand depasim dimensiunea tabloului
                            Console.WriteLine("A fost depasita dimensiunea!!");
                        }
                    }
                }
            }

        }


        //2.Metoda OnResize pentru redimensionarea ferestrei
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //Setare viewpoint pentru potrivire cu fereastra
            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height; //calculeeaza raportul aspectului
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64); //construieste o matrice de perspectiva
            GL.MatrixMode(MatrixMode.Projection); // seteaza ca matricea de proiectie
            GL.LoadMatrix(ref perspective);// incarca matricea de perspectiva
        }

        //3.Metoda OnUpdateFrame pentru actualizarea logica
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var keyboard = OpenTK.Input.Keyboard.GetState();
            var mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit(); //inchide aplicatia la apasarea tastei ESC
                return;
            }
            //Controleaza rotatia obiectului in functie de apasarea tastelor
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
            //Controleaza rotatia obicetului cu Mouse-ul
            if (mouse.X != lastMouseState.X || mouse.Y != lastMouseState.Y)
            {
                rotation_speed_horizontal = (mouse.X - Width / 2) / 40.0f;
                rotation_speed_vertical = (mouse.Y - Height / 2) / 40.0f;
            }

            lastMouseState = mouse;

            // Adăugăm logica pentru modificarea culorii triunghiului la apăsarea tastelor
            if (keyboard[Key.R])
            {
                if (triangleColor.R < 255)
                {
                    triangleColor = Color.FromArgb(triangleColor.R + 1, triangleColor.G, triangleColor.B);
                }
            }
            if (keyboard[Key.G])
            {
                if (triangleColor.G < 255)
                {
                    triangleColor = Color.FromArgb(triangleColor.R, triangleColor.G + 1, triangleColor.B);
                }
            }
            if (keyboard[Key.B])
            {
                if (triangleColor.B < 255)
                {
                    triangleColor = Color.FromArgb(triangleColor.R, triangleColor.G, triangleColor.B + 1);
                }
            }
        }

        //4. Metoda pentru desenarea frame-ului
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //curata buffer-ul de adancime si culoare

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0); //seteazaa matricea de perspectiva pentru vizualizarea obictului
            GL.MatrixMode(MatrixMode.Modelview); //seteaza ca fiind matrice de modelare
            GL.LoadMatrix(ref lookat); //incarca matricea de modelare in OpenGl

            angle_horizontal += rotation_speed_horizontal * (float)e.Time;
            GL.Rotate(angle_horizontal, 0.0f, 1.0f, 0.0f); //roteste obiectul pe orizontala
            angle_vertical += rotation_speed_vertical * (float)e.Time;
            GL.Rotate(angle_vertical, 1.0f, 0.0f, 0.0f);//roteste obiectul pe verticala

            if (showPyramid)
            {
                DrawPyramid(); //desenare piramida
            }

            DrawObjects(); //desenare obiect creat

            // Actualizăm culoarea triunghiului în funcție de valorile RGB si il afisam
            GL.Color3(triangleColor.R / 255.0f, triangleColor.G / 255.0f, triangleColor.B / 255.0f);

            // Afișăm valorile RGB în consolă
            Console.WriteLine($"R: {triangleColor.R}, G: {triangleColor.G}, B: {triangleColor.B}");

            SwapBuffers(); //afisare frame nou pe ecran
        }

        //Metoda pentru desenare obiecte cu culori modificate
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

            //modifica culoare in functie de tastele introduse
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

            //Desenare linii intre punctele specificate
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(1.0, 0.0, 0.0); //culoare rosie
            GL.Vertex3(0.0, 10, 0.0f); //deseneaza punct
            GL.Color3(0.0, 0.0, 0.0);//culoare neagra
            GL.Vertex3(0.0f, 0.0f, 0.0f);//desenare punct nou
            GL.Color3(0.0, 1.0, 0.0);//culoare verde
            GL.Vertex3(10, 10, 0.0f);//desenare alt punct
            GL.Color3(0.0, 0.0, 1.0);//culoare albastra
            GL.Vertex3(15, 0.0f, 0.0f);//desenare punct
            GL.End();//termina desenarea liniilor
        }

        //Metoda pentru desenarea piramidei
        private void DrawPyramid()
        {
            GL.Begin(PrimitiveType.Polygon); //incepe desenarea poligoanelor

            GL.Color3(Color.DarkBlue); //culoare albastru inchis
            GL.Vertex3(10.0f, 10.0f, 10.0f);//deseneaza varf piramida
            GL.Vertex3(-10.0f, -10.0f, 10.0f);//desenare alt varf
            GL.Vertex3(10.0f, -10.0f, 10.0f);//deseneaza inca un varf

            GL.Color3(Color.Aquamarine);// culoare aquamarin
            GL.Vertex3(10.0f, 10.0f, 10.0f); //desenare varf
            GL.Vertex3(10.0f, -10.0f, 10.0f); //desenare alt varf
            GL.Vertex3(10.0f, -10.0f, -10.0f); //desenare inca un varf

            GL.Color3(Color.Yellow); //culoare galben
            GL.Vertex3(10.0f, 10.0f, 10.0f);//desenare varf
            GL.Vertex3(10.0f, -10.0f, -10.0f);//desenare alt varf
            GL.Vertex3(-10.0f, -10.0f, -10.0f);//desenare inca un varf

            GL.Color3(Color.DarkRed); //culoare rosu inchis
            GL.Vertex3(10.0f, 10.0f, 10.0f);//desenare varf
            GL.Vertex3(-10.0f, -10.0f, -10.0f);//desenare alt varf
            GL.Vertex3(-10.0f, -10.0f, 10.0f);//desenare inca un varf

            GL.Color3(Color.DarkRed);//culoare rosu inchis
            GL.Vertex3(10.0f, 10.0f, 10.0f);//desenare varf
            GL.Vertex3(-10.0f, -10.0f, -10.0f);//desenare alt varf
            GL.Vertex3(-10.0f, -10.0f, 10.0f);//desenare imca un varf

            GL.End(); //terminare desenare poligoane
        }

        //Metoda main care initializeaza si ruleaza fereastra OpenGl
        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D()) //ruleaza fereastra cu 30 de cadre pe secunda
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
