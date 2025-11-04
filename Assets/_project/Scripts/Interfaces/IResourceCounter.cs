using System;

public interface IResourceCounter
{
    public event Action<int> ResourceChanged;
     public int ResourceAmount { get; set; }
    public void AddScore();
    public void RemoveScore(int value);
}