using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public MapManager mapScript;

    public static int stageLevel = 3;       

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
        // 마지막 스테이지 설정
        if (stageLevel == 4) {
            Application.Quit();
        }
        else
        {
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
        }
    }
    void Update() {

    }
}
