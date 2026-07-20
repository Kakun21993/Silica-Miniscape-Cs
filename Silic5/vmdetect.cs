using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace Silic5
{
    internal class vmdetect
    {
        public vmdetect()
        {
            bool vmwareManufacturer = false;
            bool virtualBoxBios = false;
            using (RegistryKey bios = Registry.LocalMachine.OpenSubKey(
                @"HARDWARE\DESCRIPTION\System\BIOS"))
            {
                if (bios != null)
                {
                    string manufacturer = bios.GetValue("SystemManufacturer") as string ?? "";
                    string productName = bios.GetValue("SystemProductName") as string ?? "";
                    string baseBoard = bios.GetValue("BaseBoardProduct") as string ?? "";

                    vmwareManufacturer = manufacturer.Equals(
                        "VMware, Inc.",
                        StringComparison.OrdinalIgnoreCase);

                    virtualBoxBios =
                        productName.Equals("VirtualBox", StringComparison.OrdinalIgnoreCase) ||
                        baseBoard.Equals("VirtualBox", StringComparison.OrdinalIgnoreCase);

                }
            }
            bool vmware =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\VMware, Inc.\VMware Tools") != null ||
                Registry.CurrentUser.OpenSubKey(@"Software\VMware, Inc.\VMware Tools") != null ||
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\ControlSet001\Services\VMTools") != null ||
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\VMTools") != null ||
                vmwareManufacturer;

            bool virtualBox =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oracle\VirtualBox Guest Additions") != null ||
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\ControlSet001\Services\VBoxGuest") != null ||
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\VBoxGuest") != null ||
                virtualBoxBios;

            if (vmware)
            {
                MessageBox.Show(
                    "H-hi I-i think you're running me inside VMware\n\n is that that correct? :D\n\n Good luck btw",
                    "I Noticed Something",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (virtualBox)
            {
                MessageBox.Show(
                    "A-are you running me inside VirtualBox?\n\nOr... Isn't it? \n\nI mean VMware Workstation Pro is free tho..",
                    "I Noticed Something",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
