using System;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public event Action<int> ResourceChanged;

    public int ResourceAmount { get; private set; } = 0;

    public void AddScore()
    {
        ResourceAmount++;
        ResourceChanged?.Invoke(ResourceAmount);
    }

    public void RemoveScore(int value)
    {
        ResourceAmount -= value;
        ResourceChanged?.Invoke(ResourceAmount);
    }
}