using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    private CinemachineCamera playerCam;
    public void SetupCamera()
    {
        playerCam = GetComponent<CinemachineCamera>();
        playerCam.Follow = GameManager.i.GetPlayerGO().transform;
    }
}
