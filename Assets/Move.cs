using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float moveSpeed = 8f;
    CharacterController con;
    Vector3 moveDirection = Vector3.zero;
    float jumpSpeed = 8f;
    float gravity = 20f;
    public GameObject effect;
    bool ishealing = false;

    // Use this for initialization
    void Start () {
        con = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(con.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDirection = (new Vector3(h, 0, v)).normalized;
            transform.LookAt(transform.position + moveDirection);


            moveDirection *= moveSpeed;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        con.Move(moveDirection * Time.deltaTime);
	}

    GameObject fx;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //내가작성
        //if (hit.collider.gameObject.name == "Step3" && !ishealing)
        //{
        //    var e = Instantiate(effect, transform.position, Quaternion.identity);
        //    e.transform.parent = transform;
        //    Invoke("LaunchProjectile", 3);
        //    Destroy(e.gameObject, 3f);
        //    ishealing = true;
        //}
        

        if (hit.collider.gameObject.name == "Step3")
        {
            if (!ishealing)
            {
                fx = Instantiate(effect, transform.Find("FxPos"));
                ishealing = true;
                Invoke("LaunchProjectile", 1.9f);
            }
        }
    }


    void LaunchProjectile()
    {
        Destroy(fx.gameObject);
        ishealing = false;
    }
}
