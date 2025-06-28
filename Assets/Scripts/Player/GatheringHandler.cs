using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GatheringHandler : MonoBehaviour, IGatherHandler
{
    private float gatheringTimer, gatheringTimeDelay = .4f;
    private bool canGather = false;
    public void Initialize()
    {
        gatheringTimer = 0;
        PlayerInputController_TopDown.OnPlayerGatherAttempt += GatherAttempt;
    }

    private void GatherAttempt(IGatherable _target)
    {
        if (gatheringTimer <= 0 && canGather)
        {
            if (_target != null) { _target.Gather(); gatheringTimer = gatheringTimeDelay; }
        }
        else Debug.Log("Can't Gather Now");
    }

    void Update()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        gatheringTimer -= Time.deltaTime;
    }
    public void SetCanGather(bool _value) { canGather = _value; } 
}
