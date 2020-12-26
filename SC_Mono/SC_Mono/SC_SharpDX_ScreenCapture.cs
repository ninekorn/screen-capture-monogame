// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Drawing.Imaging;
using System.IO;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;


using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

using System.Drawing;
using System.Runtime.InteropServices;

using System.Windows.Input;
//using System.Windows.Forms;


using SharpDX.Mathematics.Interop;
//using Device = SharpDX.Direct3D11.Device;
//using MapFlags = SharpDX.Direct3D11.MapFlags;
using Resource = SharpDX.DXGI.Resource;
using ResultCode = SharpDX.DXGI.ResultCode;

using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

using System.Linq;
using System.Collections.Generic;


namespace SC_Mono
{
    /// <summary>
    ///   Screen capture of the desktop using DXGI OutputDuplication.
    /// </summary>
    public class SC_SharpDX_ScreenCapture
    {
        // # of graphics card adapter
        static int _numAdapter = 0;
        // # of output device (i.e. monitor)
        static int _numOutput = 0;

        readonly Adapter1 _adapter;
        //static Factory1 _factory;

        SharpDX.Direct3D11.Device _device;
        //static Output _output;
     
        readonly Output1 _output1;
        static Texture2D _texture2D;
        readonly OutputDuplication _outputDuplication;
        readonly Texture2DDescription _textureDescription;
        Bitmap _bitmap;

        static int _width = 0;
        static int _height = 0;
        int _bytesTotal;

        SharpDX.DXGI.Resource _screenResource;
        OutputDuplicateFrameInformation _duplicateFrameInformation;

        SC_SharpDX_ScreenFrame _desktopFrame;

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        byte[] _textureByteArray;


