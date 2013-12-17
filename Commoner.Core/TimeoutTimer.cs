using System.Timers;

namespace Commoner.Core
{
    public class TimeoutTimer<T> : Timer
    {
        public T PassAlongValue { get; set; }

        public TimeoutTimer(double timeoutInMillisecs, T passAlongValue)
            : base(timeoutInMillisecs)
        {
            PassAlongValue = passAlongValue;
        }
    }
}
