using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.Class
{
    public class HttpServer
    {
        private HttpListener _listener;

        public void Start()
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("HttpListener is not supported on this platform.");
            }

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8080/");

            try
            {
                _listener.Start();
                Console.WriteLine("HTTP Listener started at http://localhost:8080/");

                while (_listener.IsListening)
                {
                    var context = _listener.GetContext();
                    var request = context.Request;

                    Console.WriteLine($"Received request: {request.Url}");

                    var response = context.Response;
                    var responseText = "Hello from HttpListener!";
                    var buffer = Encoding.UTF8.GetBytes(responseText);

                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting listener: {ex.Message}");
            }
        }

        public void Stop()
        {
            _listener?.Stop();
            _listener?.Close();
            Console.WriteLine("HTTP server stopped.");
        }
    }
}