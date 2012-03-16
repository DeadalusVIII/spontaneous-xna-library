using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SXL.TextureManipulator
{
    public struct HSV
    {
        public float Hue;
        public float Saturation;
        public float Value;

        public HSV(Vector3 hsv)
        {
            Hue = hsv.X;
            Saturation = hsv.Y;
            Value = hsv.Z;
        }

        public HSV(Color color)
        {
            Hue = (float)color.Hue();
            Saturation = (float)color.Saturation();
            Value = (float)color.Value();
        }

        public HSV(float hue, float saturation, float value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }

        public Color ToColor()
        {
            
            /*float f, p, q, t;
            if (Saturation == 0)
            {
                // achromatic (grey)
                return new Color(Value, Value, Value);
            }
            
            float h = Hue / 60;			// sector 0 to 5
            int i = (int)Math.Floor(h);
            f = h - i;			// factorial part of h
            p = Value * (1 - Saturation);
            q = Value * (1 - Saturation * f);
            t = Value * (1 - Saturation * (1 - f));
            switch (i)
            {
                case 0:
                    return new Color(Value,t,p);
                case 1:
                    return new Color(q,Value,p);
                case 2:
                    return new Color(p,Value,t);
                case 3:
                    return new Color(p,q,Value);
                case 4:
                    return new Color(t,p,Value);
                default:		// case 5:
                    return new Color(Value,p,q);
            }*/
            if (Saturation == 0 && Hue == 0)
            {
                return Color.Black;
            }
            else
            {
                while (Hue >= 360)
                    Hue -= 360;
                while (Hue < 0)
                    Hue += 360;
                Hue /= 60;
                int i = (int)Math.Floor(Hue);
                float f = Hue - (float)i;
                float p = Value * (1 - Saturation);
                float q = Value * (1 - (Saturation * f));
                float t = Value * (1 - (Saturation * (1 - f)));
                switch (i)
                {
                    case 0:
                        return new Color(new Vector3(Value, t, p));
                    case 1:
                        return new Color(new Vector3(q, Value, p));
                    case 2:
                        return new Color(new Vector3(p, Value, t));
                    case 3:
                        return new Color(new Vector3(p, q, Value));
                    case 4:
                        return new Color(new Vector3(t, p, Value));
                    case 5:
                        return new Color(new Vector3(Value, p, q));
                    default:
                        Debug.Assert(false);
                        return Color.Black;
                }
            }
        }

    }

    public static class ColorToHSV
    {
        public static HSV ToHSV(this Color color)
        {
            return new HSV(color);
        }

        public static float Hue(this Color color)
        {
            Vector3 colorVec = color.ToVector3();
            float maxValue = (float)Math.Max(Math.Max(colorVec.X, colorVec.Y), colorVec.Z);
            float minValue = (float)Math.Min(Math.Min(colorVec.X, colorVec.Y), colorVec.Z);

            float delta = (maxValue - minValue);
            if (delta == 0)
                return 0;

            float hue = 0;
            if (colorVec.X == maxValue)
                hue = (colorVec.Y - colorVec.Z) / delta;
            else if (colorVec.Y == maxValue)
                hue = 2 + (colorVec.Z - colorVec.X) / delta;
            else if (colorVec.Z == maxValue)
                hue = 4 + (colorVec.X - colorVec.Y) / delta;
            hue *= 60;
            if (hue < 0)
                hue += 360;

            return hue;
        }

        public static float Saturation(this Color color)
        {
            Vector3 colorVec = color.ToVector3();
            float maxValue = (float)Math.Max(Math.Max(colorVec.X, colorVec.Y), colorVec.Z);
            if (maxValue == 0)
                return 0;
            float minValue = (float)Math.Min(Math.Min(colorVec.X, colorVec.Y), colorVec.Z);
            return (maxValue - minValue) / maxValue;
        }

        public static float Value(this Color color)
        {
            Vector3 colorVec = color.ToVector3();
            float maxValue = (float)Math.Max(Math.Max(colorVec.X, colorVec.Y), colorVec.Z);
            return maxValue;
        }
    }
}
