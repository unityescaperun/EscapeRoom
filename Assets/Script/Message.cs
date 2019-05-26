using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    //public string sentences;
    // Start is called before the first frame update
    public Dialogue dialogue;

    public static Message instance = null;
    
    private void Start() {
        instance = this;
    }
}
