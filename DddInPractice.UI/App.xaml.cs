using DddInPractice.Logic.Utils;
using DddInPractice.Mongo;

namespace DddInPractice.UI
{
    public partial class App
    {
        public App()
        {
            Initer.Init(new HeadOfficeRepository(), new SnackMachineRepository(), new AtmRepository());
        }
    }
}
