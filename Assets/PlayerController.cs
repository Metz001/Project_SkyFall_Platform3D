using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public int hearthCount;
    //public Rigidbody body;
    private CharacterController character;

    private Vector3 moveDirecction;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
       // body = GetComponent<Rigidbody>();
       character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y, Input.GetAxis("Vertical") * speed);
        if (Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
        }*/

        moveDirecction = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirecction.y, Input.GetAxis("Vertical") * speed);
        character.Move(moveDirecction*Time.deltaTime);


        if (Input.GetButtonDown("Jump"))
        {
            moveDirecction.y = jumpForce;
        }
        
        moveDirecction.y = moveDirecction.y + (Physics.gravity.y*gravityScale);
        character.Move(moveDirecction * Time.deltaTime);    
       

    }

}
