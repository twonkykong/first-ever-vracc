using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class headNoVR : MonoBehaviour
{
    Vector3 first, second;
    float xAngle, yAngle, xAngleTemp, yAngleTemp;
    public GameObject body, obj, prefab, selectedPlacePrefab, selectedPlace;
    public bool holding = false;
    public Text grabText, nicknametext, createThrow;
    int touch;

    public Canvas canvas;

    private void Start()
    {
        selectedPlace = Instantiate(selectedPlacePrefab, transform.position, Quaternion.identity);
        selectedPlace.transform.localScale = new Vector3(0, 0, 0);
        xAngle = 0;
        yAngle = 0;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    private void Update()
    {
        canvas = GameObject.Find("Canvas player").GetComponent<Canvas>();
        //rotation
        if (transform.rotation.x > 90) transform.rotation = new Quaternion(90, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        else if (transform.rotation.x < -90) transform.rotation = new Quaternion(-90, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        body.transform.rotation = Quaternion.Euler(0, xAngle, 0);

        if (Input.touchCount > 0 && canvas.enabled)
        { 
            body.transform.rotation = Quaternion.Euler(0, xAngle, 0);
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.x >= Screen.width / 2)
                {
                    touch = i;
                    Debug.Log(i);
                    break;
                }
            }

            if (Input.GetTouch(touch).position.x >= Screen.width / 2)
            {
                if (Input.GetTouch(touch).phase == TouchPhase.Began)
                {
                    first = Input.GetTouch(touch).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }

                if (Input.GetTouch(touch).phase == TouchPhase.Moved && Input.GetTouch(touch).position.x >= Screen.width / 2)
                {
                    second = Input.GetTouch(touch).position;
                    xAngle = xAngleTemp + (second.x - first.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (second.y - first.y) * 90 / Screen.width;
                    transform.rotation = Quaternion.Euler(-yAngle, xAngle, 0);
                    body.transform.rotation = Quaternion.Euler(0, xAngle, 0);
                }
            }
        }
        body.transform.rotation = Quaternion.Euler(0, xAngle, 0);

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
            createThrow.text = "throw";
            grabText.text = "drop";
            obj.transform.position = transform.position + (transform.forward * 4f) + transform.up * 0.7f;
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
        else
        {
            grabText.text = "grab";
            createThrow.text = "create";
        }
    }

    public void grab()
    {
        if (obj != null)
        {
            if (holding == false) holding = true;
            else holding = false;
        }
    }

    public void createThrowObj()
    {
        if (holding == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * 0.7f, transform.forward, out hit, 5f))
            {
                PhotonNetwork.Instantiate(prefab.name, hit.point + transform.up, Quaternion.identity);
            }
        }
        else
        {
            holding = false;
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 7f + transform.up * 3f, ForceMode.Impulse);
        }
    }
}
