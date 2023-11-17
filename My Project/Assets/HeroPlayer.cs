using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlayer : MonoBehaviour
{
    private CharacterController characterController;

     public float characterSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 jump = new Vector3(Input.GetAxis("Jump"));
        characterController.jump (jump * Time.deltaTime * characterSpeed);
    }
}
