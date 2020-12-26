using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SharpDX;

using SharpDX.Direct3D11;
using SharpDX.DXGI;

using System.Windows;
using System.Threading.Tasks;

using SharpDX.Direct3D;



namespace SC_Mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        SharpDX.Direct3D11.Device device;

        public static Stopwatch timeWatch = new Stopwatch();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Game1 currentWindow;
        SharpDX.Direct3D11.DeviceContext context;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.Reach;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            currentWindow = this;
            backgroundWorker0 = new BackgroundWorker();
            backgroundWorker0.WorkerSupportsCancellation = true;
            backgroundWorker0.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                doStuff();

            };
            backgroundWorker0.RunWorkerAsync();

            backgroundWorker0.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            {

            }




            /*int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
        new ModeDescription(Width, Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Used for debugging dispose object references
            // Configuration.EnableObjectTracking = true;

            // Disable throws on shader compilation errors
            //Configuration.ThrowOnShaderCompileError = false;

            // Create Device and SwapChain
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
            context = device.ImmediateContext;


            SC_SharpDX_ScreenCapture screenCapture = new SC_SharpDX_ScreenCapture();
            /*var backgroundWorker0 = new BackgroundWorker();
            backgroundWorker0.DoWork += (object sender, DoWorkEventArgs args) =>
            {
            _threadLoop:

                Thread.Sleep(1);
                goto _threadLoop;
            };
            backgroundWorker0.RunWorkerAsync();*/

            base.Initialize();
        }
        public void doStuff()
        {
            for (int i = 0; i < 1; i++)
            {
                int stopper = 0;
            _threadLoop:
                if (currentWindow.IsActive)
                {
                    Console.WriteLine("found window");
                    //backgroundWorker0.CancelAsync();
                    //backgroundWorker0.Dispose();
                    KillMe();
                    stopper = 1;
                }
                /*var refreshDXEngineAction = new Action(delegate
                {
                    if (currentWindow.IsActive)
                    {
                        Console.WriteLine("found window");
                        //backgroundWorker0.CancelAsync();
                        //backgroundWorker0.Dispose();
                        KillMe();
                        stopper = 1;
                    }
                });
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                */
                if (stopper == 1)
                {
                    //KillMe();
                    break;
                }

                if (backgroundWorker0.CancellationPending)
                {
                    Console.WriteLine("cancellation pending");
                    //backgroundWorker0.CancelAsync();
                    //backgroundWorker0.Dispose();
                    //KillMe();
                    KillMe();
                    break;
                }

                Thread.Sleep(1);
                goto _threadLoop;
            }
        }
        public static BackgroundWorker backgroundWorker0;

        public static void KillMe()
        {
            if (backgroundWorker0 != null)
            {
                backgroundWorker0.CancelAsync();
            }
            if (backgroundWorker0 != null)
            {
                backgroundWorker0.Dispose();
            }
            if (backgroundWorker0 != null)
            {
                backgroundWorker0 = null;
            }
            GC.Collect();
        }
        int Width;
        int Height;

        public void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;

            //Console.WriteLine(Handle);

            Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                new ModeDescription(Width, Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Used for debugging dispose object references
            // Configuration.EnableObjectTracking = true;

            // Disable throws on shader compilation errors
            //Configuration.ThrowOnShaderCompileError = false;

            // Create Device and SwapChain
            SwapChain swapChain;
            SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
            context = device.ImmediateContext;
            screenCapture = new SC_SharpDX_ScreenCapture(0,0, device);
        }













        public static SC_SharpDX_ScreenCapture screenCapture;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        int startOncer = 1;



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            //screenCapture.ScreenCapture(1, 1);
            timeWatch.Stop();
            timeWatch.Reset();
            timeWatch.Start();

            if (screenCapture != null)
            {
                if (startOncer == 1)
                {
                    backgroundWorker0 = new BackgroundWorker();
                    backgroundWorker0.WorkerSupportsCancellation = true;
                    backgroundWorker0.DoWork += (object sender, DoWorkEventArgs args) =>
                    {
                    _threadLoop:
                        screenCapture.ScreenCapture(1, 1);
                        Thread.Sleep(1);
                        goto _threadLoop;

                    };
                    backgroundWorker0.RunWorkerAsync();

                    backgroundWorker0.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
                    {

                    }

                    startOncer = 0;
                }
               
            }

            timeWatch.Stop();
            Console.WriteLine(timeWatch.Elapsed.Milliseconds);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here 
            base.Draw(gameTime);
        }
    }
}
