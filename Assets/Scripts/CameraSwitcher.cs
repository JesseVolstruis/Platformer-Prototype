using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera myCamera;
    public Vector2 respawnPos;
    CoinManager coinManager;
    public static bool isFollowing = false;
    [SerializeField]
    bool isCamLock = false;
    [SerializeField]
    bool isCamLockOff = false;
    [SerializeField]
    bool isRespawn = true;
    PlayerManager playerManager;
    Vector3 initPos;
    
    // Start is called before the first frame update
    void Start()
    {
        initPos = myCamera.transform.position;
       
    }

    private void OnEnable()
    {
       DeathManager.onDeath += ResetCam;
    }

    private void OnDisable()
    {
        DeathManager.onDeath -=  ResetCam;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing && playerManager != null)
        {
            Vector3 target = new Vector3(playerManager.transform.position.x,
                myCamera.transform.position.y,
                myCamera.transform.position.z);

            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, target, 3*Time.deltaTime);
         
        }
        else if(myCamera.transform.position != initPos && !isFollowing)
        {
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, initPos, 3 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Camera[] cameraArray = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cameraArray)
        {
            c.enabled = false;
        }

        myCamera.enabled = true;
        if (isRespawn)
        {
            SetRespawn();
            coinManager = FindAnyObjectByType<CoinManager>();

            if (coinManager.heldCoin != null)
            {
                coinManager.CollectCoin(coinManager.heldCoin);
            }
        }

        if(isCamLock)
        {
            playerManager = collision.GetComponent<PlayerManager>();
            isFollowing = true;
        }
        if (isCamLockOff)
        {
            playerManager = collision.GetComponent<PlayerManager>();
            isFollowing = false;
            //myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, initPos, 3 * Time.deltaTime);
        }

    }

    private void SetRespawn()
    {
        DeathManager death = FindAnyObjectByType<DeathManager>();
        death.respawnPos = respawnPos;
    }

    void ResetCam(int i)
    {
        isFollowing = false;
        
    }
}
