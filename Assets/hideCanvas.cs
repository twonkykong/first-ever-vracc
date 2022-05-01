using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideCanvas : MonoBehaviour
{
    public Canvas canvas;
    public void pressed()
    {
        if (canvas.enabled) canvas.enabled = false;
        else canvas.enabled = true;
    }
}
