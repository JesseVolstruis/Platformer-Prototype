using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    private Transform backCheck;
    [SerializeField]
    private Transform bounceWallCheck;
    [SerializeField]
    private LayerMask bounceLayer;
    [SerializeField]
    private LayerMask bounceWallLayer;
    [SerializeField]
    private LayerMask bounceWallLayerLeft;
    [SerializeField]
    private float bounceForce;
    public static event Action <Vector3> onBounce;
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

        if(isWallBounced() || Physics2D.OverlapCircle(backCheck.position, 0.2f, bounceWallLayer))
        {
            playerManager.coyoteTimeCounter = playerManager.coyoteTime;
            playerManager.canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            playerManager.hasJump = true;
            playerManager.isWallBoucning = true;
            WallBounce();
        }

        if (isWallBouncedLeft() || Physics2D.OverlapCircle(backCheck.position, 0.2f, bounceWallLayerLeft))
        {
            playerManager.coyoteTimeCounter = playerManager.coyoteTime;
            playerManager.canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            playerManager.hasJump = true;
            playerManager.isWallBoucning = true;
            WallBounceLeft();
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

    private bool isWallBouncedLeft()
    {
        return Physics2D.OverlapCircle(bounceWallCheck.position, 0.2f, bounceWallLayerLeft);
    }


    private void Bounce()
    {
        rb.velocity = new Vector2(0, bounceForce);
        onBounce(bounceCheck.transform.position);
    }

    private void WallBounce()
    {
        playerManager.canDash = true;
            rb.velocity = new Vector2(Vector2.right.x * 15f, 5);
        Invoke(nameof(StopWallBouncing), 0.35f);
    }

    private void WallBounceLeft()
    {
        playerManager.canDash = true;
        rb.velocity = new Vector2(Vector2.left.x * 15f, 5);
        Invoke(nameof(StopWallBouncing), 0.35f);
    }

    private void StopWallBouncing()
    {
        playerManager.isWallBoucning=false;
    }
}
