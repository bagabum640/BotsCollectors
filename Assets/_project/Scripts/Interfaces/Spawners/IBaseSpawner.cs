using System.Collections;
using UnityEngine;

public interface IBaseSpawner
{
    public IEnumerator Create(IUnit unit, Vector3 flagPosition);
}