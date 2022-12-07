using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAN_CHECK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            update();
            MessageBox.Show("Thank You For Using HARDWARE ID, Developed By CPU#9995. To Exit The Application, Click Anywhere In Blank Space! Enjoy!");
        }

        private void update()
        {
            ManagementObjectSearcher searcherWin32_Processor = new ManagementObjectSearcher("SELECT Name, SystemName, ProcessorId, SerialNumber FROM Win32_Processor");
            ManagementObjectSearcher searcherWin32_CsProduct = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");
            ManagementObjectSearcher searcherWin32_OperatingSystem = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcherWin32_DiskDrive = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive");
            ManagementObjectSearcher searcherWin32_LogicalDisk = new ManagementObjectSearcher("SELECT VolumeSerialNumber, Name FROM Win32_LogicalDisk");
            ManagementObjectSearcher searcherWin32_BaseBoard = new ManagementObjectSearcher("SELECT SerialNumber, Product, Version FROM Win32_BaseBoard");
            ManagementObjectSearcher searcherWin32_NetworkAdapter = new ManagementObjectSearcher("SELECT Name, MACAddress FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searcherWin32_BIOS = new ManagementObjectSearcher("SELECT Name, Version, SerialNumber FROM Win32_BIOS");

            foreach (ManagementObject info in searcherWin32_Processor.Get())
            {
                label2.Text = info["Name"].ToString();
            }

            foreach (ManagementObject info in searcherWin32_CsProduct.Get())
            {
                label7.Text = info["UUID"].ToString();
            }
            
            RegistryKey keyBaseX64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey keyBaseX86 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey keyX64 = keyBaseX64.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey keyX86 = keyBaseX86.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
            object resultObjX64 = keyX64.GetValue("MachineGuid", (object)"default");
            object resultObjX86 = keyX86.GetValue("MachineGuid", (object)"default");
            if (resultObjX64 != null && resultObjX64.ToString() != "default")
            {
                label8.Text = resultObjX64.ToString();
            }
            if (resultObjX86 != null && resultObjX86.ToString() != "default")
            {
                label8.Text = resultObjX86.ToString();
            }
            
            foreach (ManagementObject info in searcherWin32_BaseBoard.Get())
            {
                label3.Text = info["Product"].ToString();
            }

            label5.Text = "";
            foreach (ManagementObject info in searcherWin32_NetworkAdapter.Get())
            {
                object macNA = info["MACAddress"];
                if (!(macNA == null))
                {
                    string[] nameNA = info["Name"].ToString().Split(new Char[] { '\n' });
                    label5.Text = label5.Text + $"{macNA} \n";
                }
            }

            foreach (ManagementObject info in searcherWin32_BIOS.Get())
            {
                label14.Text = info["Name"].ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
