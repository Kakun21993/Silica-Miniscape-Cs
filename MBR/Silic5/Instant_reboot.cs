using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Runtime.InteropServices;

using System.Diagnostics;



namespace Silic5

{

    public static class RebootHelper

    {

        private const uint SHTDN_REASON_MAJOR_HARDWARE = 0x00020000;

        private const uint SHTDN_REASON_MINOR_POWER_SUPPLY = 0x0000000A;

        private const uint EWX_REBOOT = 0x00000002;

        private const uint EWX_FORCE = 0x00000004;



        private enum REBOOT_ACTION : uint

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



        // Forces __stdcall and uses 1-byte elements for native BOOLEAN types

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]

        private delegate int RtlAdjustPrivilegeDelegate(

            uint Privilege,

            byte Enable,

            byte CurrentThread,

            out byte Enabled);



        [UnmanagedFunctionPointer(CallingConvention.StdCall)]

        private delegate int NtShutdownSystemDelegate(REBOOT_ACTION Action);



        // Corrected 4-parameter signature for the native NtSetSystemPowerState api

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]

        private delegate int NtSetSystemPowerStateDelegate(

            uint SystemAction,

            uint MinSystemState,

            uint Flags,

            byte DisableWakes);



        public static bool ForceRebootComputer()

        {

            IntPtr hNtDll = LoadLibraryW("ntdll.dll");

            if (hNtDll == IntPtr.Zero) return false;



            IntPtr pRtlAdjustPrivilege = GetProcAddress(hNtDll, "RtlAdjustPrivilege");

            IntPtr pNtShutdownSystem = GetProcAddress(hNtDll, "NtShutdownSystem");

            IntPtr pNtSetSystemPowerState = GetProcAddress(hNtDll, "NtSetSystemPowerState");



            // 1. Escalate privileges to allow shutdown (SeShutdownPrivilege = 19)

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



            // 2. Try native kernel power-state pull-the-plug method

            if (pNtSetSystemPowerState != IntPtr.Zero)

            {

                var NtSetSystemPowerState = (NtSetSystemPowerStateDelegate)Marshal.GetDelegateForFunctionPointer(

                    pNtSetSystemPowerState, typeof(NtSetSystemPowerStateDelegate));



                // PowerActionShutdownOff = 6, PowerSystemShutdown = 6

                int ntReturnValue = NtSetSystemPowerState(

                    6,

                    6,

                    SHTDN_REASON_MAJOR_HARDWARE | SHTDN_REASON_MINOR_POWER_SUPPLY,

                    1); // DisableWakes = true



                if (ntReturnValue == 0) return true;

            }



            // 3. Try standard native NT system shutdown method

            if (pNtShutdownSystem != IntPtr.Zero)

            {

                var NtShutdownSystem = (NtShutdownSystemDelegate)Marshal.GetDelegateForFunctionPointer(

                    pNtShutdownSystem, typeof(NtShutdownSystemDelegate));



                int ntReturnValue = NtShutdownSystem(REBOOT_ACTION.ShutdownReboot);

                if (ntReturnValue == 0) return true;

            }



            // 4. Ultimate fallback to standard forced user API

            return ExitWindowsEx(EWX_REBOOT, EWX_FORCE);

        }

    }

}