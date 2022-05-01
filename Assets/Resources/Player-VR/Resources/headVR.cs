using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class headVR : MonoBehaviour
{
    public Quaternion accRot;
    public Text nicknameleft, nicknameright;
    public float rotx, roty, rotz, rotw;

    private void Start()
    {
        nicknameleft = GameObject.FindWithTag("nickname left").GetComponent<Text>();
        nicknameright = GameObject.FindWithTag("nickname right").GetComponent<Text>();
    }

    private void Update()
    {
        rotx = Input.acceleration.z * 100f;
        roty = Input.compass.magneticHeading;
        rotz = Input.acceleration.x * 100f;

        Input.compass.enabled = true;
        accRot = Quaternion.Euler(-rotx, roty, -rotz);
        transform.rotation = Quaternion.Slerp(transform.rotation, accRot, 5f * Time.deltaTime);

        Debug.DrawRay(transform.position + transform.up * 0.5f, transform.forward * 4f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, 4f) && hit.collider.gameObject.GetComponent<PhotonView>() != null)
        {
            if (hit.collider.tag == "Player" && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                nicknameleft.text = hit.collider.gameObject.GetComponent<PhotonView>().Owner.NickName;
                nicknameright.text = hit.collider.gameObject.GetComponent<PhotonView>().Owner.NickName;
            }
        }
        else
        {
            nicknameleft.text = "";
            nicknameright.text = "";
        }
    }
}
