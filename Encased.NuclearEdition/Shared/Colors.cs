using System;
using UnityEngine;

namespace Encased.NuclearEdition.Shared
{
    public static class Colors
    {
        // Pinkcolors
        public static Color Pink { get; } = MakeColor(0xFFC0CB);
        public static Color LightPink { get; } = MakeColor(0xFFB6C1);
        public static Color HotPink { get; } = MakeColor(0xFF69B4);
        public static Color DeepPink { get; } = MakeColor(0xFF1493);
        public static Color PaleVioletRed { get; } = MakeColor(0xDB7093);

        public static Color MediumVioletRed { get; } = MakeColor(0xC71585);

        //Redcolors
        public static Color LightSalmon { get; } = MakeColor(0xFFA07A);
        public static Color Salmon { get; } = MakeColor(0xFA8072);
        public static Color DarkSalmon { get; } = MakeColor(0xE9967A);
        public static Color LightCoral { get; } = MakeColor(0xF08080);
        public static Color IndianRed { get; } = MakeColor(0xCD5C5C);
        public static Color Crimson { get; } = MakeColor(0xDC143C);
        public static Color Firebrick { get; } = MakeColor(0xB22222);
        public static Color DarkRed { get; } = MakeColor(0x8B0000);

        public static Color Red { get; } = MakeColor(0xFF0000);

        //Orangecolors
        public static Color OrangeRed { get; } = MakeColor(0xFF4500);
        public static Color Tomato { get; } = MakeColor(0xFF6347);
        public static Color Coral { get; } = MakeColor(0xFF7F50);
        public static Color DarkOrange { get; } = MakeColor(0xFF8C00);

        public static Color Orange { get; } = MakeColor(0xFFA500);

        //Yellowcolors
        public static Color Yellow { get; } = MakeColor(0xFFFF00);
        public static Color LightYellow { get; } = MakeColor(0xFFFFE0);
        public static Color LemonChiffon { get; } = MakeColor(0xFFFACD);
        public static Color LightGoldenrodYellow { get; } = MakeColor(0xFAFAD2);
        public static Color PapayaWhip { get; } = MakeColor(0xFFEFD5);
        public static Color Moccasin { get; } = MakeColor(0xFFE4B5);
        public static Color PeachPuff { get; } = MakeColor(0xFFDAB9);
        public static Color PaleGoldenrod { get; } = MakeColor(0xEEE8AA);
        public static Color Khaki { get; } = MakeColor(0xF0E68C);
        public static Color DarkKhaki { get; } = MakeColor(0xBDB76B);

        public static Color Gold { get; } = MakeColor(0xFFD700);

        //Browncolors
        public static Color Cornsilk { get; } = MakeColor(0xFFF8DC);
        public static Color BlanchedAlmond { get; } = MakeColor(0xFFEBCD);
        public static Color Bisque { get; } = MakeColor(0xFFE4C4);
        public static Color NavajoWhite { get; } = MakeColor(0xFFDEAD);
        public static Color Wheat { get; } = MakeColor(0xF5DEB3);
        public static Color Burlywood { get; } = MakeColor(0xDEB887);
        public static Color Tan { get; } = MakeColor(0xD2B48C);
        public static Color RosyBrown { get; } = MakeColor(0xBC8F8F);
        public static Color SandyBrown { get; } = MakeColor(0xF4A460);
        public static Color Goldenrod { get; } = MakeColor(0xDAA520);
        public static Color DarkGoldenrod { get; } = MakeColor(0xB8860B);
        public static Color Peru { get; } = MakeColor(0xCD853F);
        public static Color Chocolate { get; } = MakeColor(0xD2691E);
        public static Color SaddleBrown { get; } = MakeColor(0x8B4513);
        public static Color Sienna { get; } = MakeColor(0xA0522D);
        public static Color Brown { get; } = MakeColor(0xA52A2A);

