using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        BouncePad.onBounce += Teleport;
    }

    private void OnDisable()
    {
        BouncePad.onBounce -= Teleport;
    }

    void Teleport(Vector3 pos, Vector3 angle)
    {
        transform.position = pos;
        transform.eulerAngles = angle;
        particles.Emit(1);
    }
}
