using Enplace.Service.DTO;

namespace Enplace.Service.Services.Managers
{
    public static class EventManager
    {
        public delegate Task AsyncEventHandler();
        public static event AsyncEventHandler OnSubmit;
        public static void TriggerOnSubmit()
        {
            OnSubmit?.Invoke();
        }
        public static AsyncEventManager IngredientRequested { get; set; } = new();
        public static AsyncEventManager<MenuDTO> MenuSelection { get; set; } = new();
        public static AsyncEventManager<MenuDTO> MenuCreated { get; set; } = new();
        public static event Action<string, NotificationType> OnNotification;
    }

    public class AsyncEventManager
    {
        public delegate Task AsyncEventHandler();
        public event AsyncEventHandler OnEventTriggered;
        public void TriggerEvent()
        {
            OnEventTriggered?.Invoke();
        }
    }

    public class AsyncEventManager<TMessage>
    {
        public delegate Task AsyncEventHandler(TMessage message);
        public event AsyncEventHandler OnEventTriggered;
        public void TriggerEvent(TMessage message)
        {
            OnEventTriggered?.Invoke(message);
        }
    }
}
