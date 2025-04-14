// In App.xaml.cs
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Oasis
{
    public partial class App : Application
    {
        private const string MutexName = "OasisApp";
        private const string PipeName = "OasisPipe";
        private Mutex _appMutex;
        private bool _isFirstInstance;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _appMutex = new Mutex(true, MutexName, out _isFirstInstance);

            if (!_isFirstInstance)
            {
                if (e.Args.Length > 0)
                {
                    await SendUriToExistingInstance(e.Args[0]);
                }
                Shutdown();
                return;
            }

            StartPipeServer();

            if (e.Args.Length > 0)
            {
                ProcessInitialUri(e.Args[0]);
            }
            else
            {
                await Task.Delay(500);
                NavigateToLogin();
            }
        }

        private void ProcessInitialUri(string uri)
        {
            Dispatcher.Invoke(() =>
            {
                if (MainWindow is MainWindow mainWindow)
                {
                    mainWindow.ProcessUriScheme(uri);
                    //mainWindow.NavMain();
                    BringWindowToFront();
                }
            });
        }

        private async Task SendUriToExistingInstance(string uri)
        {
            try
            {
                using (var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out))
                {
                    await client.ConnectAsync(1000);
                    using (var writer = new StreamWriter(client))
                    {
                        await writer.WriteLineAsync(uri);
                    }
                }
            }
            catch { /* Handle errors */ }
        }

        private void StartPipeServer()
        {
            Task.Run(async () =>
            {
                while (_isFirstInstance)
                {
                    using (var server = new NamedPipeServerStream(PipeName, PipeDirection.In))
                    {
                        try
                        {
                            Debug.WriteLine("Waiting for connection...");

                            await server.WaitForConnectionAsync();

                            Debug.WriteLine("Connection established.");

                            using (var reader = new StreamReader(server))
                            {
                                var uri = await reader.ReadLineAsync();
                                Debug.WriteLine($"Received URI: {uri}");

                                if (!string.IsNullOrEmpty(uri))
                                {
                                    Dispatcher.Invoke(() =>
                                    {
                                        if (MainWindow is MainWindow mainWindow)
                                        {
                                            Debug.WriteLine("Processing URI in MainWindow.");
                                            mainWindow.ProcessUriScheme(uri);
                                            //mainWindow.NavigateMain();
                                            BringWindowToFront();
                                        }
                                    });
                                }
                                else
                                {
                                    Debug.WriteLine("Received empty URI.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error in pipe server: {ex.Message}");
                        }
                    }
                }
            });
        }


        private void BringWindowToFront()
        {
            if (MainWindow != null)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                    MainWindow.WindowState = WindowState.Normal;

                MainWindow.Activate();
                MainWindow.Topmost = true;
                MainWindow.Topmost = false;
                MainWindow.Focus();
            }
        }

        private async void NavigateToLogin()
        {
            if (MainWindow is MainWindow mainWindow)
            {
                await Task.Delay(500);
                mainWindow.LoadThatShitUp();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _appMutex?.ReleaseMutex();
            _appMutex?.Close();
            base.OnExit(e);
        }
    }
}