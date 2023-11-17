using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace ConsoleApp3
{
    class MovingObject
    {
        private float _x;
        private float _y;
        private float _speed;

        public MovingObject(float x, float y, float speed)
        {
            _x = x;
            _y = y;
            _speed = speed;
        }

        public void Update()
        {
            _y += _speed;

            // Poți adăuga aici și logica pentru oprirea la marginea de jos a ferestrei, dacă este necesar
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f, 1.0f, 1.0f); // Culoarea obiectului (alb)
            GL.Vertex2(_x, _y);
            GL.Vertex2(_x + 50, _y);
            GL.Vertex2(_x + 50, _y + 50);
            GL.Vertex2(_x, _y + 50);
            GL.End();
        }
    }

    class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private readonly Randomizer rando;
        private readonly Axes ax;
        private readonly Grid grid;
        private readonly Camera3DIsometric cam;
        private bool displayMarker;
        private ulong updatesCounter;
        private ulong framesCounter;
        private MovingObject movingObject;

        //1
        private FallingObject fallingObject;
        private MassiveObject objFromFile;

        private float _speed = 5.0f;

        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);

        //1


        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            rando = new Randomizer();
            ax = new Axes();
            grid = new Grid();
            cam = new Camera3DIsometric();
            movingObject = null;

            DisplayHelp();
            displayMarker = false;
            updatesCounter = 0;
            framesCounter = 0;

            //3
            objFromFile = new MassiveObject(Color.White, "D:\\OpenTK - Lab 05\\cube.obj", 10.0f);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.ClearColor(DEFAULT_BKG_COLOR);

            GL.Viewport(0, 0, this.Width, this.Height);

            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            cam.SetCamera();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            updatesCounter++;

            if (displayMarker)
            {
                TimeStampIt("update", updatesCounter.ToString());
            }

            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
                ax.Show();
                grid.Show();
            }

            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                ax.ToggleVisibility();
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                grid.ToggleVisibility();
            }

            if (fallingObject != null)
            {
                fallingObject.Update();
            }

            if (currentKeyboard[Key.O] && !previousKeyboard[Key.O])
            {
                // Toggle visibility of the existing object
                if (movingObject != null)
                {
                    movingObject = null;
                }
                else
                {
                    // Create a new object at a random position
                    float randomX = (float)rando.RandomDouble() * this.Width;
                    float randomY = (float)rando.RandomDouble() * this.Height;
                    movingObject = new MovingObject(randomX, randomY, _speed);
                }
            }

            // camera control (isometric mode)
            if (currentKeyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (currentKeyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (currentKeyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (currentKeyboard[Key.D])
            {
                cam.MoveRight();
            }
            if (currentKeyboard[Key.Q])
            {
                cam.MoveUp();
            }
            if (currentKeyboard[Key.E])
            {
                cam.MoveDown();
            }

            // helper functions
            if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
            {
                displayMarker = !displayMarker;
            }

            // Update the moving object if it exists
            if (movingObject != null)
            {
                movingObject.Update();
            }

            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;
        }

        //1
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                fallingObject = new FallingObject(100, 500, 5.0f);
            }
            else if (e.Button == MouseButton.Right)
            {
                cam.MoveToNear(); // Click dreapta pentru a repoziționa la locația "aproape"
            }
            else if (e.Button == MouseButton.Middle)
            {
                cam.MoveToFar(); // Click mijloc pentru a repoziționa la locația "departe"
            }
        }



        // Existing code...

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            framesCounter++;

            if (displayMarker)
            {
                TimeStampIt("render", framesCounter.ToString());
            }

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            grid.Draw();
            ax.Draw();

            // Desenare pentru obiectul care cade
            if (fallingObject != null)
            {
                fallingObject.Draw();
            }

            // Desenare pentru obiectul masiv
            if (objFromFile != null)
            {
                objFromFile.Draw();
            }

            SwapBuffers();
        }

        // Existing code...


        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" (H) - meniul");
            Console.WriteLine(" (ESC) - parasire aplicatie");
            Console.WriteLine(" (K) - schimbare vizibilitate sistem de axe");
            Console.WriteLine(" (R) - resteaza scena la valori implicite");
            Console.WriteLine(" (B) - schimbare culoare de fundal");
            Console.WriteLine(" (V) - schimbare vizibilitate linii");
            Console.WriteLine(" (O) - toggle vizibilitate obiect");
            Console.WriteLine(" (W,A,S,D) - deplasare camera (izometric)");
        }

        private void TimeStampIt(String source, String counter)
        {
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }
    }
}
