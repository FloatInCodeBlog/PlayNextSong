using System;
using System.Windows.Automation;

namespace PlayNextSong
{
    public class NextCommand: IKeyboardShortcutCommand
    {
        public InvokePattern InvokePattern { get; private set; }

        public NextCommand()
        {
            Id = Guid.NewGuid().GetHashCode();
        }

        public int Id
        {
            get;
            set;
        }

        public uint Modifiers
        {
            get
            {
                return (uint)KeyboardModifierKeys.MOD_NONE;
            }
        }

        public uint VirtualKey
        {
            get
            {
                return (uint)KeyCodes.VK_MEDIA_NEXT_TRACK;
            }
        }

        public void Invoke()
        {
            if (InvokePattern != null)
            {
                try
                {
                    InvokePattern.Invoke();
                }
                catch (Exception)
                {
                    Initialize();
                    InvokePattern.Invoke();
                }
            }
            else
            {
                Initialize();
                InvokePattern.Invoke();
            }
        }

        public bool Initialize()
        {
            string lpszParentClass = "IEFrame";
            IntPtr ParenthWnd = new IntPtr(0);
            ParenthWnd = NativeMethods.FindWindow(lpszParentClass, null);
            if (!ParenthWnd.Equals(IntPtr.Zero))
            {
                var nameProperty = new PropertyCondition(AutomationElement.NameProperty, "Next");
                var frameworkIdProperty = new PropertyCondition(AutomationElement.FrameworkIdProperty, "InternetExplorer");
                var controlTypeProperty = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
                var hasInvokePatter = new PropertyCondition(AutomationElement.IsInvokePatternAvailableProperty, true);

                var condition = new AndCondition(controlTypeProperty, hasInvokePatter, nameProperty, frameworkIdProperty);
                var elementCollection = AutomationElement.FromHandle(ParenthWnd).FindAll(TreeScope.Subtree | TreeScope.Element, condition);

                foreach (AutomationElement item in elementCollection)
                {
                    InvokePattern = (InvokePattern)item.GetCurrentPattern(InvokePattern.Pattern);
                    return true;
                }
            }

            return false;
        }
    }

}
