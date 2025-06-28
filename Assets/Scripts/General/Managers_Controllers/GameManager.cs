using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Events
    public static event Action OnPlayerSpawned;
    #endregion
    #region Variables
    private static GameManager _i;
    public static GameManager i { get { return _i; } }
    [SerializeField] private Transform sysMessagePoint;
    [SerializeField] private Transform spawnPoint;

    private StaticVariables _s;
    private GameObject playerGO;
    private bool isPaused;


    #endregion

    #region Initialize
    private void Awake()
    {
        _i = this;
        SetupObjectPools();
        _s = GetComponent<StaticVariables>();
        Initialize();
    }

    private void Initialize()
    {
        SpawnWorld();
    }

    private void SpawnWorld()
    {
        GameObject newWorld = Instantiate(GameAssets.i.possibleWorlds[UnityEngine.Random.Range(0, GameAssets.i.possibleWorlds.Length)],
            Vector2.zero, Quaternion.identity);
        NodeSpawnController _n = newWorld.GetComponent<IWorldHandler>().GetNodeSpawner();
        _n.Initialize();
        SpawnPlayerObject();
    }

    private void SpawnPlayerObject()
    {
        playerGO = Instantiate(GameAssets.i.pfPlayerObject, spawnPoint);
        playerGO.transform.parent = null;
        playerGO.GetComponent<IHandler>().Initialize();
        OnPlayerSpawned?.Invoke();
    }

    public void SetupObjectPools()
    {
        //Do the below for all objects that will need pooled for use
        //ObjectPooler.SetupPool(OBJECT, SIZE, "NAME") == Object is pulled from GameAssets, Setup object with a SO that contains size and name

        //The below is placed in location where object is needed from pool
        //==============================
        //PREFAB_SCRIPT instance = ObjectPooler.DequeueObject<PREFAB_SCRIPT>("NAME");
        //instance.gameobject.SetActive(true);
        //instance.Initialize();
        //==============================
    }
    #endregion

    public void PauseGame() { if (isPaused) return; else isPaused = true; }
    public void UnPauseGame() { if (isPaused) isPaused = false; else return; }

    public Transform GetSysMessagePoint() { return sysMessagePoint; }
    public GameObject GetPlayerGO() { return playerGO; }
    public bool GetIsPaused() { return isPaused; }
    

}
