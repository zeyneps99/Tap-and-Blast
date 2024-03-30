using UnityEngine;

[RequireComponent(typeof(Commander))]
public class Entity : MonoBehaviour
{
    protected Commander _commander;

    private void Awake()
    {
        _commander = GetComponent<Commander>();
    }

}
