using System;
using servicehost;

namespace portfoliosimulation.backend.server
{
    class Program
    {
        static void Main(string[] args) {
            BackendController.Handler = new MessageHandling();
            
            using(var host = new ServiceHost()) {
                host.Start(new Uri("http://localhost:8080"), new[]{typeof(BackendController)});
                Console.WriteLine("Backend running...");
                Console.ReadLine();
            }
        }
    }
}
