using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughObject : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       //When player below platform the collider turn off
        if (Player.transform.position.y < transform.position.y)
        {
            collider.enabled = false;
        }
       //When player above platform the collider turn on
       if (Player.transform.position.y > transform.position.y)
        {
            collider.enabled = true;
        }

        // When player press "S" key the collider will turn off
        if (Input.GetAxis("Vertical") < 0)
        {
            collider.enabled = false;
        }
    }
}
