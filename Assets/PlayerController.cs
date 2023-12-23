using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool jumping;

    public int health;
    

    private CharacterController character;

    private Vector3 moveDirecction;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
       
       character = GetComponent<CharacterController>();
       transform.position = GameManager.instance.lastCheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {     
        moveDirecction = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirecction.y, Input.GetAxis("Vertical") * speed);
        character.Move(moveDirecction*Time.deltaTime);
    
        if (Input.GetButtonDown("Jump") && character.isGrounded)
        {
            jumping = true;
            moveDirecction.y = jumpForce;
        }
        
        moveDirecction.y = moveDirecction.y + (Physics.gravity.y*gravityScale);
        character.Move(moveDirecction * Time.deltaTime);       
    }


}
