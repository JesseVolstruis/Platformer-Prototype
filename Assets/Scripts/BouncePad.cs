using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField]    
    private PlayerManager playerManager;
    [SerializeField]
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField]
    private Transform bounceCheck;
    [SerializeField]
    private Transform bounceWallCheck;
    [SerializeField]
    private LayerMask bounceLayer;
    [SerializeField]
    private LayerMask bounceWallLayer;
    [SerializeField]
    private float bounceForce;
    [SerializeField]
    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        sprite = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBounced())
        {
            playerManager.coyoteTimeCounter = playerManager.coyoteTime;
            playerManager.canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            playerManager.hasJump = true;
            Bounce();
        }

        if(isWallBounced())
        {
            playerManager.coyoteTimeCounter = playerManager.coyoteTime;
            playerManager.canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            playerManager.hasJump = true;
            playerManager.isWallBoucning = true;
            WallBounce();
        }
       
    }

    private bool isBounced()
    {
        return Physics2D.OverlapCircle(bounceCheck.position, 0.2f, bounceLayer);
    }

    private bool isWallBounced()
    {
        return Physics2D.OverlapCircle(bounceWallCheck.position, 0.2f, bounceWallLayer);
    }

    private void Bounce()
    {
       rb.velocity = new Vector2(0, bounceForce);
    }

    private void WallBounce()
    {
        playerManager.canDash = true;
        rb.velocity = new Vector2(direction*15f, 5);
        Invoke(nameof(StopWallBouncing), 0.35f);
    }

    private void StopWallBouncing()
    {
        playerManager.isWallBoucning=false;
    }
}
