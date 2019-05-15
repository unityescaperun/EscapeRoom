using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int columns = 7;
    public int rows = 20;

    public GameObject exit;
    public GameObject outerWall;
    public GameObject wall;

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

    void Boardsetup() {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++) {
            for (int y = -1; y < rows + 1; y++) {
                GameObject toinstantiate = wall;
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toinstantiate = outerWall;

                GameObject instance = Instantiate(toinstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void SetupScene() {
        Boardsetup();
        InitialiseList();
        
        Instantiate(exit, new Vector3(columns - 1, rows -7, 0f), Quaternion.identity);
    }
}
