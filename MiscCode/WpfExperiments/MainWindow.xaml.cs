using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExperiments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var context = SynchronizationContext.Current;

            var action = new Action<string>(s => Debug.WriteLine(">>>>>Func call "+s+" thread: " + Thread.CurrentThread.ManagedThreadId));

            action("START");

            var thread = new Thread(OnBackgroundWork);
            
            thread.Start(context);
        }

        private void OnBackgroundWork(object obj)
        {
            var context = (SynchronizationContext)obj;

            Debug.WriteLine(">>>>>INSIDE thread: " + Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(2000);

            context.Post(s => Debug.WriteLine(">>>>>ON END thread: " + Thread.CurrentThread.ManagedThreadId+"|"+s), "Hey");
        }
    }
}
