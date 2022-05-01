using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class headPC : MonoBehaviour
{
    public float x, y, sensitivity;
    public GameObject prefab, obj, selectedPlacePrefab, selectedPlace, body;
    public Text nicknametext;
    bool holding;

    public void Start()
    {
        selectedPlace = Instantiate(selectedPlacePrefab, transform.position, Quaternion.identity);
        selectedPlace.transform.localScale = new Vector3(0, 0, 0);

        Vector3 euler = transform.rotation.eulerAngles;
        x = euler.x;
        y = euler.y;
    }

    void Update()
    {
        //rotation
        Debug.Log("x: " + Input.GetAxis("Mouse X") + " y: " + Input.GetAxis("Mouse Y"));
        x += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
        y += Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);
        body.transform.rotation = Quaternion.Euler(0, x, 0);
        transform.rotation = Quaternion.Euler(-y, transform.rotation.eulerAngles.y, 0);

        //grabing
        RaycastHit hit;
        Debug.DrawRay(transform.position + transform.up * 0.7f, transform.forward, Color.yellow);
        if (Physics.Raycast(transform.position + transform.up * 0.7f, transform.forward, out hit, 5f))
        {
            selectedPlace.transform.localScale = new Vector3(1, 1, 1);
            selectedPlace.transform.position = hit.point + transform.up;

            //show nickname
            if (hit.collider.tag == "Player" && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                nicknametext.text = hit.collider.gameObject.GetComponent<PhotonView>().Owner.NickName;
            }
            //grabing continue
            Debug.Log(hit.collider.name);
            if (holding == false)
            {
                if (hit.collider.gameObject.GetComponent<prefabObj>() != null)
                {
                    obj = hit.collider.gameObject;
                }
                else
                {
                    obj = null;
                }
            }
        }
        else if (holding == false)
        {
            selectedPlace.transform.localScale = new Vector3(0, 0, 0);
            nicknametext.text = "";
            obj = null;
        }
        else
        {
            selectedPlace.transform.localScale = new Vector3(0, 0, 0);
            nicknametext.text = "";
        }

        if (holding == true)
        {
            selectedPlace.transform.localScale = new Vector3(0, 0, 0);
            obj.transform.position = transform.position + (transform.forward * 4f) + transform.up * 0.7f;
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }

        //inputs
        if (Input.GetMouseButtonDown(0))
        {
            if (holding == false)
            {
                if (Physics.Raycast(transform.position + transform.up * 0.7f, transform.forward, out hit, 5f))
                {
                    Instantiate(prefab, hit.point + transform.up, Quaternion.identity);
                }
            }
            else
            {
                holding = false;
                obj.GetComponent<Rigidbody>().AddForce(transform.forward * 7f + transform.up * 3f, ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (obj != null)
            {
                if (holding == false) holding = true;
                else holding = false;
            }
        }
    }
}
