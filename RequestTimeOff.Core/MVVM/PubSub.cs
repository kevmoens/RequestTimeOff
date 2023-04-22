using System;
using System.Collections.Concurrent;

namespace RequestTimeOff.MVVM
{
    public abstract class PubSub<T>
    {
        private readonly ConcurrentDictionary<PubSubToken, Action<T>> _subscribers = new ConcurrentDictionary<PubSubToken, Action<T>>();
        public PubSubToken Subscribe (Action<T> action)
        {
            var token = new PubSubToken();
            _subscribers.TryAdd(token, action);
            return token;
        }
        public void Unsubscribe(PubSubToken token)
        {
            Action<T> action;
            _subscribers.TryRemove(token, out action);
        }
        public void Publish(T message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Value(message);
            }
        }
    }
    public class PubSubToken
    {
    }
}
