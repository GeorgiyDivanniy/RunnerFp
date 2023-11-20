using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlayer : MonoBehaviour
{
    private CharacterController characterController;

     public float characterSpeed = 5f;
     public float horizontal, vertical;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * characterSpeed*Time.deltaTime;
        transform.Translate(horizontal, 0, 0);
        vertical = Input.GetAxis("Vertical") * characterSpeed * Time.deltaTime;
        transform.Translate(0,0,vertical);
    }
}
