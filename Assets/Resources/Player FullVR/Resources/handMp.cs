using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class handMp : MonoBehaviourPunCallbacks
{
    public float joystickVert, joystickHor, angle;
    public GameObject obj, prefab, selectedPlacePrefab, selectedPlace;
    public bool holding;
    public Text nicknameleft, nicknameright;

    private void Start()
    {
        selectedPlace = Instantiate(selectedPlacePrefab, transform.position, Quaternion.identity);
        selectedPlace.transform.localScale = new Vector3(0, 0, 0);
        nicknameleft = GameObject.FindWithTag("nickname left").GetComponent<Text>();
        nicknameright = GameObject.FindWithTag("nickname right").GetComponent<Text>();
    }

    private void Update()
    {
        //ROTATION
        joystickVert = Input.GetAxis("Mouse Y");
        joystickHor = Input.GetAxis("Mouse X");

        if (joystickVert >= 0.2f) transform.Rotate(Vector3.right, joystickVert * 40 * Time.deltaTime);
        else if (joystickVert <= -0.2f) transform.Rotate(Vector3.right, joystickVert * 40 * Time.deltaTime);
        if (joystickHor >= 0.2f) transform.Rotate(Vector3.forward, joystickHor * 40 * Time.deltaTime);
        else if (joystickHor <= -0.2f) transform.Rotate(Vector3.forward, joystickHor * 40 * Time.deltaTime);

        //reset rotation
        if (Input.GetKeyDown("joystick 1 button 9")) transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        //GRABING
        //is looking on grabable obj
        Debug.DrawRay(transform.position, -transform.up, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 5f))
        {
            selectedPlace.transform.localScale = new Vector3(1, 1, 1);
            selectedPlace.transform.position = hit.point + transform.up;

            //nickname
            if (hit.collider.tag == "Player" && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                nicknameleft.text = hit.collider.gameObject.GetComponent<PhotonView>().Owner.NickName;
                nicknameright.text = hit.collider.gameObject.GetComponent<PhotonView>().Owner.NickName;
            }
            else
            {
                nicknameleft.text = "";
                nicknameright.text = "";
            }

            //grabing continue
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

            //spawn prefabs
            if (Input.GetKeyDown("joystick 1 button 7") && holding == false)
            {
               PhotonNetwork.Instantiate(prefab.name, hit.point + transform.up, Quaternion.identity);
            }
        } 
        else if (holding == false)
        {
            selectedPlace.transform.localScale = new Vector3(0, 0, 0);
            obj = null;
        }

        //grab
        if (Input.GetKeyDown("joystick 1 button 6") && obj != null)
        {
            if (holding == true) holding = false;
            else holding = true;
        }

        if (holding == true)
        {
            if (Input.GetKeyDown("joystick 1 button 7"))
            {
                holding = false;
                obj.GetComponent<Rigidbody>().AddForce(-transform.up * 7f + (-transform.up * 3f), ForceMode.Impulse);
            }

            selectedPlace.transform.localScale = new Vector3(0, 0, 0);
            obj.transform.position = transform.position + -transform.up * 4f; ;
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
