using UnityEngine;

public interface IGatherable
{
    public void Initialize(GatheringNodeSO _nodeSO);
    public void Gather();
    public bool GetAbleToGather();
}
