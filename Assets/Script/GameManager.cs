using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public MapManager mapScript;

    private FadeManager fadeScript;

    public static int stageLevel = 2;       

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        mapScript = GetComponent<MapManager>();
        InitGame();
    }

    void Start() {
        fadeScript = FindObjectOfType<FadeManager>();
    }

    void InitGame() {
        mapScript.SetupScene();
    }

    public void EndGame() {
        stageLevel++;
        // 마지막 스테이지 설정
        if (stageLevel == 4) {
            Application.Quit();
        }
        else {
            StartCoroutine(FadeOutCoroutine());
        }
        
    }

    IEnumerator FadeOutCoroutine() {
        FadeManager.instance.FadeOut();
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }

    IEnumerator FadeInCoroutine() {
        mapScript.SetupScene();
        yield return new WaitForSeconds(1f);
        FadeManager.instance.FadeIn();
    }
}
