using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPosition : MonoBehaviour
{

    public int xPos, yPos;
    // Start is called before the first frame update
    public static PortalPosition instance = null;

    private void Start()
    {
        instance = this;
    }

}
