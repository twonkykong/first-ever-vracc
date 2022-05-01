using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyVR : MonoBehaviour
{
    public GameObject head, moving;
    public bool grounded;
    public float z = 0;

    private void Update()
    {
        //rotation
        Input.compass.enabled = true;
        transform.rotation = Quaternion.Euler(0f, Input.compass.magneticHeading, 0f);

        //movement
        if (moving.transform.position.y - transform.position.y <= -0.5f) z = 1;
        else z = 0;

        if (z == 0) GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f);

        if (GetComponent<Rigidbody>().velocity.x > 5f) GetComponent<Rigidbody>().velocity = new Vector3(5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z > 5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 5f);
        if (GetComponent<Rigidbody>().velocity.x < -5f) GetComponent<Rigidbody>().velocity = new Vector3(-5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z < -5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, -5f);

        GetComponent<Rigidbody>().AddForce(transform.forward * z, ForceMode.VelocityChange);

        //jump
        if (Physics.CheckSphere(transform.position - transform.up, 1f)) grounded = true;
        else grounded = false;

        if (Physics.Raycast(transform.position - transform.up * 0.2f, transform.forward, 1.5f) && grounded) GetComponent<Rigidbody>().AddForce(transform.up * 6f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        if (other.tag == "kill")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            transform.position = pos;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }
}
