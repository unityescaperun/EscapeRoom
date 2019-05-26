using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    // Start is called before the first frame update

    public static PortalController instance = null;

    private void Start()
    {
        instance = this;
    }

    public void Teleport(GameObject player, PortalPosition pos)
    {
        player.transform.position = new Vector3(pos.xPos, pos.yPos, 0f);
    }
}
