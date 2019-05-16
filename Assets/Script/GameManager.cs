using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public MapManager mapScript;

    static int stageLevel = 1;
    static bool ifEnded = false;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        mapScript = GetComponent<MapManager>();
        InitGame();
    }

    void InitGame() {
        mapScript.SetupScene();
    }

    public static void EndGame() {
        stageLevel++;

        if (stageLevel == 3)
            ifEnded = true;
        else
            SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
            
    }
    void Update() {

    }
}
