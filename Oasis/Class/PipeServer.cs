using Oasis;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows;

public class PipeServer
{
    private const string PipeName = "OasisPipe";
    private Thread _serverThread;

    public void StartServer()
    {
        _serverThread = new Thread(() =>
        {
            while (true)
            {
                using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
                {
                    Console.WriteLine("Waiting for a connection...");
                    pipeServer.WaitForConnection();

                    using (StreamReader reader = new StreamReader(pipeServer))
                    {
                        string message = reader.ReadLine();
                        if (!string.IsNullOrEmpty(message))
                        {
                            Console.WriteLine($"Received message: {message}");
                            HandleCallback(message); // Process the incoming message
                        }
                    }
                }
            }
        });

        _serverThread.IsBackground = true;
        _serverThread.Start();
    }

    private void HandleCallback(string message)
    {
        Console.WriteLine($"Callback handled: {message}");
        App.Current.Dispatcher.Invoke(() =>
        {
            MessageBox.Show($"Callback recieved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }
}
