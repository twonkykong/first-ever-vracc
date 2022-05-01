using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideShow : MonoBehaviour
{
    public GameObject[] hide;
    public GameObject[] show;
    public void press()
    {
        foreach (GameObject g in hide) g.SetActive(false);
        foreach (GameObject g in show) g.SetActive(true);
    }
}
