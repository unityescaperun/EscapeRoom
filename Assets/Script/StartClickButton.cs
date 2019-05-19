using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClickButton : MonoBehaviour {
    void Start() {

    }

    void Update() {

    }

    public void OnClickExit() {
        SceneManager.LoadScene("Scene1");
    }
}
