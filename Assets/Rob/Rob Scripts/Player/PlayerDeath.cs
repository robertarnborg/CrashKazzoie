using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [SerializeField] private GameObject waterDeathFX;
    [SerializeField] private GameObject bloodSplatterFX;
    [SerializeField] private SimplerSFX gruesomeDeathSFX;
    
    private GameObject playerGFX;
    private PlayerMovement playerMovement;
    private CapsuleCollider playerCollider;
    private Rigidbody playerBody;

    [SerializeField] private float timeToReloadLevel = 4.0f;

    private Cinemachine.CinemachineFreeLook cinemachineFreeLookCam;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerGFX = transform.Find("Graphics").gameObject;
        playerCollider = GetComponent<CapsuleCollider>();
        playerBody = GetComponent<Rigidbody>();

        cinemachineFreeLookCam = FindObjectOfType<Cinemachine.CinemachineFreeLook>();
    }


    public void GruesomeDeath()
    {
        playerBody.isKinematic = true;
        playerMovement.isDead = true;
        playerGFX.SetActive(false);
        playerCollider.enabled = false;

        cinemachineFreeLookCam.Follow = null;
        cinemachineFreeLookCam.LookAt = null;

        gruesomeDeathSFX.PlayRandomSfx();
        Instantiate(bloodSplatterFX, transform.position, transform.rotation);
        StartCoroutine("RestartLevel", timeToReloadLevel);

        MusicManager.Instance.PlayGameOverMusic();
    }

    public void WaterDeath()
    {
        playerMovement.isDead = true;
        Instantiate(waterDeathFX, transform.position, Quaternion.Euler(-75,0,0));
        StartCoroutine("RestartLevel", timeToReloadLevel);

        MusicManager.Instance.PlayGameOverMusic();
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