        public static Color Maroon { get; } = MakeColor(0x800000);

        //Greencolors
        public static Color DarkOliveGreen { get; } = MakeColor(0x556B2F);
        public static Color Olive { get; } = MakeColor(0x808000);
        public static Color OliveDrab { get; } = MakeColor(0x6B8E23);
        public static Color YellowGreen { get; } = MakeColor(0x9ACD32);
        public static Color LimeGreen { get; } = MakeColor(0x32CD32);
        public static Color Lime { get; } = MakeColor(0x00FF00);
        public static Color LawnGreen { get; } = MakeColor(0x7CFC00);
        public static Color Chartreuse { get; } = MakeColor(0x7FFF00);
        public static Color GreenYellow { get; } = MakeColor(0xADFF2F);
        public static Color SpringGreen { get; } = MakeColor(0x00FF7F);
        public static Color MediumSpringGreen { get; } = MakeColor(0x00FA9A);
        public static Color LightGreen { get; } = MakeColor(0x90EE90);
        public static Color PaleGreen { get; } = MakeColor(0x98FB98);
        public static Color DarkSeaGreen { get; } = MakeColor(0x8FBC8F);
        public static Color MediumAquamarine { get; } = MakeColor(0x66CDAA);
        public static Color MediumSeaGreen { get; } = MakeColor(0x3CB371);
        public static Color SeaGreen { get; } = MakeColor(0x2E8B57);
        public static Color ForestGreen { get; } = MakeColor(0x228B22);
        public static Color Green { get; } = MakeColor(0x008000);

        public static Color DarkGreen { get; } = MakeColor(0x006400);

        //Cyancolors
        public static Color Aqua { get; } = MakeColor(0x00FFFF);
        public static Color Cyan { get; } = MakeColor(0x00FFFF);
        public static Color LightCyan { get; } = MakeColor(0xE0FFFF);
        public static Color PaleTurquoise { get; } = MakeColor(0xAFEEEE);
        public static Color Aquamarine { get; } = MakeColor(0x7FFFD4);
        public static Color Turquoise { get; } = MakeColor(0x40E0D0);
        public static Color MediumTurquoise { get; } = MakeColor(0x48D1CC);
        public static Color DarkTurquoise { get; } = MakeColor(0x00CED1);
        public static Color LightSeaGreen { get; } = MakeColor(0x20B2AA);
        public static Color CadetBlue { get; } = MakeColor(0x5F9EA0);
        public static Color DarkCyan { get; } = MakeColor(0x008B8B);

        public static Color Teal { get; } = MakeColor(0x008080);

        //Bluecolors
        public static Color LightSteelBlue { get; } = MakeColor(0xB0C4DE);
        public static Color PowderBlue { get; } = MakeColor(0xB0E0E6);
        public static Color LightBlue { get; } = MakeColor(0xADD8E6);
        public static Color SkyBlue { get; } = MakeColor(0x87CEEB);
        public static Color LightSkyBlue { get; } = MakeColor(0x87CEFA);
        public static Color DeepSkyBlue { get; } = MakeColor(0x00BFFF);
        public static Color DodgerBlue { get; } = MakeColor(0x1E90FF);
        public static Color CornflowerBlue { get; } = MakeColor(0x6495ED);
        public static Color SteelBlue { get; } = MakeColor(0x4682B4);
        public static Color RoyalBlue { get; } = MakeColor(0x4169E1);
        public static Color Blue { get; } = MakeColor(0x0000FF);
        public static Color MediumBlue { get; } = MakeColor(0x0000CD);
        public static Color DarkBlue { get; } = MakeColor(0x00008B);
        public static Color Navy { get; } = MakeColor(0x000080);

        public static Color MidnightBlue { get; } = MakeColor(0x191970);