        public SC_SharpDX_ScreenCapture(int adapter, int numOutput,SharpDX.Direct3D11.Device _dx11Device)
        {
            _desktopFrame = new SC_SharpDX_ScreenFrame();

            _numAdapter = adapter;
            _numOutput = numOutput;
     
            try
            {
                using (var _factory = new Factory1())
                {
                    this._adapter = _factory.GetAdapter1(_numAdapter);
                    /*if (this._adapter != null)
                    {
                        Console.WriteLine(this._adapter);
                    }*/
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                //return;
            }
            this._device = _dx11Device;
            /*try
            {
                //initializeDevice();
                this._device = new Device(_adapter);
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                //return null;
            }
            _dx11Device = this._device;*/

            try
            {
                using (var _output = _adapter.GetOutput(_numOutput))
                {
                    _width = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                    _height = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;
                    _desktopFrame.width = _width;
                    _desktopFrame.height = _height;
                    this._output1 = _output.QueryInterface<Output1>();

                   
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                //return;
            }

            /*_currentByteArray = new byte[_width * _height * 4];

            for (int i = 0; i < _currentByteArray.Length; i++)
            {
                _currentByteArray[i] = 0;
            }

            _previousTextureByteArray = new byte[_width * _height * 4];

            for (int i = 0; i < _previousTextureByteArray.Length; i++)
            {
                _previousTextureByteArray[i] = 0;
            }*/


            try
            {
                //duplicateOutput();
                this._outputDuplication = _output1.DuplicateOutput(_device);

                /*if (this._outputDuplication != null)
                {
                    Console.WriteLine(this._outputDuplication);
                }*/
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                //return;
            }

            try
            {
                /*this._textureDescription = new Texture2DDescription
                {
                    Format = Format.D32_Float_S8X24_UInt,
                    ArraySize = 1,
                    MipLevels = 1,
                    Width = _width,
                    Height = _height,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.DepthStencil,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None
                };*/

                /*this._textureDescription = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = _width,
                    Height = _height,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };*/

                /*this._textureDescription = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.RenderTarget,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = _width,
                    Height = _height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };*/
                this._textureDescription = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = _width,
                    Height = _height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
               // return;
            }

            _texture2D = new Texture2D(_device, _textureDescription);


            if (_texture2D != null)
            {
                Console.WriteLine(_texture2D);
            }
            try
            {
                _bitmap = new System.Drawing.Bitmap(_width, _height, PixelFormat.Format32bppArgb);
                var boundsRect = new System.Drawing.Rectangle(0, 0, _width, _height);
                var bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                _bitmap.UnlockBits(bmpData);
                _textureByteArray = new byte[_bytesTotal];
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
            try
            {
                /*_bitmap = new System.Drawing.Bitmap(_width, _height, PixelFormat.Format32bppArgb);
                var boundsRect = new System.Drawing.Rectangle(0, 0, _width, _height);
                var bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                _bitmap.UnlockBits(bmpData);
                _textureByteArray = new byte[_bytesTotal];*/
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                //return;
            }

            //Console.WriteLine("test");
        }


        void writeTo(string test)
        {
            Console.WriteLine(test);
        }



        //IntPtr memIntPtr;
        // Get a byte pointer from the IntPtr object.
        //byte* memBytePtr;
        bool _hasAcquiredFrame = false;

        [STAThread]
        public SC_SharpDX_ScreenFrame ScreenCapture(int timeOut,int milliSeconds)
        {
            _hasAcquiredFrame = false;
            try
            {
                //acquireFrame(timeOut);
                if (!acquireFrame(timeOut, milliSeconds))
                {
                    _hasAcquiredFrame = false;
                    ///return _desktopFrame;
                    return _desktopFrame;
                }
                else
                {
                    //releaseFrame();
                    _hasAcquiredFrame = true;
                }
                copyResource();

                /*if (_desktopFrame._texture2d!= null)
                {
                    Console.WriteLine("!null");
                }*/
                //mapSubResource();
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (_hasAcquiredFrame)
            {
                releaseFrame();
            }
            return _desktopFrame;      
        }

        int _testCounter = 0;

        static Stopwatch _testWatch = new Stopwatch();

        int _totalFrames = 0;
        static bool _startStopWatch = true;

        bool acquireFrame(int timeOut, int millisecondTest)
        {
            _screenResource = null;
            try
            {
                if (_startStopWatch)
                {
                    _testWatch.Stop();
                    _testWatch.Reset();
                    _testWatch.Start();
                    _startStopWatch = false;
                }

                if (_testWatch.Elapsed.Milliseconds >= millisecondTest)
                {
                    if (_totalFrames > 1)
                    {
                        _testWatch.Stop();
                        //int _milli = _testWatch.Elapsed.Milliseconds;
                        //Console.WriteLine("_milli" + "***" + millisecondTest);

                        Result _result = _outputDuplication.TryAcquireNextFrame(timeOut, out _duplicateFrameInformation, out _screenResource);


                        _totalFrames = 0;
                        _startStopWatch = true;
                    }               
                }
            }
            catch //(SharpDXException ex)
            {
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
            _totalFrames++;

            if (_screenResource != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        Stopwatch _testingWatch = new Stopwatch();
        Bitmap im ;
        bool copyResource()
        {
            try
            {
                //_texture2D = new Texture2D(_device, _textureDescription);

                using (var screenTexture2D = _screenResource.QueryInterface<Texture2D>())
                {
                    _device.ImmediateContext.CopyResource(screenTexture2D, _texture2D);
                }         
            }
            catch
            {
                return false;
            }

            var dataBox = _device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, MapFlags.None);


            var boundsRect = new System.Drawing.Rectangle(0, 0, _width, _height);
            var sourcePtr = dataBox.DataPointer;

            Marshal.Copy(sourcePtr, _textureByteArray, 0, _bytesTotal);
            _device.ImmediateContext.UnmapSubresource(_texture2D, 0);
            DeleteObject(sourcePtr);

            //var array = Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0);

            int memoryBitmapStride = _width * 4;
            someBitmap = new Bitmap(_width, _height, memoryBitmapStride, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0));
            //_desktopFrame._texture2d = _texture2D;
            someBitmap.Save(@"C:\Users\ninekorn\Desktop\monoScreen\" + counter + ".png");
            //Console.WriteLine("anus");


            counter++;
            if (_screenResource!= null)
            {
                _screenResource.Dispose();
            }
          
            return true;
        }

        int counter = 0;
        Bitmap someBitmap;



        Stopwatch _countingTime = new Stopwatch();


        bool releasedFrame = true;
        bool releaseFrame()
        {
            //_texture2D.Dispose(); // lags like fucking hell
            for (int i = 0; i < 2; i++)
            {
                releasedFrame = true;
                try
                {
                    _outputDuplication.ReleaseFrame();
                }
                catch (SharpDXException ex)
                {
                    //SC_WPF_RENDER.SC_Graphics.SC_Models.SC_VR_Desktop_Screen._desktopDupe = new SC_SharpDX_ScreenCapture(0, 0,_device);
                    releasedFrame = false;
                    Console.WriteLine(ex.ToString());
                }

                if (releasedFrame)
                {
                    break;
                }
            }
            if (releasedFrame)
            {
                return true;
            }
            else
            {
                //SC_WPF_RENDER.SC_Graphics.SC_Models.SC_VR_Desktop_Screen._desktopDupe = new SC_SharpDX_ScreenCapture(0,0,_device);
                return false;
            }
        }

        public void Disposer()
        {
            _device.Dispose();
            _output1.Dispose();
            _texture2D.Dispose();
            _outputDuplication.Dispose();
            //_textureDescription.Dispose();
            _bitmap.Dispose();
            _screenResource.Dispose();
            //_desktopFrame.Dispose();
            GC.Collect();
        }

    }
}

//int memoryBitmapStride = _width * 4;
//im = new Bitmap(_width, _height, memoryBitmapStride, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0));