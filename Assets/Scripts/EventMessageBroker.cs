using UnityEngine;
using System;
using System.Collections.Generic;

public class EventMessageBroker : Singleton<EventMessageBroker>
{
    class EventSubscription
    {
        public List<Delegate> subscribers = new List<Delegate>();
        public bool iterating;
    }

    private readonly Dictionary<Type, EventSubscription> _subscriptions;

    public void ClearSubscribers()
    {
        _subscriptions?.Clear();
    }

    private EventMessageBroker()
    {
        _subscriptions = new Dictionary<Type, EventSubscription>();
    }

    public void Publish<T>(T message)
    {
        if (message == null)
            return;
        var messageType = typeof(T);
        if (!_subscriptions.ContainsKey(messageType))
        {
            return;
        }
        var subscription = _subscriptions[messageType];
        var delegates = subscription.subscribers;
        if (delegates == null || delegates.Count == 0)
        {
            return;
        }
        subscription.iterating = true;
        for (int i = 0; i < delegates.Count; i++)
        {
            var handler = (Action<T>)delegates[i];
            try
            {
                handler?.Invoke(message);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }
        subscription.iterating = false;
    }

    public void Subscribe<T>(Action<T> subscriber)
    {
        var subscription = _subscriptions.ContainsKey(typeof(T)) ? _subscriptions[typeof(T)] : new EventSubscription();
        if (!subscription.subscribers.Contains(subscriber) && !subscription.iterating)
        {
            subscription.subscribers.Add(subscriber);
        }
        _subscriptions[typeof(T)] = subscription;
    }

    public void Unsubscribe<T>(Action<T> subscriber)
    {
        if (!_subscriptions.ContainsKey(typeof(T))) return;
        var subscription = _subscriptions[typeof(T)];
        if (subscription.subscribers.Contains(subscriber) && !subscription.iterating)
            subscription.subscribers.Remove(subscriber);
        if (subscription.subscribers.Count == 0 && !subscription.iterating)
            _subscriptions.Remove(typeof(T));
    }

    public void Dispose()
    {
        _subscriptions?.Clear();
    }
}