        //Purple,violet,andmagentacolors
        public static Color Lavender { get; } = MakeColor(0xE6E6FA);
        public static Color Thistle { get; } = MakeColor(0xD8BFD8);
        public static Color Plum { get; } = MakeColor(0xDDA0DD);
        public static Color Violet { get; } = MakeColor(0xEE82EE);
        public static Color Orchid { get; } = MakeColor(0xDA70D6);
        public static Color Fuchsia { get; } = MakeColor(0xFF00FF);
        public static Color Magenta { get; } = MakeColor(0xFF00FF);
        public static Color MediumOrchid { get; } = MakeColor(0xBA55D3);
        public static Color MediumPurple { get; } = MakeColor(0x9370DB);
        public static Color BlueViolet { get; } = MakeColor(0x8A2BE2);
        public static Color DarkViolet { get; } = MakeColor(0x9400D3);
        public static Color DarkOrchid { get; } = MakeColor(0x9932CC);
        public static Color DarkMagenta { get; } = MakeColor(0x8B008B);
        public static Color Purple { get; } = MakeColor(0x800080);
        public static Color Indigo { get; } = MakeColor(0x4B0082);
        public static Color DarkSlateBlue { get; } = MakeColor(0x483D8B);
        public static Color SlateBlue { get; } = MakeColor(0x6A5ACD);

        public static Color MediumSlateBlue { get; } = MakeColor(0x7B68EE);

        //Whitecolors
        public static Color White { get; } = MakeColor(0xFFFFFF);
        public static Color Snow { get; } = MakeColor(0xFFFAFA);
        public static Color Honeydew { get; } = MakeColor(0xF0FFF0);
        public static Color MintCream { get; } = MakeColor(0xF5FFFA);
        public static Color Azure { get; } = MakeColor(0xF0FFFF);
        public static Color AliceBlue { get; } = MakeColor(0xF0F8FF);
        public static Color GhostWhite { get; } = MakeColor(0xF8F8FF);
        public static Color WhiteSmoke { get; } = MakeColor(0xF5F5F5);
        public static Color Seashell { get; } = MakeColor(0xFFF5EE);
        public static Color Beige { get; } = MakeColor(0xF5F5DC);
        public static Color OldLace { get; } = MakeColor(0xFDF5E6);
        public static Color FloralWhite { get; } = MakeColor(0xFFFAF0);
        public static Color Ivory { get; } = MakeColor(0xFFFFF0);
        public static Color AntiqueWhite { get; } = MakeColor(0xFAEBD7);
        public static Color Linen { get; } = MakeColor(0xFAF0E6);
        public static Color LavenderBlush { get; } = MakeColor(0xFFF0F5);

        public static Color MistyRose { get; } = MakeColor(0xFFE4E1);

        //Grayandblackcolors
        public static Color Gainsboro { get; } = MakeColor(0xDCDCDC);
        public static Color LightGray { get; } = MakeColor(0xD3D3D3);
        public static Color Silver { get; } = MakeColor(0xC0C0C0);
        public static Color DarkGray { get; } = MakeColor(0xA9A9A9);
        public static Color Gray { get; } = MakeColor(0x808080);
        public static Color DimGray { get; } = MakeColor(0x696969);
        public static Color LightSlateGray { get; } = MakeColor(0x778899);
        public static Color SlateGray { get; } = MakeColor(0x708090);
        public static Color DarkSlateGray { get; } = MakeColor(0x2F4F4F);
        public static Color Black { get; } = MakeColor(0x000000);

        public static Color MakeColor(UInt32 value)
        {
            var b = ((value >> 8 * 0) & 0xFF) / 255f;
            var g = ((value >> 8 * 1) & 0xFF) / 255f;
            var r = ((value >> 8 * 2) & 0xFF) / 255f;
            return new Color(r, g, b);
        }

        public static Color A(this Color color, Byte a)
        {
            return new Color(color.r, color.g, color.b, a / 255f);
        }

        public static Color A(this Color color, Single a)
        {
            return new Color(color.r, color.g, color.b, a);
        }
    }
}