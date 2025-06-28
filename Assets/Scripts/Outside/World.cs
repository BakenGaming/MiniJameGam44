using UnityEngine;

public class World : MonoBehaviour, IWorldHandler
{
    [SerializeField] private NodeSpawnController nodeSpawnController;

    public NodeSpawnController GetNodeSpawner()
    {
        return nodeSpawnController;
    }
}
