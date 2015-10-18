/*
 * Touch controller for jubeat analyser
 * 
 * Copyright © 2015 sinu <cpu344@gmail.com>
 * This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * the COPYING file for more details. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JATouchController
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if ((Environment.OSVersion.Version.Major < 6) || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 2))
            {
                MessageBox.Show("This program is designed to be run on Windows 8 or later.");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
