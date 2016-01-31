namespace DddInPractice.Logic.Management
{
    public static class HeadOfficeInstance
    {
        private const long HeadOfficeId = 1;

        public static HeadOffice Instance { get; private set; }

        public static void Init(IHeadOfficeRepository repository)
        {
            Instance = repository.GetById(HeadOfficeId);
        }
    }
}
