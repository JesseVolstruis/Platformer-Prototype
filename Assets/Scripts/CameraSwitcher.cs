using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera myCamera;
    public Vector2 respawnPos;
    CoinManager coinManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Camera[] cameraArray = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cameraArray)
        {
            c.enabled = false;
        }

        myCamera.enabled = true;
        SetRespawn();
        coinManager = FindAnyObjectByType<CoinManager>();
        if(coinManager.heldCoin != null)
        {
            coinManager.CollectCoin(coinManager.heldCoin);
        }
        
    }

    private void SetRespawn()
    {
        DeathManager death = FindAnyObjectByType<DeathManager>();
        death.respawnPos = respawnPos;
    }
}
