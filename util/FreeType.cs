﻿using System;
using System.Runtime.InteropServices;

#nullable disable

namespace Cairo.Freetype
{
    public class FreeTypeHelper
    {
        public static IntPtr loadFreeTypeFont(string font)
        {
            System.IntPtr libptr;
            int ret = Library.FT_Init_FreeType(out libptr);
            if (ret != 0)
            {
                Console.WriteLine("Could not init freetype");
                return IntPtr.Zero;
            }

            //Once we have the library we create and load the font face
            //Face face;
            System.IntPtr faceptr;
            int retb = Face.FT_New_Face(libptr, font, 0, out faceptr);
            if (retb != 0)
            {
                Console.WriteLine("Could not init freetype face");
                return IntPtr.Zero;
            }

            //face = (Face)Marshal.PtrToStructure(faceptr, typeof(Face));

            //Freetype measures the font size in 1/64th of pixels for accuracy 
            //so we need to request characters in size*64
            //Face.FT_Set_Char_Size(faceptr, size << 6, size << 6, 96, 96);

            //Provide a reasonably accurate estimate for expected pixel sizes
            //when we later on create the bitmaps for the font
            //Face.FT_Set_Pixel_Sizes(faceptr, size, size);

            // Dispose of these as we don't need
            //Face.FT_Done_Face(faceptr);
            //Library.FT_Done_FreeType(libptr);

            return faceptr;
        }
    }

    public enum FT_LOAD_TYPES
    {
        FT_LOAD_DEFAULT = 0,
        FT_LOAD_NO_SCALE = 1
    }

    public enum FT_RENDER_MODES
    {
        FT_RENDER_MODE_NORMAL = 0,
        FT_RENDER_MODE_LIGHT = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Library
    {
        public System.IntPtr memory;
        public Generic generic;
        public int major;
        public int minor;
        public int patch;
        public uint modules;
        public System.IntPtr module0, module1, module2, module3, module4, module5, module6, module7, module8, module9, module10;
        public System.IntPtr module11, module12, module13, module14, module15, module16, module17, module18, module19, module20;
        public System.IntPtr module21, module22, module23, module24, module25, module26, module27, module28, module29, module30;
        public System.IntPtr module31;
        public ListRec renderers;
        public System.IntPtr renderer;
        public System.IntPtr auto_hinter;
        public System.IntPtr raster_pool;
        public long raster_pool_size;
        public System.IntPtr debug0, debug1, debug2, debug3;
        [DllImport("freetype6")]
        public static extern int FT_Init_FreeType(out System.IntPtr lib);
        [DllImport("freetype6")]
        public static extern void FT_Done_FreeType(System.IntPtr lib);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Generic
    {
        public System.IntPtr data;
        public System.IntPtr finalizer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class BBox
    {
        public int xMin, yMin;
        public int xMax, yMax;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ListRec
    {
        public System.IntPtr head;
        public System.IntPtr tail;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Vector
    {
        public int x;
        public int y;
    }


    [StructLayout(LayoutKind.Sequential)]
    public class Face
    {
        public int num_faces;
        public int face_index;
        public int face_flags;
        public int style_flags;
        public int num_glyphs;
        public string family_name;
        public string style_name;
        public int num_fixed_sizes;
        public System.IntPtr available_sizes;
        public int num_charmaps;
        public System.IntPtr charmaps;
        public Generic generic;
        public BBox box;
        public ushort units_per_EM;
        public short ascender;
        public short descender;
        public short height;
        public short max_advance_width;
        public short max_advance_height;
        public short underline_position;
        public short underline_tickness;
        public System.IntPtr glyphrec;
        public System.IntPtr size;
        public System.IntPtr charmap;
        public System.IntPtr driver;
        public System.IntPtr memory;
        public System.IntPtr stream;
        public ListRec sizes_list;
        public Generic autohint;
        public System.IntPtr extensions;
        public System.IntPtr internal_face;

        [DllImport("freetype6")]
        public static extern int FT_New_Face(System.IntPtr lib,
            string fname,
            int index,
            out System.IntPtr face);

        [DllImport("freetype6")]
        public static extern void FT_Set_Char_Size(System.IntPtr face,
            int width,
            int height,
            int horz_resolution,
            int vert_resolution);

        [DllImport("freetype6")]
        public static extern void FT_Set_Pixel_Sizes(System.IntPtr face,
            int pixel_width,
            int pixel_height);

        [DllImport("freetype6")]
        public static extern void FT_Done_Face(System.IntPtr face);

        [DllImport("freetype6")]
        public static extern int FT_Get_Char_Index(System.IntPtr face, char c);

        [DllImport("freetype6")]
        public static extern int FT_Load_Glyph(System.IntPtr face, int index, FT_LOAD_TYPES flags);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GlyphRec
    {
        public System.IntPtr library;
        public System.IntPtr clazz;
        public int format;
        public Vector advance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GlyphClass
    {
        public int size;
        public uint format;
        public System.IntPtr init;
        public System.IntPtr done;
        public System.IntPtr copy;
        public System.IntPtr transform;
        public System.IntPtr bbox;
        public System.IntPtr prepare;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class BitmapGlyph
    {
        public GlyphRec root;
        public int left;
        public int top;
        public Bitmap bitmap;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Bitmap
    {
        public int rows;
        public int width;
        public int pitch;
        public IntPtr buffer;
        public short num_grays;
        public sbyte pixel_mode;
        public sbyte palette_mode;
        public IntPtr palette;
    }


    [StructLayout(LayoutKind.Sequential)]
    public class Glyph
    {
        [DllImport("freetype6")]
        public static extern int FT_Get_Glyph(System.IntPtr glyphrec,
            out System.IntPtr glyph);
        [DllImport("freetype6")]
        public static extern void FT_Glyph_To_Bitmap(out System.IntPtr glyph,
            FT_RENDER_MODES render_mode,
            int origin,
            int destroy);
    }
}
