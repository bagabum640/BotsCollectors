using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Builder : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    private Unit _unit;

    [field: SerializeField] public bool IsCreating { get; private set; }

    private void Awake() =>    
        _unit = GetComponent<Unit>();    

    public void OnCreate() =>   
        IsCreating = true;   

    public void SetBaseSpawner(BaseSpawner baseSpawner) =>
        _baseSpawner = baseSpawner;

    public void CompleteCreate(Base @base) =>
        @base.DeactiveFlag();

    public void Create(Vector3 flag)
    {
        if (IsCreating && flag != null)
        {
            StartCoroutine(_baseSpawner.Spawn(_unit, flag));

            IsCreating = false;
        }
    }
}