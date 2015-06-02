using System;
using System.Runtime.InteropServices;

namespace PlayNextSong
{
    public static class NativeMethods
    {
        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(
        [In] IntPtr hWnd,
        [In] int id,
        [In] uint fsModifiers,
        [In] uint vk);

        [DllImport("User32.dll")]
        public static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(
            [In] uint dwFlags,
            [In] uint dx,
            [In] uint dy,
            [In] int dwData,
            [In] uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(
            [In] int X,
            [In] int Y);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]

        public static extern void keybd_event(
           [In] byte bVk, 
           [In] byte bScan, 
           [In] uint dwFlags,
           [In] UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(
           [In] IntPtr idHook, 
           [In] int nCode,
           [In] int wParam, 
           [In] [Out] ref KeyboardHookStruct lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(
            [In] string lpFileName);

        public struct KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        public delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(
            int idHook, 
            KeyboardHookProc callback, 
            IntPtr hInstance, 
            uint threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(
            IntPtr idHook);

        [DllImport("USER32.dll")]
        public static extern short GetKeyState(
            int nVirtKey);

        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
