using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Coin heldCoin;
    public List<Coin> unCollectedCoins;
    public string levelTitle;
    [SerializeField]
    public AudioClip coinSound;
    void Start()
    {
        ColorCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnCoins()
    {
        foreach (var coin in unCollectedCoins)
        {
            coin.gameObject.transform.localScale = new Vector3(0.5f,0.5f,1);
        }
    }

    public void CollectCoin(Coin coin)
    {
        if(PlayerPrefs.GetInt(coin.coinTitle) == 0)
        {
            PlayerPrefs.SetInt(levelTitle, PlayerPrefs.GetInt(levelTitle) + 1);
            PlayerPrefs.SetInt(coin.coinTitle, 1);
        }
        unCollectedCoins.Remove(coin);
        heldCoin = null;
    }

    public void ColorCoins()
    {
        foreach(var coin in unCollectedCoins)
        {
            if(PlayerPrefs.GetInt(coin.coinTitle) == 1){
                coin.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }

}
