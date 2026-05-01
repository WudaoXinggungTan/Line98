using System;

public interface IBroadcastService
{
    public void Add<T>(Action<T> receiver)
    {
    }

    public void Broadcast(object args)
    {
    }

    public void Remove<T>(Action<T> receiver)
    {
    }
}
