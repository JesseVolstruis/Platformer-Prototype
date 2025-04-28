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
    private float bounceForce;
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
            WallBounce();
        }
       
    }

    private bool isBounced()
    {
        return Physics2D.OverlapCircle(bounceCheck.position, 0.2f, bounceLayer);
    }

    private bool isWallBounced()
    {
        return Physics2D.OverlapCircle(bounceWallCheck.position, 0.2f, bounceLayer);
    }

    private void Bounce()
    {
       rb.velocity = new Vector2(0, bounceForce);
    }

    private void WallBounce()
    {
        rb.velocity = new Vector2(Vector2.right.x*10f, 0);
    }
}
