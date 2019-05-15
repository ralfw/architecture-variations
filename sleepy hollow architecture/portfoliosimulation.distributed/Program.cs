using System;
using System.Threading;
using portfoliosimulation.backend;
using portfoliosimulation.contract.data.messages.commands;
using portfoliosimulation.contract.data.messages.queries;
using portfoliosimulation.frontend;

namespace portfoliosimulation.distributed
{
    class Program
    {
        static void Main(string[] args) { 
            using (var backend = new BackendServer()) {
                var backendProxy = new BackendProxy(); 
                var frontend = new UserInterface(backendProxy);
            
                Start(backend);
                frontend.Run();
            }


            void Start(BackendServer backend) {
                backend.Run();
                Console.Write("Waiting... ");
                Thread.Sleep(5000);
            }
        }
    }
}