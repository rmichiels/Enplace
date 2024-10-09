namespace Enplace.Service.Services.Managers.Cache
{
    public class CachedItem<T>
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public T Item { get; set; }

        public bool IsValid(double allowedLifetimeMinutes) => TimeStamp.AddMinutes(allowedLifetimeMinutes) > DateTime.Now;
    }
}
