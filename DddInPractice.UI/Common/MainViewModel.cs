﻿using DddInPractice.Mongo;
using DddInPractice.UI.Management;

namespace DddInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public DashboardViewModel Dashboard { get; private set; }

        public MainViewModel()
        {
            Dashboard = new DashboardViewModel(new SnackMachineRepository(), new AtmRepository(), new HeadOfficeRepository());
        }
    }
}
