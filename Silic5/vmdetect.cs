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
            bool vmware =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\VMware, Inc.\VMware Tools") != null ||
                Registry.CurrentUser.OpenSubKey(@"Software\VMware, Inc.\VMware Tools") != null ||
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\VMTools") != null;

            bool virtualBox =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oracle\VirtualBox Guest Additions") != null;

            if (vmware)
            {
                MessageBox.Show(
                    "I think you're running me inside VMware\n\n is that correct? VMware Workstation right?",
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
