using UnityEngine;
using UnityEngine.AI;

public interface IMover
{
    public NavMeshAgent Agent { get; }
    public void Warp(Vector3 position);
    public void Stop();
    public void MoveTo(Vector3 target);
}