//
// Mono.Cairo.Context.cs
//
// Author:
//   Miguel de Icaza (miguel@novell.com)
//
// This is an OO wrapper API for the Cairo API.
//
// Copyright 2007 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

#nullable disable

namespace Cairo
{

    public class Path : IDisposable
	{
		IntPtr handle = IntPtr.Zero;

		internal Path (IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		~Path ()
		{
			Dispose (false);
		}

		public IntPtr Handle { get { return handle; } }

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<Path> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_path_destroy (handle);
			handle = IntPtr.Zero;
		}
	}
}
