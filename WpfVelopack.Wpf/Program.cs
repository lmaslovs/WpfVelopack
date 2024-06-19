using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velopack;

namespace WpfVelopack.Wpf
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            VelopackApp.Build().Run();
            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
