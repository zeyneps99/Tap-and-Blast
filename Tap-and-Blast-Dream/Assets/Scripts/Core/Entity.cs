using UnityEngine;

[RequireComponent(typeof(Commander))]
public class Entity : MonoBehaviour, IEntity
{
    protected Commander _commander;

    private void Awake()
    {
        _commander = GetComponent<Commander>();
    }

}
