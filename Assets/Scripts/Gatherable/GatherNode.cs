using UnityEngine;
using UnityEngine.EventSystems;

public class GatherNode : MonoBehaviour, IGatherable, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer nodeSR;
    [SerializeField] private Transform spawnPoint;
    private float rareCalculator, rareChance;
    private GameObject rareItemToSpawn;
    private GatherableSO itemToSpawn;
    private IGatherHandler _activeHandler;
    private IInputHandler _playerHandler;
    private bool _ableToGather = false;
    private int remainingGatherAttempts;
    public void Initialize(GatheringNodeSO _nodeSO)
    {
        nodeSR.sprite = _nodeSO.nodeSprite;
        itemToSpawn = _nodeSO.collectableItem;
        rareItemToSpawn = _nodeSO.rareItem;
        rareChance = _nodeSO.rareChance;
        remainingGatherAttempts = _nodeSO.numberOfAttempts;
        Debug.Log("Gather Node Initialize");
    }
    public void Gather()
    {
        Debug.Log("Gathering");
        GameObject newGatherable = Instantiate(itemToSpawn.gatherableObject, spawnPoint);
        newGatherable.GetComponent<Gatherable>().Initialize(itemToSpawn);
        newGatherable.transform.parent = null;

        float dropForce = 75f;
        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        newGatherable.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

        remainingGatherAttempts--;
        if (remainingGatherAttempts <= 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IGatherHandler>() == null) return;
        Debug.Log("Player Can Gather");
        _playerHandler = other.GetComponent<IInputHandler>();
        _playerHandler.SetGatherableTarget(this);
        _activeHandler = other.GetComponent<IGatherHandler>();
        _activeHandler.SetCanGather(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IGatherHandler>() == null) return;
        Debug.Log("Player Cannot Gather");
        _playerHandler.RemoveGatherableTarget();
        _activeHandler.SetCanGather(false);
        _activeHandler = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _ableToGather = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _ableToGather = false;
    }

    public bool GetAbleToGather() { return _ableToGather; }
}
