using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] switches;
    [SerializeField]
    private PlayerManager playerManager;
    void Start()
    {
        switches = GameObject.FindGameObjectsWithTag("SwitchBlock");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumped())
        {
            ToggleSwitches();
        }
        
    }

    bool isJumped()
    {
        return (playerManager.hasJump && Input.GetKeyDown(KeyCode.Space));
    }

    void ToggleSwitches()
    {
        foreach (GameObject s in switches)
        {
            SpriteRenderer sprite = s.GetComponent<SpriteRenderer>();
            if(sprite.color.a == 1)
            {
                Color tmpColor = sprite.color;
                tmpColor.a = 0.0588f;
                sprite.color = tmpColor;
            }
            else
            {
                Color tmpColor = sprite.color;
                tmpColor.a = 1;
                sprite.color = tmpColor;
            }
        }
        Debug.Log("And jump");
    }
}
