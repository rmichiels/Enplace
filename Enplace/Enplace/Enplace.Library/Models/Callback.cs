namespace Enplace.Library.Models
{
    public class Callback<T>
    {
        public string Label { get; set; }
        public string Class { get; set; }
        public Action<T> Action { get; set; }
    }
}
