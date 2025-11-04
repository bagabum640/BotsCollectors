using UnityEngine;

public class Flag : MonoBehaviour
{
    public void Deactive() =>
    gameObject.SetActive(false);

    public void Active() =>
    gameObject.SetActive(true);
}