using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnPlayerSpawned += SetupCamera;
    }

    private void SetupCamera()
    {
        Camera.main.gameObject.transform.SetParent(GameManager.i.GetPlayerGO().transform);
    }
}
