namespace Enplace.Library.Context
{
    public class ReactiveProperty<T>
    {
        //public delegate Task AsyncEventHandler(T? message);
        public delegate void AsyncEventHandler(T? input);
        public event AsyncEventHandler _trigger;
        private T _value;

        public void Bind(params AsyncEventHandler[] args)
        {
            foreach (var handler in args)
            {
                _trigger += handler;
            }
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _trigger?.Invoke(value);
            }
        }

        public ReactiveProperty(T value)
        {
            Value = value;
        }
    }
}
