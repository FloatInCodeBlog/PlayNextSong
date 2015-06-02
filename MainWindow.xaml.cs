using System.Windows;
using System.Windows.Interop;

namespace PlayNextSong
{
    public partial class MainWindow : Window
    {
        private Shortcuts _shortcuts;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _shortcuts = new Shortcuts();
            _shortcuts.AddShortcut(new PlayPauseCommand());
            _shortcuts.AddShortcut(new NextCommand());
            _shortcuts.AddShortcut(new PreviousCommand());

            var handle = new WindowInteropHelper(this).Handle;
             
            _shortcuts.Register(handle);
        }
    }
}