using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public MapManager mapScript;

    private FadeManager fadeScript;

    public static int stageLevel = 1;       

    public AudioClip stage1;
    public AudioClip stage2;
    public AudioClip stage3;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        mapScript = GetComponent<MapManager>();
        fadeScript = FindObjectOfType<FadeManager>();
        InitGame();
    }

    void Start() {
    }

    void InitGame() {
        mapScript.SetupScene();
        FadeManager.instance.FadeIn();
    }

    public void EndGame() {
        stageLevel++;
        // 마지막 스테이지 설정
        if (stageLevel == 5) {
            Application.Quit();
        }
        else {
            StartCoroutine(FadeOutCoroutine());
        }      
    }

    IEnumerator FadeOutCoroutine() {
        FadeManager.instance.FadeOut();
        //FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
        if(stageLevel == 1)
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        else if (stageLevel == 2)
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = stage2;
        else if (stageLevel == 3)
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = stage3;
        else if (stageLevel == 4)
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
    }

    IEnumerator FadeInCoroutine() {
        yield return new WaitForSeconds(1f);
        
    }
}
