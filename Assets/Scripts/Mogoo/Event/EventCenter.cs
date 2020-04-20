using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Mogoo.Event
{
    public interface IEvent
    {

    }

    public class EventData<T> : IEvent
    {
        public UnityAction<T> Action;

        public EventData(UnityAction<T> action)
        {
            Action += action;
        }
    }

    public class EventData : IEvent
    {
        public UnityAction Action;

        public EventData(UnityAction action)
        {
            Action += action;
        }
    }

    public class EventCenter : Singleton<EventCenter>
    {
        private readonly Dictionary<string, IEvent> _events = new Dictionary<string, IEvent>();

        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            if (_events.ContainsKey(name))
            {
                (_events[name] as EventData<T>).Action += action;
            }
            else
            {
                _events.Add(name, new EventData<T>(action));
            }
        }

        public void AddEventListener(string name, UnityAction action)
        {
            if (_events.ContainsKey(name))
            {
                (_events[name] as EventData).Action += action;
            }
            else
            {
                _events.Add(name, new EventData(action));
            }
        }

        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (_events.ContainsKey(name))
            {
                (_events[name] as EventData<T>).Action -= action;
            }
        }

        public void RemoveEventListener(string name, UnityAction action)
        {
            if (_events.ContainsKey(name))
            {
                (_events[name] as EventData).Action -= action;
            }
        }

        public void DispatchEvent<T>(string name, T t)
        {
            if (_events.ContainsKey(name))
            {
                (_events[name] as EventData<T>).Action.Invoke(t);
            }
        }

        public void DispatchEvent(string name)
        {
            if (_events.ContainsKey(name))
            {
                (_events[name] as EventData).Action.Invoke();
            }
        }

        public void Clear()
        {
            _events.Clear();
        }
    }
}
