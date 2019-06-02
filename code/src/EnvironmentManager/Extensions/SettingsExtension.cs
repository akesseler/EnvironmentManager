/*
 * MIT License
 * 
 * Copyright (c) 2019 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Extensions
{
    public static class SettingsExtension
    {
        private static Char[] Delimiters = new Char[] { ',', ';', ':' };
        private static Char Delimiter = SettingsExtension.Delimiters[0];

        public static Rectangle StandardBounds(this Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException();
            }

            return form.StandardBounds(form.Size);
        }

        public static Rectangle StandardBounds(this Form form, Size suggestion)
        {
            if (form == null)
            {
                throw new ArgumentNullException();
            }

            Size minimumSize = form.MinimumSize;
            Size currentSize = suggestion;
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            Int32 w = SettingsExtension.ApplyLimitations(currentSize.Width, minimumSize.Width, workingArea.Width);
            Int32 h = SettingsExtension.ApplyLimitations(currentSize.Height, minimumSize.Height, workingArea.Height);

            Size s = new Size(w, h);
            Point p = SettingsExtension.ScreenCentered(s);

            return new Rectangle(p, s);
        }

        public static void EnsureScreenLocation(this Form form)
        {
            if (form == null)
            {
                return;
            }

            Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
            Point location = form.Location;
            Size size = form.Size;

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.Contains(new Rectangle(location, size)))
                {
                    return; // Location is still within bounds.
                }
            }

            // Adjust X location to be on the primary screen!

            Int32 x = 0;
            if (location.X < bounds.Left)
            {
                x = bounds.Left;
            }
            else if (location.X + size.Width > bounds.Left + bounds.Right)
            {
                x = bounds.Right - size.Width;
            }
            else
            {
                x = location.X;
            }

            // Adjust Y location to be on the primary screen!

            Int32 y = 0;
            if (location.Y < bounds.Top)
            {
                y = bounds.Top;
            }
            else if (location.Y + size.Height > bounds.Top + bounds.Bottom)
            {
                y = bounds.Bottom - size.Height;
            }
            else
            {
                y = location.Y;
            }

            form.Location = new Point(x, y);
        }

        public static Point ScreenCentered(this Size size)
        {
            Rectangle bounds = Screen.PrimaryScreen.WorkingArea;

            return new Point(
                (bounds.Width - SettingsExtension.ApplyLimitations(size.Width, 0, bounds.Width)) / 2,
                (bounds.Height - SettingsExtension.ApplyLimitations(size.Height, 0, bounds.Height)) / 2
            );
        }

        public static Boolean IsValid(this Rectangle value)
        {
            return value != null && value.Left >= 0 && value.Top >= 0 && value.Width > 0 && value.Height > 0;
        }

        public static Rectangle StringToBounds(this String value, Rectangle standard)
        {
            if (!standard.IsValid())
            {
                throw new ArgumentOutOfRangeException();
            }

            Rectangle result = standard;

            if (String.IsNullOrWhiteSpace(value))
            {
                return result;
            }

            String[] array = value.Split(SettingsExtension.Delimiters, StringSplitOptions.None);

            if (array.Length > 0 && Int32.TryParse(array[0].Trim(), out Int32 x))
            {
                result.X = x;
            }
            else
            {
                result.X = standard.X;
            }

            if (array.Length > 1 && Int32.TryParse(array[1].Trim(), out Int32 y))
            {
                result.Y = y;
            }
            else
            {
                result.Y = standard.Y;
            }

            if (array.Length > 2 && Int32.TryParse(array[2].Trim(), out Int32 w))
            {
                result.Width = w;
            }
            else
            {
                result.Width = standard.Width;
            }

            if (array.Length > 3 && Int32.TryParse(array[3].Trim(), out Int32 h))
            {
                result.Height = h;
            }
            else
            {
                result.Height = standard.Height;
            }

            return result;
        }

        public static String BoundsToString(this Rectangle value)
        {
            if (!value.IsValid())
            {
                return String.Empty;
            }

            return $"{value.X}{SettingsExtension.Delimiter}{value.Y}{SettingsExtension.Delimiter}{value.Width}{SettingsExtension.Delimiter}{value.Height}";
        }

        public static Int32 StringToInteger(this String value, Int32 lower, Int32 upper, Int32 standard)
        {
            if (Int32.TryParse(value, out Int32 result))
            {
                return SettingsExtension.ApplyLimitations(result, lower, upper);
            }
            else
            {
                return standard;
            }
        }

        public static String IntegerToString(this Int32 value)
        {
            return value.ToString();
        }

        private static Int32 ApplyLimitations(Int32 value, Int32 lower, Int32 upper)
        {
            if (value < lower)
            {
                return lower;
            }

            if (value > upper)
            {
                return upper;
            }

            return value;
        }
    }
}
