namespace CansolveANK.Controllers
{
    public class CalculateTimeIntrval
    {
        public long CalculateTimeIntervalInMs(TimeUnit unit, int rate)
        {
            switch (unit)
            {
                case TimeUnit.minute:
                    return rate * 60 * 1000;
                case TimeUnit.hour:
                    return rate * 60 * 60 * 1000;
                case TimeUnit.week:
                    return rate * 24 * 60 * 60 * 1000;
                case TimeUnit.day:
                    return rate * 7 * 24 * 60 * 60 * 1000;
                case TimeUnit.month:
                    
                    return rate * 30 * 24 * 60 * 60 * 1000;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
            }
        }
    }
}
