    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Silic5
{
    public static class ShutdownHelper
    {
        private const uint SHTDN_REASON_MAJOR_HARDWARE = 0x00020000;
        private const uint SHTDN_REASON_MINOR_POWER_SUPPLY = 0x0000000A;
        private const uint EWX_POWEROFF = 0x00000008;
        private const uint EWX_FORCE = 0x00000004;

        private enum SHUTDOWN_ACTION : uint //All was references and ideas from monoxide.exe
        {
            ShutdownNoReboot = 0,
            ShutdownReboot = 1,
            ShutdownPowerOff = 2
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibraryW(string lpLibFileName);
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int RtlAdjustPrivilegeDelegate(
            uint Privilege,
            byte Enable,
            byte CurrentThread,
            out byte Enabled);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int NtShutdownSystemDelegate(SHUTDOWN_ACTION Action);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int NtSetSystemPowerStateDelegate(
            uint SystemAction,
            uint MinSystemState,
            uint Flags,
            byte DisableWakes);

        public static bool ForceShutdownComputer()
        {
            IntPtr hNtDll = LoadLibraryW("ntdll.dll");
            if (hNtDll == IntPtr.Zero) return false;

            IntPtr pRtlAdjustPrivilege = GetProcAddress(hNtDll, "RtlAdjustPrivilege");
            IntPtr pNtShutdownSystem = GetProcAddress(hNtDll, "NtShutdownSystem");
            IntPtr pNtSetSystemPowerState = GetProcAddress(hNtDll, "NtSetSystemPowerState");

            if (pRtlAdjustPrivilege != IntPtr.Zero)
            {
                var RtlAdjustPrivilege = (RtlAdjustPrivilegeDelegate)Marshal.GetDelegateForFunctionPointer(
                    pRtlAdjustPrivilege, typeof(RtlAdjustPrivilegeDelegate));

                int ntReturnValue = RtlAdjustPrivilege(19, 1, 0, out byte bUnused);
                if (ntReturnValue != 0)
                {
                    return false;
                }
            }

            if (pNtSetSystemPowerState != IntPtr.Zero)
            {
                var NtSetSystemPowerState = (NtSetSystemPowerStateDelegate)Marshal.GetDelegateForFunctionPointer(
                    pNtSetSystemPowerState, typeof(NtSetSystemPowerStateDelegate));

                int ntReturnValue = NtSetSystemPowerState(
                    6,
                    6,
                    SHTDN_REASON_MAJOR_HARDWARE | SHTDN_REASON_MINOR_POWER_SUPPLY,
                    1);

                if (ntReturnValue == 0) return true;
            }

            if (pNtShutdownSystem != IntPtr.Zero)
            {
                var NtShutdownSystem = (NtShutdownSystemDelegate)Marshal.GetDelegateForFunctionPointer(
                    pNtShutdownSystem, typeof(NtShutdownSystemDelegate));

                int ntReturnValue = NtShutdownSystem(SHUTDOWN_ACTION.ShutdownPowerOff);
                if (ntReturnValue == 0) return true;
            }

            return ExitWindowsEx(EWX_POWEROFF, EWX_FORCE);
        }
    }
} 