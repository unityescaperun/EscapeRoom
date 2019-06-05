using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    // row : 가로 column : 세로
    int columns = 7;
    int rows = 20;
    int columns2 = 5;
    int rows2 = 45;
    int columns3 = 11;
    int rows3 = 70;

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
    public GameObject Board;
    public GameObject Sink;
    public GameObject GasRange;
    public GameObject Part1;
    public GameObject Part2;
    public GameObject Part3;
    public GameObject Shelf1;
    public GameObject Shelf2;
    public GameObject ShelfPoison;
    public GameObject Garbage_Wall;
    public GameObject Restaurant_Window_Middle;
    public GameObject Restaurant_Window_Bottom;
    public GameObject Restaurant_Window_Left1;
    public GameObject Restaurant_Window_Right1;
    public GameObject Restaurant_Window_Left2;
    public GameObject Restaurant_Window_Right2;
    public GameObject Chair1;
    public GameObject Chair2;
    public GameObject Table;

    // 스테이지3 사용
    public GameObject JailWall;
    public GameObject Stair_Up;
    public GameObject Stair_Down;
    public GameObject Game_End_Door;
    public GameObject Door3_1;
    public GameObject Chain;
    public GameObject TV;
    public GameObject Table_Stage3;
    public GameObject Bookshelf;
    public GameObject Bookshelf2;
    public GameObject Bookshelf3;
    public GameObject Clock;
    public GameObject Windows;
    public GameObject Window3_1;
    public GameObject Window3_2;
    public GameObject Picture3_1;
    public GameObject Picture3_2;
    public GameObject PC;
    public GameObject Piano;
    public GameObject TreeVase;
    public GameObject FlowerVase1;
    public GameObject FlowerVase2;
    public GameObject Jail1;
    public GameObject Jail2;
    public GameObject Jail3;
    public GameObject Jail4;



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
                for (int y = -1; y < columns + 1; y++) {
                    GameObject toInstantiate = wall;
                    if (x == -1 || x == rows || y == -1 || y == columns || y == 3)
                        toInstantiate = outerWall;

                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        // 스테이지 2
        else if (GameManager.stageLevel == 2) {
            Debug.Log("월드2");
            for (int x = -1; x < rows2 + 1; x++) {
                for (int y = -1; y < columns2 + 1; y++) {
                    GameObject toInstantiate = wall;
                    if (x > 31 && x < rows2)
                        toInstantiate = Garbage_Wall;
                    else if (x == 16) {
                        if (y > 0 && y < columns2)
                            toInstantiate = Restaurant_Window_Left1;
                        else if (y == 0)
                            toInstantiate = Restaurant_Window_Left2;
                    }
                    else if (x == 28) {
                        if (y > 0 && y < columns2)
                            toInstantiate = Restaurant_Window_Right1;
                        else if (y == 0)
                            toInstantiate = Restaurant_Window_Right2;
                    }
                    else if (x > 16 && x < 28) {
                        if (y > 0 && y < columns2)
                            toInstantiate = Restaurant_Window_Middle;
                        else if (y == 0)
                            toInstantiate = Restaurant_Window_Bottom;
                    }

                    if (x == -1 || x == 13 || x == 15 || x == 29 || x == 31 || x == rows2)
                        toInstantiate = outerWall;
                    else if (x == 14 || x == 30)
                        continue;
                    else
                        if (y == -1 || y == columns2)
                        toInstantiate = outerWall;

                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        // 스테이지 3
        else if (GameManager.stageLevel == 3) {
            Debug.Log("월드3");
            for (int x = -1; x < 29 + 1; x++) {
                for (int y = -1; y < columns3 + 1; y++) {
                    GameObject toInsatantiate = wall;
                    if (x == -1 || x == 13 || x == 15 || x == 29)
                        toInsatantiate = outerWall;
                    else if (x == 14 || x == 30)
                        continue;
                    else if (y == -1 || y == 5 || y == columns3)
                        toInsatantiate = outerWall;

                    GameObject instance = Instantiate(toInsatantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }

            for (int x = 49; x < 66; x++) {
                for(int y = -1; y < 7; y++) {
                    GameObject toInsatantiate = JailWall;
                    if (x == 49 || x == 65) {
                        toInsatantiate = outerWall;
                    }
                    else if (y == -1 || y == 6) {
                        toInsatantiate = outerWall;
                    }
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
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
            Instantiate(exit, new Vector3(rows - 1, columns - 7, 0f), Quaternion.identity);
            Instantiate(PortalUp, new Vector3(0.1f, 0.0f, 0f), Quaternion.identity);
            Instantiate(PortalDown, new Vector3(18.95f, 4.05f, 0f), Quaternion.identity);
            Instantiate(Box1, new Vector3(7, 3.85f, 0f), Quaternion.identity);
            Instantiate(Box2, new Vector3(3, -0.14f, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(1, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(3, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(5, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(7, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window2, new Vector3(11, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(13, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(15, columns - 2, 0f), Quaternion.identity);
            Instantiate(Window1, new Vector3(17, columns - 2, 0f), Quaternion.identity);
            Instantiate(Fireplace, new Vector3(16, 0.2f, 0f), Quaternion.identity);
        }
        else if (GameManager.stageLevel == 2) {
            Instantiate(Door1_1, new Vector3(12, 0, 0f), Quaternion.identity);
            Instantiate(Door1_2, new Vector3(16, 0, 0f), Quaternion.identity);
            Instantiate(Door2_1, new Vector3(28, 0, 0f), Quaternion.identity);
            Instantiate(Door2_2, new Vector3(32, 0, 0f), Quaternion.identity);

            Instantiate(NPC, new Vector3(23, 0, 0f), Quaternion.identity);

            Instantiate(ShelfPoison, new Vector3(1, 0.3f, 0f), Quaternion.identity);
            Instantiate(Part2, new Vector3(2, 0.3f, 0f), Quaternion.identity);
            Instantiate(Part1, new Vector3(3, 0.3f, 0f), Quaternion.identity);
            Instantiate(Part2, new Vector3(4, 0.3f, 0f), Quaternion.identity);
            Instantiate(Sink, new Vector3(5, 0.3f, 0f), Quaternion.identity);
            Instantiate(Board, new Vector3(6, 0.3f, 0f), Quaternion.identity);
            Instantiate(GasRange, new Vector3(7, 0.3f, 0f), Quaternion.identity);

            Instantiate(Part3, new Vector3(8, 0.15f, 0f), Quaternion.identity);
            Instantiate(Shelf1, new Vector3(9, 0.3f, 0f), Quaternion.identity);
            Instantiate(Shelf1, new Vector3(10, 0.3f, 0f), Quaternion.identity);
            Instantiate(Shelf2, new Vector3(11, 0.3f, 0f), Quaternion.identity);

            Instantiate(Chair1, new Vector3(17, -0.05f, 0f), Quaternion.identity);
            Instantiate(Table, new Vector3(19, -0.15f, 0f), Quaternion.identity);
            Instantiate(Chair2, new Vector3(21, -0.05f, 0f), Quaternion.identity);

            Instantiate(Chair1, new Vector3(23, -0.05f, 0f), Quaternion.identity);
            Instantiate(Table, new Vector3(25, -0.15f, 0f), Quaternion.identity);
            Instantiate(Chair2, new Vector3(27, -0.05f, 0f), Quaternion.identity);

            Instantiate(exit, new Vector3(rows2 - 1, 0, 0f), Quaternion.identity);
        }
        else if (GameManager.stageLevel == 3) {
            Instantiate(Door1_1, new Vector3(12, 0, 0f), Quaternion.identity);
            Instantiate(Stair_Up, new Vector3(0.04f, 0.27f, 0f), Quaternion.identity);
            Instantiate(Stair_Down, new Vector3(12, 6, 0f), Quaternion.identity);
            Instantiate(Door1_2, new Vector3(16, 0, 0f), Quaternion.identity);
            Instantiate(Door3_1, new Vector3(28, 0, 0f), Quaternion.identity);
            Instantiate(Door2_2, new Vector3(16, 6, 0f), Quaternion.identity);
            Instantiate(Bookshelf, new Vector3(1.72f, 6.51f, 0f), Quaternion.identity);
            Instantiate(Bookshelf2, new Vector3(18, 6.34f, 0f), Quaternion.identity);
            Instantiate(Bookshelf3, new Vector3(20.7f, 6.34f, 0f), Quaternion.identity);
            Instantiate(Chain, new Vector3(23, 8.08f, 0f), Quaternion.identity);
            Instantiate(Table_Stage3, new Vector3(20, -0.2f, 0f), Quaternion.identity);
            Instantiate(Windows, new Vector3(19, 1.5f, 0f), Quaternion.identity);
            Instantiate(Windows, new Vector3(17, 1.5f, 0f), Quaternion.identity);
            Instantiate(Windows, new Vector3(27, 1.5f, 0f), Quaternion.identity);
            Instantiate(Windows, new Vector3(21, 1.5f, 0f), Quaternion.identity);
            Instantiate(Window3_1, new Vector3(25, 1.5f, 0f), Quaternion.identity);
            Instantiate(Window3_2, new Vector3(23, 1.5f, 0f), Quaternion.identity);
            Instantiate(Picture3_1, new Vector3(8.4f, 1.5f, 0f), Quaternion.identity);
            Instantiate(Picture3_2, new Vector3(3.3f, 1.5f, 0f), Quaternion.identity);
            Instantiate(Clock, new Vector3(24, 0.4f, 0f), Quaternion.identity);
            Instantiate(TV, new Vector3(20, 0.48f, 0f), Quaternion.identity);
            Instantiate(PC, new Vector3(5, 5.9f, 0f), Quaternion.identity);
            Instantiate(Piano, new Vector3(26, 6.53f, 0f), Quaternion.identity);
            Instantiate(TreeVase, new Vector3(2, 0.31f, 0f), Quaternion.identity);
            Instantiate(TreeVase, new Vector3(10, 0.31f, 0f), Quaternion.identity);
            Instantiate(FlowerVase1, new Vector3(18, -0.15f, 0f), Quaternion.identity);
            Instantiate(FlowerVase1, new Vector3(22, -0.15f, 0f), Quaternion.identity);
            Instantiate(FlowerVase1, new Vector3(26, -0.15f, 0f), Quaternion.identity);
            Instantiate(FlowerVase2, new Vector3(7, 6, 0f), Quaternion.identity);
            Instantiate(Game_End_Door, new Vector3(5.8f, 1, 0f), Quaternion.identity);
            Instantiate(Jail1, new Vector3(55, 0.44f, 0f), Quaternion.identity);
            Instantiate(Jail2, new Vector3(57, 0.44f, 0f), Quaternion.identity);
            Instantiate(Jail3, new Vector3(59, 0.44f, 0f), Quaternion.identity);
            Instantiate(Jail4, new Vector3(61, 0.44f, 0f), Quaternion.identity);
        }

    }
}
