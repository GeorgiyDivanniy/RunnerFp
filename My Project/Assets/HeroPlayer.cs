using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody))]

public class HeroPlayer : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;
    Rigidbody r;
    bool grounded;
    Vector3 defaultScale;
    bool crouch;
    public int collectedCoins;
    bool isImmortal;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        r.freezeRotation = true;
        r.useGravity = false;
        defaultScale = transform.localScale;
        collectedCoins = 0;
        isImmortal = false;
        crouch = false;
        grounded = false;
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            r.velocity = new Vector3(r.velocity.x, CalculateJumpVerticalSpeed(), r.velocity.z);
        }

        //Crouch
        crouch = Input.GetKey(KeyCode.S);
        if (crouch)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(defaultScale.x, defaultScale.y * 0.4f, defaultScale.z), Time.deltaTime * 7);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, Time.deltaTime * 7);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            collectedCoins -= 5;
            isImmortal = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // We apply gravity manually for more tuning control
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            if(isImmortal == true)
            {
                isImmortal = false;
                return;
            }

            else
            {
                //Конец игры
                levelGenerator.instance.gameOver = true;
            }
        }

        if (collision.gameObject.tag == "Coin") 
        {
            collectedCoins++;
            collision.gameObject.SetActive(false);
            collision.gameObject.IsDestroyed();
        }

        if (collision.gameObject.tag == "Booster")
        {
            isImmortal = true;
            collision.gameObject.SetActive(false);
            collision.gameObject.IsDestroyed();
        }
    }
    private void OnGUI()
    {
        GUI.color = Color.green;
        GUI.Label(new Rect(770, 5, 200, 25), "Coins: " + ((int)collectedCoins));

        if(collectedCoins >= 5)
        {
            GUI.Label(new Rect(5, 400, 200, 25), "Press E to buy booster for 5 coins");
        }

        else
        {
            GUI.Label(new Rect(5, 400, 200, 25)," ");
        }
    }
}
