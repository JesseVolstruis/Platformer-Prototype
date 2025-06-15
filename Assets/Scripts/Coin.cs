using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public string coinTitle;
    [SerializeField]
    private CoinManager coinManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinManager.heldCoin = this;
            if (gameObject.transform.localScale != Vector3.zero)
            {
                SFXManager.instance.PlayClip(coinManager.coinSound, transform, 0.1f);
            }
            gameObject.transform.localScale = Vector3.zero;

        }
    }
}
