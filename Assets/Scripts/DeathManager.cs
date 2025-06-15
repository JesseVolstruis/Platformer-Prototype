using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField]
    private Transform lavaCheck;
    [SerializeField]
    private Transform lavaWallCheck;
    [SerializeField] 
    private Transform lavaRoofCheck;
    [SerializeField]
    private Transform lavaBackCheck;
    [SerializeField]
    private LayerMask lavaLayer;
    [SerializeField]
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private SpriteRenderer outline;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private PlayerManager playerManager;
    private static int deathCount = 0;
    public static event Action<int> onDeath;
    public Vector2 respawnPos = new Vector2(-64, -4);
    [SerializeField]
    private CoinManager coinManager;
    [SerializeField]
    private AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        //sprite =player.GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }

    private void Respawn()
    {
        if (Physics2D.OverlapCircle(lavaCheck.position, 0.2f, lavaLayer) 
            || Physics2D.OverlapCircle(lavaWallCheck.position, 0.2f, lavaLayer) 
            || Physics2D.OverlapCircle(lavaRoofCheck.position, 0.1f, lavaLayer)
            || Physics2D.OverlapCircle(lavaBackCheck.position, 0.2f, lavaLayer))
        {
            StartCoroutine(Death(respawnPos)); 
        }
       
    }

    private IEnumerator Death(Vector2 respawnPos)
    {
        sprite.enabled = false;
        outline.enabled = false;
        deathCount++;
        onDeath?.Invoke(deathCount);
        SFXManager.instance.PlayClip(deathSound, playerManager.transform, 0.3f);
        Vector2 currentPosition = rb.transform.position;
        particles.transform.position = currentPosition;
        particles.Emit(10);
        rb.velocity = Vector2.zero;
        player.transform.position = respawnPos;
        yield return new WaitForSeconds(0.4f);
        rb.velocity = Vector2.zero;
        sprite.enabled = true;
        outline.enabled = true;
        coinManager.heldCoin = null;
        coinManager.RespawnCoins();

    }

}
