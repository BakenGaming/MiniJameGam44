using UnityEngine;

public enum GatherableType
{ milk, salt, sugar, butter, cream, icecream}
public class GatherableSO : ScriptableObject
{
    [SerializeField] public GatherableType gatherableType;
    [SerializeField] public GameObject gatherableObject;
    [SerializeField] public Sprite gatherableSprite;
}
