using Enplace.Service.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static AsyncEventManager<MenuDTO> MenuSelection { get; set; } = new();

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
