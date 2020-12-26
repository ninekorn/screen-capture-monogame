﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;
using SharpDX.Direct3D11;
using System.Drawing;

namespace SC_Mono
{
    public struct SC_SharpDX_ScreenFrame
    {
        public int width;
        public int height;
        //public byte[] bitmapByteArray;
        //public byte[] bitmapEmptyByteArray;
        //public Bitmap mouseCursor;
        //public byte[] cursorByteArray;
        //public System.Drawing.Point cursorPointPos;
        public Texture2D _texture2d;
        public int _desktopWidth;
        public int _desktopHeight;
    }
}
