using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace FECSim
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}