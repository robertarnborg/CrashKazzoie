using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private MeshRenderer playerMeshRenderer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CapsuleCollider playerCollider;

    [SerializeField] private GameObject waterDeathFX;
    [SerializeField] private GameObject bloodSplatterFX;

    [SerializeField] private float timeToReloadLevel = 4.0f;

    private Cinemachine.CinemachineFreeLook cinemachineFreeLookCam;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMeshRenderer = GetComponent<MeshRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        cinemachineFreeLookCam = FindObjectOfType<Cinemachine.CinemachineFreeLook>();
    }


    public void GruesomeDeath()
    {
        playerMovement.isDead = true;
        playerMeshRenderer.enabled = false;
        playerCollider.enabled = false;

        cinemachineFreeLookCam.Follow = null;
        cinemachineFreeLookCam.LookAt = null;


        Instantiate(bloodSplatterFX, transform.position, transform.rotation);
        StartCoroutine("RestartLevel", timeToReloadLevel);
    }

    public void WaterDeath()
    {
        playerMovement.isDead = true;
        Instantiate(waterDeathFX, transform.position, Quaternion.Euler(Vector3.up));
        StartCoroutine("RestartLevel", timeToReloadLevel);
    }

    public void TimeOutDeath()
    {

        StartCoroutine("RestartLevel", timeToReloadLevel);
    }


    public IEnumerator RestartLevel(float timeToReloadLevel)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f) // In a while loop while counting down
        {
            normalizedTime += Time.deltaTime / timeToReloadLevel;
            yield return null;
        }
        SaveManager.Instance.RestartLevelLoadCurrentCheckPoint();
    }

}
