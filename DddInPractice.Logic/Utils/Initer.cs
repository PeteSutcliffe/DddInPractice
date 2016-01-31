using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.SnackMachines;

namespace DddInPractice.Logic.Utils
{
    public static class Initer
    {
        public static void Init(IHeadOfficeRepository headOfficeRepository, 
            ISnackMachineRepository snackMachineRepository,
            IAtmRepository atmRepository)
        {
            SetupSnackMachine(snackMachineRepository);
            SetupHeadOffice(headOfficeRepository);
            SetupAtm(atmRepository);

            HeadOfficeInstance.Init(headOfficeRepository);
            DomainEvents.Init();
        }

        private static void SetupAtm(IAtmRepository atmRepository)
        {
            var atm = new Atm() {Id = 1};
            atm.LoadMoney(new Money(20,20,20,20,20,20));
            atmRepository.Save(atm);
        }

        private static void SetupHeadOffice(IHeadOfficeRepository headOfficeRepository)
        {
            var headOffice = new HeadOffice {Id = 1};
            headOfficeRepository.Save(headOffice);
        }

        private static void SetupSnackMachine(ISnackMachineRepository snackMachineRepository)
        {
            var snackMachine = new SnackMachine() { Id = 1 };
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 3m));
            snackMachine.LoadSnacks(2, new SnackPile(Snack.Soda, 15, 2m));
            snackMachine.LoadSnacks(3, new SnackPile(Snack.Gum, 20, 1m));

            snackMachine.LoadMoney(new Money(10, 10, 10, 10, 10, 10));
            snackMachineRepository.Save(snackMachine);
        }
    }
}
