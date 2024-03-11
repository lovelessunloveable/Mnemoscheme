using System;
using System.Windows;
using System.Windows.Input;
using WinInterop = System.Windows.Interop;
using Mnemoscheme.Models.WindowHelper;

namespace Mnemoscheme
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MaximizeHelper maximizeHelper;

        public MainWindow()
        {
            InitializeComponent();
            maximizeHelper = new MaximizeHelper((int)MinHeight, (int)MinWidth);
            this.SourceInitialized += new EventHandler(MainWindowSourceInitialized);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        void MainWindowSourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(maximizeHelper.WindowProc));
        }
    }
}