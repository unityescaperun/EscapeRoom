using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public int columns = 7;
    public int rows = 20;

    // 모든 스테이지 공용.
    public GameObject exit;
    public GameObject outerWall;
    public GameObject wall;
    public GameObject Key;

    // 스테이지 1 사용.
    public GameObject PortalUp;
    public GameObject PortalDown;
    public GameObject Item1;
    public GameObject Item2;

    private Transform boardHolder;
    private List<Vector3> gridPosition = new List<Vector3>();

    void InitialiseList() {
        gridPosition.Clear();
        for (int x = 1; x < columns - 1; x++) {
            for (int y = 1; y < rows - 1; y++) {
                gridPosition.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // 배경 맵 구현. GameManager의 stageLevel에 따라 맵 구현을 다르게 해준다.
    void Boardsetup() {
        boardHolder = new GameObject("Board").transform;
        // 스테이지 1
        if (GameManager.stageLevel == 1) {
            for (int x = -1; x < columns + 1; x++) {
                for (int y = -1; y < rows + 1; y++) {
                    GameObject toinstantiate = wall;
                    if (x == -1 || x == columns || y == -1 || y == rows || y == 3)
                        toinstantiate = outerWall;

                    GameObject instance = Instantiate(toinstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        // 스테이지 2
        else if(GameManager.stageLevel == 2) {
            for (int x = -1; x < columns + 1; x++) {
                for (int y = -1; y < rows + 1; y++) {
                    GameObject toinstantiate = wall;
                    if (x == -1 || x == columns || y == -1 || y == rows || y == 3 || y == 7)
                        toinstantiate = outerWall;

                    GameObject instance = Instantiate(toinstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    public void SetupScene() {
        Boardsetup();
        InitialiseList();

        // 스테이지 1 오브젝트 구현
        if (GameManager.stageLevel == 1) {
            Instantiate(exit, new Vector3(columns - 1, rows - 7, 0f), Quaternion.identity);
            Instantiate(PortalUp, new Vector3(0, rows - 7, 0f), Quaternion.identity);
            Instantiate(PortalDown, new Vector3(19, rows - 3, 0f), Quaternion.identity);
            Instantiate(Item1, new Vector3(7, rows - 3, 0f), Quaternion.identity);
            Instantiate(Item2, new Vector3(3, rows - 7, 0f), Quaternion.identity);
        }
    }
}
