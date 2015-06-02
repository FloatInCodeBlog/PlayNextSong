using System.Windows.Automation;

namespace PlayNextSong
{
    public interface IKeyboardShortcutCommand
    {
        void Invoke();
        uint Modifiers { get; }
        uint VirtualKey { get; }
        int Id { get; set; }
        InvokePattern InvokePattern { get; }
        bool Initialize(); 
    }
}
