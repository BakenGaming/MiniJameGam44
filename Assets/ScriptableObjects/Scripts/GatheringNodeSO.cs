using UnityEngine;

[CreateAssetMenu(menuName = "Gathering Node")]
public class GatheringNodeSO : ScriptableObject
{
    public int numberOfAttempts;
    public Sprite nodeSprite;
    public GatherableSO collectableItem;
    public GameObject rareItem;
    public float rareChance;
}
