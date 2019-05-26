using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public static SpriteController instance = null;

    private void Start()
    {
        instance = this;
    }

    public Sprite NextDoor1;
    public Sprite NextDoor2;

    public void ChangeSprite(Collider2D door)
    {
        if (door.name == "Door1_1(Clone)" || door.name == "Door2_1(Clone)")
            door.GetComponent<SpriteRenderer>().sprite = NextDoor1;
        else if (door.name == "Door1_2(Clone)" || door.name == "Door2_2(Clone)")
            door.GetComponent<SpriteRenderer>().sprite = NextDoor2;
    }
}
