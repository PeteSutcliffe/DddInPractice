using System.Windows;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Logic.Utils;
using DddInPractice.Mongo;
using DddInPractice.UI.Common;

namespace DddInPractice.UI
{
    public partial class App
    {
        private static WindsorContainer container;

        public App()
        {
            InitializeContainer();
        }

        private void InitializeContainer()
        {
            container = new WindsorContainer();
            container.Install(FromAssembly.This());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Initer.Init(container.Resolve<IHeadOfficeRepository>(), 
                container.Resolve<ISnackMachineRepository>(), 
                container.Resolve<IAtmRepository>());

            var viewModel = container.Resolve<MainViewModel>();
            var window = new MainWindow { DataContext = viewModel };
            window.Show();
        }
    }
}
