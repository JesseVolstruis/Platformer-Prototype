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
    private Animator playerAnimation;
    private GameObject spriteParent;
    private GameObject spritesParent;
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
    public static event Action <Vector3,Vector3> onBounce;
    // Start is called before the first frame update
    void Start()
    {
        spriteParent = GameObject.Find("Player Sprite");
        rb = player.GetComponent<Rigidbody2D>();
        sprite = spriteParent.GetComponent<SpriteRenderer>();
        spritesParent = GameObject.Find("Sprites");
        playerAnimation = spritesParent.GetComponent<Animator>();
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
            playerAnimation.SetBool("Grounded", true);
            SFXManager.instance.PlaySingleClip(playerManager.bounceSound, playerManager.transform, 0.2f);
            Bounce();
        }

        if(isWallBounced() || Physics2D.OverlapCircle(backCheck.position, 0.2f, bounceWallLayer))
        {
            playerManager.coyoteTimeCounter = playerManager.coyoteTime;
            playerManager.canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            playerManager.hasJump = true;
            playerManager.isWallBoucning = true;
            SFXManager.instance.PlaySingleClip(playerManager.bounceSound, playerManager.transform, 0.2f);
            WallBounce();
        }

        if (isWallBouncedLeft() || Physics2D.OverlapCircle(backCheck.position, 0.2f, bounceWallLayerLeft))
        {
            playerManager.coyoteTimeCounter = playerManager.coyoteTime;
            playerManager.canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            playerManager.hasJump = true;
            playerManager.isWallBoucning = true;
            SFXManager.instance.PlaySingleClip(playerManager.bounceSound, playerManager.transform, 0.2f);
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
        onBounce(bounceCheck.transform.position,transform.eulerAngles);
    }

    private void WallBounce()
    {
        playerManager.canDash = true;
        rb.velocity = new Vector2(Vector2.right.x * 15f, 5);
        onBounce(bounceWallCheck.transform.position, transform.eulerAngles);
        Invoke(nameof(StopWallBouncing), 0.35f);
    }

    private void WallBounceLeft()
    {
        playerManager.canDash = true;
        rb.velocity = new Vector2(Vector2.left.x * 15f, 5);
        onBounce(bounceWallCheck.transform.position, transform.eulerAngles);
        Invoke(nameof(StopWallBouncing), 0.35f);
    }

    private void StopWallBouncing()
    {
        playerManager.isWallBoucning=false;
    }
}
