using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField]
    private Transform bounceCheck;
    [SerializeField]
    private Transform bounceWallCheck;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
