using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeSpawnController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform maxNorth;
    [SerializeField] private Transform maxEast;
    [SerializeField] private Transform maxSouth;
    [SerializeField] private Transform maxWest;
    [SerializeField] private GameObject currentSpawnPosition;
    [SerializeField] private GameObject lastSpawnPosition;
    [SerializeField] private GatheringNodeSO[] possibleNodes;
    private GatheringNodeSO lastNodeSpawned = null;
    private float maxNodes = 5;
    public void Initialize()
    {
        Debug.Log("Initial Location");
        currentSpawnPosition.transform.position =
            new Vector2(Random.Range(maxWest.position.x, maxEast.position.x),
            Random.Range(maxNorth.position.y, maxSouth.position.y));
        lastSpawnPosition.transform.position = currentSpawnPosition.transform.position;
        for (int i = 0; i < maxNodes; i++)
            SpawnNewNode();
    }

    private void SpawnNewNode()
    {
        Debug.Log("Spawning Node");
        GameObject newNode = Instantiate(GameAssets.i.pfGatheringNode, currentSpawnPosition.transform.position, Quaternion.identity);
        newNode.transform.SetParent(null);
        GatheringNodeSO nodeToSpawn = possibleNodes[Random.Range(0, possibleNodes.Length)];
        if (nodeToSpawn == lastNodeSpawned)
        {
            Debug.Log("Another New Node");
            //SpawnNewNode();
        }

        lastNodeSpawned = nodeToSpawn;
        newNode.GetComponent<IGatherable>().Initialize(nodeToSpawn);
        PickNewSpawnLocation();
    }

    private void PickNewSpawnLocation()
    {
        Debug.Log("New Location");
        currentSpawnPosition.transform.position =
            new Vector2(Random.Range(maxWest.position.x, maxEast.position.x),
            Random.Range(maxNorth.position.y, maxSouth.position.y));

        if (Vector2.Distance(currentSpawnPosition.transform.position, lastSpawnPosition.transform.position) < 5f)
            PickNewSpawnLocation();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentSpawnPosition.transform.position, .5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<TilemapCollider2D>() != null)
                PickNewSpawnLocation();
            break;
        }

        lastSpawnPosition.transform.position = currentSpawnPosition.transform.position;
    }
    #endregion
    #region Get Functions
    public Transform GetMaxNorth() { return maxNorth; }
    public Transform GetMaxEast() { return maxEast; }
    public Transform GetMaxSouth() { return maxSouth; }
    public Transform GetMaxWest() { return maxWest; }
    #endregion
}
