using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Mongo;
using DddInPractice.UI.Management;

namespace DddInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public DashboardViewModel Dashboard { get; private set; }

        public MainViewModel(ISnackMachineRepository smRep, IAtmRepository atmRep, IHeadOfficeRepository hoRep, EventDispatcher dispatcher)
        {
            Dashboard = new DashboardViewModel(smRep, atmRep, hoRep, dispatcher);
        }
    }
}
