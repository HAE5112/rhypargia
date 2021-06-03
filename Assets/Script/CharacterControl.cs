using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalPower;
    private float horizontalMovement;
    private bool jumpFlag = false;
    private float jumpTime = 0;
    private int floorCnt = 0;
    private new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * horizontalSpeed;
        if (Input.GetButtonDown("Jump") && floorCnt > 0)
        {
            jumpFlag = true;
            jumpTime = 0f;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpFlag = false;
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(horizontalMovement, rigidbody.velocity.y);
        if (jumpFlag)
        {
            jumpTime += Time.fixedDeltaTime;
            if (jumpTime < 0.5f)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalPower);
            else
                jumpFlag = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        floorCnt++;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        floorCnt--;
    }
}
