using System;

public interface IDamageable<T>
{
    void Damage(T amount);
}