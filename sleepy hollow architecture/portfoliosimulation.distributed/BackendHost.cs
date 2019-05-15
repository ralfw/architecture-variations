using System;
using System.Diagnostics;

namespace portfoliosimulation.distributed
{
    class BackendServer : IDisposable
    {
        private Process _process;
        
        public BackendServer() {
            var psi = new ProcessStartInfo();
            psi.FileName = "dotnet";
            psi.Arguments = "exec portfoliosimulation.backend.server.dll";
            psi.WorkingDirectory = "../portfoliosimulation.backend.server/bin/debug/netcoreapp2.2";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true; // to disconnect background from console UI of frontend
            _process = new Process {StartInfo = psi};
        }

        public void Run()
        {
            _process.Start();
        }
        
        public void Dispose()
        {
            _process.Kill();
        }
    }
}