using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] switches;
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private AudioClip switchSound;
    void Start()
    {
        switches = GameObject.FindGameObjectsWithTag("SwitchBlock");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumped())
        {
            Invoke(nameof(ToggleSwitches), 0.01f);

        }

    }

     bool isJumped()
    {
        return (playerManager.IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            || (playerManager.IsWalled() && Input.GetKeyDown(KeyCode.Space))
            || (Input.GetKeyDown(KeyCode.Space) && playerManager.coyoteTimeCounter > 0);



    }

    void ToggleSwitches()
    {
        foreach (GameObject s in switches)
        {
            SpriteRenderer sprite = s.GetComponent<SpriteRenderer>();
            Collider2D col = sprite.GetComponent<Collider2D>();
            if(sprite.color.a == 1)
            {
                Color tmpColor = sprite.color;
                tmpColor.a = 0.0588f;
                sprite.color = tmpColor;
                col.enabled = false;
            }
            else
            {
                Color tmpColor = sprite.color;
                tmpColor.a = 1;
                sprite.color = tmpColor;
                col.enabled = true; 
            }
        }
    
    }

}
