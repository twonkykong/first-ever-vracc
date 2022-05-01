using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headPC1 : MonoBehaviour
{
    public GameObject body;
    public float sensitivity = 150, x, y;

    private void Update()
    {
        //rotation
        x += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
        y += Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);

        body.transform.rotation = Quaternion.Euler(0, x, 0);
        transform.rotation = Quaternion.Euler(-y, x, 0);
    }
}
