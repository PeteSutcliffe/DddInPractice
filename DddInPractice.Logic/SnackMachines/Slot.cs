using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.SnackMachines
{
    public class Slot : Entity
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual int Position { get; private set; }

        protected Slot()
        {
        }

        public Slot(int position)
            : this()
        {
            Position = position;
            SnackPile = SnackPile.Empty;
        }
    }
}
