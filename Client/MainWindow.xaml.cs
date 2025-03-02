using Core;
using Core.LogModule;
using System.Diagnostics;
using System.Text.Json;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process process = null;
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    string A = Core.Network.Get.GetBody($"http://127.0.0.1:{Config.Core_RunConfig._Port}/api/init_inspect", false);
                    OperationQueue.pack<string>? OJ = JsonSerializer.Deserialize<OperationQueue.pack<string>>(A);
                    if (OJ != null && OJ.message.ToLower() == "ok")
                    {
                        this.Dispatcher.Invoke(() =>
                        {

                            WV2.Visibility = Visibility.Visible;
                            starttest.Visibility = Visibility.Collapsed;
                            this.WV2.Source = new Uri($"http://127.0.0.1:{Config.Core_RunConfig._Port}");
                        });
                        return;
                    }
                }
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //if (process != null)
            //{
            //    try
            //    {
            //        process.Close();
            //        process.Kill();
            //    }
            //    catch (Exception) { }
            //    process = null;
            //}
        }
    }
}
