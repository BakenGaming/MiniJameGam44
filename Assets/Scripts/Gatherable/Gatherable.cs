using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gatherable : MonoBehaviour, ICollectable
{
    public static event Action<GatherableSO> OnItemGathered;
    private Vector3 targetPosition;
    private bool playerFound = false, timerTrigger = false, isMoving = true, hasStoppedMoving = false, readyToGather = false;
    private Rigidbody2D itemRB;
    private GatherableSO gatherable;
    private float gatherAttractTimer = .75f, actualAttractSpeed = 5f, stopTimer = .1f;
    public void Collect()
    {
        OnItemGathered?.Invoke(gatherable);
        Destroy(gameObject);
    }

    public void Initialize(GatherableSO _gatherable)
    {
        gatherable = _gatherable;
        itemRB = GetComponent<Rigidbody2D>();
        GetComponentInChildren<SpriteRenderer>().sprite = gatherable.gatherableSprite;
        playerFound = false;
        timerTrigger = false;
    }

    public void SetTarget(Vector3 position)
    {
        if (!readyToGather) return;
        targetPosition = position;
        playerFound = true;
        timerTrigger = true;
    }

    private void Update()
    {
        if (timerTrigger && gatherAttractTimer >= 0) {gatherAttractTimer -= Time.deltaTime; }
        if (isMoving) stopTimer -= Time.deltaTime;

        if (!hasStoppedMoving && stopTimer <= 0)
        {
            isMoving = false;
            readyToGather = true;
            hasStoppedMoving = true;
            itemRB.linearVelocity = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if (playerFound && gatherAttractTimer <= 0)
        {
            Vector3 targetDirection = (targetPosition - transform.position).normalized;
            itemRB.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * actualAttractSpeed;
        }
    }
}
