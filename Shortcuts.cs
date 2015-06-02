using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Interop;

namespace PlayNextSong
{
    public class Shortcuts
    {
        private IList<IKeyboardShortcutCommand> _shortcuts = new List<IKeyboardShortcutCommand>();
        private int WM_HOTKEY = 0x0312;
        private IntPtr windowHook = IntPtr.Zero;
        private HwndSource _source = null;

        public void AddShortcut(IKeyboardShortcutCommand shortcut)
        {
            _shortcuts.Add(shortcut);
        }

        public void RemoveShortcut(IKeyboardShortcutCommand shortcut)
        {
            var toRemove = _shortcuts.FirstOrDefault(i => i.Id == shortcut.Id);
            if (toRemove != null)
            {
                _shortcuts.Remove(toRemove);
            }
        }

        public void Register(IntPtr windowHandle)
        {
            windowHook = windowHandle;

            _source = HwndSource.FromHwnd(windowHook);
            _source.AddHook(HwndHook);

            for (int i = 0; i < _shortcuts.Count; i++)
            {
                if (!NativeMethods.RegisterHotKey(windowHook, _shortcuts[i].Id,
                        _shortcuts[i].Modifiers,
                        _shortcuts[i].VirtualKey))
                {
                    throw new Exception(message: string.Format("Unable to register hotkey. Modifiers: {0}, VK: {1}", _shortcuts[i].Modifiers, _shortcuts[i].VirtualKey));
                }
            }
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                var unInitialized = _shortcuts.Where(x => x.InvokePattern == null);
                foreach (var command in unInitialized)
                {
                    command.Initialize();
                }

                var matched = _shortcuts.Where(x => x.Id == wParam.ToInt32()).ToList();

                foreach (var command in matched)
                {
                    command.Invoke();
                }
            }
            return IntPtr.Zero;
        }

        public void Dispose()
        {
            _shortcuts.Clear();
        }
    }
}
