using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour {
    private Vector2 velocity;
    private float smoothTimeX;
    private float smoothTimeY;

    public GameObject player;

    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start() {
        // 플레이어를 따라다닌다.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // 카메라 제어. 
    // 스테이지1 : y축 고정
    // 스테이지2 ~ : y축 고정 해제
    void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = 3f;

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}