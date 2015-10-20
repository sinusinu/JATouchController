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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JATouchController
{
    public partial class frmMain : Form
    {
        private const int WM_POINTERUPDATE = 0x0245;
        private const int WM_POINTERDOWN = 0x0246;
        private const int WM_POINTERUP = 0x0247;
        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATE = 0x0003;
        private const int WM_POINTERACTIVATE = 0x024B;
        private const int PA_NOACTIVATE = MA_NOACTIVATE;

        private const int WM_GESTURE = 0x0119;

        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;

        private const int VK_ESCAPE = 0x001B;

        private const int VK_4 = 0x0034;
        private const int VK_5 = 0x0035;
        private const int VK_6 = 0x0036;
        private const int VK_7 = 0x0037;

        private const int VK_R = 0x0052;
        private const int VK_T = 0x0054;
        private const int VK_Y = 0x0059;
        private const int VK_U = 0x0055;

        private const int VK_F = 0x0046;
        private const int VK_G = 0x0047;
        private const int VK_H = 0x0048;
        private const int VK_J = 0x004A;

        private const int VK_V = 0x0056;
        private const int VK_B = 0x0042; 
        private const int VK_N = 0x004E;
        private const int VK_M = 0x004D;

        private int[] vkEnum = new int[] { VK_4, VK_R, VK_F, VK_V, VK_5, VK_T, VK_G, VK_B, VK_6, VK_Y, VK_H, VK_N, VK_7, VK_U, VK_J, VK_M };

        private class jaPointer
        {
            public int pid;    // unique id of the pointer (total touch count)
            public Point point;

            public jaPointer(int pid, Point point)
            {
                this.pid = pid;
                this.point = point;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        struct TestUnion
        {
            [FieldOffset(0)]
            public uint Number;

            [FieldOffset(0)]
            public ushort Low;

            [FieldOffset(2)]
            public ushort High;
        }

        [DllImport("user32.dll", EntryPoint="EnableMouseInPointer", CharSet=CharSet.Unicode, ExactSpelling=true, SetLastError=true)]
        public static extern int EnableMouseInPointer(bool fEnable);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        IntPtr ptrAnalyser = IntPtr.Zero;
        bool isAnalyserFound = false;

        List<jaPointer> listPointer = new List<jaPointer>(10);
        List<Rectangle> listPanel = new List<Rectangle>(16);

        Point outerPoint = new Point(-1023, -1023);

        int expandRate = 0;

        bool[] isTapped = new bool[16];
        bool[] nisTapped = new bool[16];

        bool exitFlag = true;
        bool escFlag = true;

        Pen sepPen;

        Rectangle clientRect;

        public frmMain()
        {
            InitializeComponent();

            // Uncomment this function to enable mouse interaction (for test purpose)
            //EnableMouseInPointer(true);

            sepPen = new Pen(Brushes.White);
            sepPen.Width = 2f;
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            clientRect = RectangleToScreen(this.ClientRectangle);
            float skWidth = clientRect.Width / 4;
            float skHeight = clientRect.Height / 4;
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawLine(sepPen, 0, 0, 0, clientRect.Height);
            e.Graphics.DrawLine(sepPen, skWidth, 0, skWidth * 1, clientRect.Height);
            e.Graphics.DrawLine(sepPen, skWidth * 2, 0, skWidth * 2, clientRect.Height);
            e.Graphics.DrawLine(sepPen, skWidth * 3, 0, skWidth * 3, clientRect.Height);
            e.Graphics.DrawLine(sepPen, clientRect.Width, 0, clientRect.Width, clientRect.Height);
            e.Graphics.DrawLine(sepPen, 0, 0, clientRect.Width, 0);
            e.Graphics.DrawLine(sepPen, 0, skHeight, clientRect.Width, skHeight * 1);
            e.Graphics.DrawLine(sepPen, 0, skHeight * 2, clientRect.Width, skHeight * 2);
            e.Graphics.DrawLine(sepPen, 0, skHeight * 3, clientRect.Width, skHeight * 3);
            e.Graphics.DrawLine(sepPen, 0, clientRect.Height, clientRect.Width, clientRect.Height);
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            clientRect = RectangleToScreen(this.ClientRectangle);
            CalculatePanel();
        }

        private void CalculatePanel()
        {
            float skWidth = this.ClientSize.Width / 4;
            float skHeight = this.ClientSize.Height / 4;

            int expandPixelX = (int)(skWidth * (expandRate / 100f));
            int expandPixelY = (int)(skHeight * (expandRate / 100f));

            listPanel.Clear();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    listPanel.Add(new Rectangle((int)(x * skWidth) - expandPixelX, (int)(y * skHeight) - expandPixelY, (int)(skWidth) + (expandPixelX * 2), (int)(skHeight) + (expandPixelY * 2)));
                }
            }

            for (int i = 0; i < 16; i++)
            {
                isTapped[i] = false;
            }
        }

        private void btnConnectToAnalyser_Click(object sender, EventArgs e)
        {
            if (tbxCurrentTitle.Text.Length == 0)
            {
                if (MessageBox.Show("Search for the title \"music select\"?", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tbxCurrentTitle.Text = "music select";
                }
                else
                {
                    MessageBox.Show("Enter the title of the game window!");
                    return;
                }
            }

            ptrAnalyser = IntPtr.Zero;

            ptrAnalyser = FindWindow("hspwnd0", tbxCurrentTitle.Text);

            if (ptrAnalyser == IntPtr.Zero)
            {
                MessageBox.Show("jubeat analyser not found!");
                isAnalyserFound = false;
                this.Opacity = 0.8f;
            }
            else
            {
                CalculatePanel();
                isAnalyserFound = true;
                this.Opacity = 0.2f;
                btnConnectToAnalyser.Hide();
                tbxCurrentTitle.Hide();
                label1.Hide();
                gbxTouchRange.Hide();
                exitFlag = false;
                escFlag = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.TopMost = true;
            }
        }

        private void UpdateKeyState()
        {
            SetForegroundWindow(ptrAnalyser);

            for (int i = 0; i < 16; i++)
            {
                nisTapped[i] = false;
            }

            foreach (jaPointer pointer in listPointer)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (listPanel[j].Contains(pointer.point))
                    {
                        nisTapped[j] = true;
                    }
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (isTapped[i] != nisTapped[i])
                {
                    if (nisTapped[i])
                    {
                        keybd_event((byte)vkEnum[i], 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    }
                    else
                    {
                        keybd_event((byte)vkEnum[i], 0, KEYEVENTF_KEYUP | 0, 0);
                    }
                }
            }

            Array.Copy(nisTapped, isTapped, nisTapped.Length);
        }

        private void OnPointerDown(int id, Point position)
        {
            if (isAnalyserFound)
            {
                bool isIdExist = false;
                foreach (jaPointer pointer in listPointer)
                {
                    if (id == pointer.pid) isIdExist = true;
                }
                if (isIdExist) return;
                jaPointer newPointer = new jaPointer(id, position);
                listPointer.Add(newPointer);
                UpdateKeyState();
            }
        }

        private void OnPointerUpdate(int id, Point position, bool isInteracting)
        {
            if (isInteracting && isAnalyserFound)
            {
                foreach (jaPointer pointer in listPointer)
                {
                    if (id == pointer.pid)
                    {
                        pointer.point = position;
                        break;
                    }
                }
                UpdateKeyState();
            }
        }

        private void OnPointerUp(int id, Point position)
        {
            if (isAnalyserFound)
            {
                jaPointer pointerToRemove = null;
                foreach (jaPointer pointer in listPointer)
                {
                    if (id == pointer.pid)
                    {
                        pointerToRemove = pointer;
                        break;
                    }
                }
                if (pointerToRemove != null) listPointer.Remove(pointerToRemove);
                UpdateKeyState();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_POINTERUPDATE)
            {
                IntPtr wp = m.WParam;
                var tu = new TestUnion { Number = (uint)wp };
                int pid = tu.Low;
                IntPtr xy = m.LParam;
                int x = unchecked((short)(long)xy);
                int y = unchecked((short)((long)xy >> 16));
                
                // convert into relative position
                x = (x - clientRect.Left);
                y = (y - clientRect.Top);

                // fix this with proper bit value...
                bool isInteracting = ((unchecked((short)((long)wp >> 16))) < 1024) || ((unchecked((short)((long)wp >> 16))) > 10240);

                OnPointerUpdate(pid, new Point(x, y), isInteracting);
            }

            else if (m.Msg == WM_POINTERDOWN)
            {
                IntPtr wp = m.WParam;
                var tu = new TestUnion { Number = (uint)wp };
                int pid = tu.Low; 
                IntPtr xy = m.LParam;
                int x = unchecked((short)(long)xy);
                int y = unchecked((short)((long)xy >> 16));

                // convert into relative position
                x = (x - clientRect.Left);
                y = (y - clientRect.Top);
                
                OnPointerDown(pid, new Point(x, y));
            }

            else if (m.Msg == WM_POINTERUP)
            {
                IntPtr wp = m.WParam;
                var tu = new TestUnion { Number = (uint)wp };
                int pid = tu.Low;
                IntPtr xy = m.LParam;
                int x = unchecked((short)(long)xy);
                int y = unchecked((short)((long)xy >> 16));

                // convert into relative position
                x = (x - clientRect.Left);
                y = (y - clientRect.Top);

                OnPointerUp(pid, new Point(x, y));
            }

            // prevent window from being activated
            else if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
                return;
            }
            else if (m.Msg == WM_POINTERACTIVATE)
            {
                m.Result = (IntPtr)PA_NOACTIVATE;
                return;
            }

            else if (m.Msg == WM_SYSCOMMAND) {
                if (isAnalyserFound)
                {
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE) return;
                }
            }

            else if (m.Msg == WM_GESTURE)
            {
                m.Result = (IntPtr)0;
                return;
            }

            base.WndProc(ref m);
        }

        private void tbrTouchRange_ValueChanged(object sender, EventArgs e)
        {
            lblTouchRange.Text = tbrTouchRange.Value + "%";
            expandRate = tbrTouchRange.Value;
            CalculatePanel();
        }

        private void tmrExitFlagRemover_Tick(object sender, EventArgs e)
        {
            sepPen.Color = Color.White;
            this.Invalidate();
            exitFlag = false;
            escFlag = false;
            tmrExitFlagRemover.Stop();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exitFlag)
            {
                sepPen.Color = Color.Red;
                this.Invalidate();
                e.Cancel = true;
                exitFlag = true;
                tmrExitFlagRemover.Stop();
                tmrExitFlagRemover.Start();
            }
            else
            {
                if (!escFlag)
                {
                    sepPen.Color = Color.Black;
                    this.Invalidate();
                    if (isAnalyserFound)
                    {
                        SetForegroundWindow(ptrAnalyser);
                        keybd_event((byte)VK_ESCAPE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                        Thread.Sleep(50);
                        keybd_event((byte)VK_ESCAPE, 0, KEYEVENTF_KEYUP | 0, 0);
                    }
                    e.Cancel = true;
                    escFlag = true;
                    tmrExitFlagRemover.Stop();
                    tmrExitFlagRemover.Start();
                }
                else
                {

                }
            }
        }
    }
}
