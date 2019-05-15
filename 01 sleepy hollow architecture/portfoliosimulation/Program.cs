using System;
using onlinestocks;
using onlinestocks.alphavantage;
using portfoliosimulation.backend;
using portfoliosimulation.contract;
using portfoliosimulation.frontend;

namespace portfoliosimulation
{
    class Program
    {
        static void Main(string[] args) {
            var backend = new MessageHandling();
            var frontend = new UserInterface(backend);

            frontend.Run();
        }
    }
}