using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bodyMp : MonoBehaviourPunCallbacks
{
    public float joystickVert, joystickHor;
    bool grounded;
    Vector3 move;

    private void Update()
    {
        //rotation
        Input.compass.enabled = true;
        transform.rotation = Quaternion.Euler(0f, Input.compass.magneticHeading, 0f);

        //movement
        joystickVert = Input.GetAxis("Vertical");
        joystickHor = Input.GetAxis("Horizontal");

        move = (transform.forward * joystickVert + transform.right * joystickHor).normalized;

        GetComponent<Rigidbody>().AddForce(move, ForceMode.VelocityChange);

        if (joystickVert == 0 && joystickHor == 0) GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

        if (GetComponent<Rigidbody>().velocity.x > 5f) GetComponent<Rigidbody>().velocity = new Vector3(5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z > 5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 5f);
        if (GetComponent<Rigidbody>().velocity.x < -5f) GetComponent<Rigidbody>().velocity = new Vector3(-5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z < -5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, -5f);

        //jump
        if (Physics.Raycast(transform.position, -transform.up, 1.5f)) grounded = true;
        else grounded = false;

        if (Input.GetKeyDown("joystick 1 button 0") && grounded) GetComponent<Rigidbody>().AddForce(transform.up * 5f, ForceMode.Impulse);
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
}
