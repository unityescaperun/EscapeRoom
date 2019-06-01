﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    // row : 가로 column : 세로
    int columns = 7;
    int rows = 20;
    int columns2 = 5;
    int rows2 = 45;
    int columns3 = 11;
    int rows3 = 29;

    bool DoorOpen = false;

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
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Window1;
    public GameObject Window2;
    public GameObject Fireplace;

    // 스테이지 2 사용
    public GameObject Door1_1;
    public GameObject Door1_2;
    public GameObject Door2_1;
    public GameObject Door2_2;
    public GameObject NPC;

    // 스테이지 3 사용
    public GameObject Stair_Up;
    public GameObject Stair_Down;
    public GameObject Game_End_Door;


    private Transform boardHolder;
    private List<Vector3> gridPosition = new List<Vector3>();

    void InitialiseList() {
        gridPosition.Clear();
        for (int x = 1; x < rows - 1; x++) {
            for (int y = 1; y < columns - 1; y++) {
                gridPosition.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // 배경 맵 구현. GameManager의 stageLevel에 따라 맵 구현을 다르게 해준다.
    void Boardsetup() {
        boardHolder = new GameObject("Board").transform;

        // 스테이지 1
        if (GameManager.stageLevel == 1) {
            Debug.Log("월드1");
            for (int x = -1; x < rows + 1; x++) {
                for(int y = -1; y < columns + 1; y++) {
                    GameObject toInstantiate = wall;
                    if(x == -1 || x == rows || y == -1 || y == columns || y == 3)
                        toInstantiate = outerWall;

                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder); 
                }
            }
        }

        // 스테이지 2
        else if(GameManager.stageLevel == 2) {
            Debug.Log("월드2");
            for (int x = -1; x < rows2 + 1; x++) {
                for(int y = -1; y < columns2 + 1; y++) {
                    GameObject toInsatantiate = wall;
                    if(x == -1 || x == 13 || x == 15 || x == 29 || x == 31 || x == rows2)
                        toInsatantiate = outerWall;
                    else if(x == 14 || x == 30)
                        continue;
                    else
                        if(y == -1 || y == columns2)
                            toInsatantiate = outerWall;

                    GameObject instance = Instantiate(toInsatantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }          
        }

        // 스테이지 3
        else if (GameManager.stageLevel == 3) {
            Debug.Log("월드3");
            for (int x = -1; x < rows3 + 1; x++) {
                for (int y = -1; y < columns3 + 1; y++) {
                    GameObject toInsatantiate = wall;
                    if (x == -1 || x == 13 || x == 15 || x == rows3)
                        toInsatantiate = outerWall;
                    else if (x == 14 || x == 30 )
                        continue;
                    else if (y == -1 || y == 5 || y == columns3)
                        toInsatantiate = outerWall;

                    GameObject instance = Instantiate(toInsatantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

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
            Instantiate(exit, new Vector3(rows - 1, columns - 7, 0f), Quaternion.identity);
            Instantiate(PortalUp, new Vector3(0, columns - 7, 0f), Quaternion.identity);
            Instantiate(PortalDown, new Vector3(19, columns - 3, 0f), Quaternion.identity);
            Instantiate(Box1, new Vector3(7, columns - 3, 0f), Quaternion.identity);
            Instantiate(Box2, new Vector3(3, columns - 7, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(0, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(2, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(4, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(6, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window2, new Vector3(10, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(12, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(14, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(16, columns - 2, 0f), Quaternion.identity);
            Instantiate(Fireplace, new Vector3(16, columns - 7, 0f), Quaternion.identity);
        }
        else if(GameManager.stageLevel == 2) {
            Instantiate(Door1_1, new Vector3(12, 0, 0f), Quaternion.identity);
            Instantiate(Door1_2, new Vector3(16, 0, 0f), Quaternion.identity);
            Instantiate(Door2_1, new Vector3(28, 0, 0f), Quaternion.identity);
            Instantiate(Door2_2, new Vector3(32, 0, 0f), Quaternion.identity);
            Instantiate(NPC, new Vector3(23, 0, 0f), Quaternion.identity);
        }

        else if (GameManager.stageLevel == 3) {
            Instantiate(Door1_1, new Vector3(12, 0, 0f), Quaternion.identity);
            Instantiate(Stair_Up, new Vector3(0.1f, 0.33f, 0f), Quaternion.identity);
            Instantiate(Stair_Down, new Vector3(12, 6, 0f), Quaternion.identity);
            Instantiate(Door1_2, new Vector3(16, 0, 0f), Quaternion.identity);
            Instantiate(Door2_1, new Vector3(28, 0, 0f), Quaternion.identity);
            Instantiate(Door2_2, new Vector3(16, 6, 0f), Quaternion.identity);
            Instantiate(Game_End_Door, new Vector3(5.8f, 1, 0f), Quaternion.identity);
        }
    }
}
