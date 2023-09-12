using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GameObject[,] map;

    [SerializeField] private GameObject room;
    public List<GameObject> roomList;

    const int COLS = 9;
    const int ROWS = 6;

    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PurgeMap();
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        map = new GameObject[ROWS, COLS];

        float cellWidth = 17.75f;
        float cellHeight = 10.0f;
        float x;
        float y = 0;
        float offsetPos = 2.0f;

        for (int l = 0 ; l < ROWS; l++)
        {
            x = 0;
            for (int c = 0; c < COLS; c++)
            {
                Debug.Log("[" + l + ", " + c + "]");
                map[l, c] = Instantiate(room, new Vector3(x, y, 0), Quaternion.identity, transform);
                map[l, c].GetComponent<Room>().row = l;
                map[l, c].GetComponent<Room>().col = c;

                x += cellWidth + offsetPos;
            }
            y -= cellHeight + offsetPos;
        }

        // Generate Rooms
        int NB_ROOM = 20;

        // Create Start Room
        int row = Random.Range(0, ROWS - 1);
        int col = Random.Range(0, COLS - 1);

        GameObject startRoom = map[row, col];
        startRoom.name = "Start room";
        startRoom.GetComponent<Room>().isOpen = true;
        Debug.Log("StartRoom[" + row + ", " + col + "]");

        roomList.Add(startRoom);

        while (roomList.Count < NB_ROOM)
        {
            int nRoom = Random.Range(0, roomList.Count - 1);
            int nRow = roomList[nRoom].GetComponent<Room>().row;
            int nCol = roomList[nRoom].GetComponent<Room>().col;

            GameObject room = roomList[nRoom];
            GameObject newRoom = null;

            int dir = Random.Range(0, 3);

            if (dir == 0 && nRow > 0)
            {
                newRoom = map[nRow - 1, nCol];
                
                if (!newRoom.GetComponent<Room>().isOpen)
                {
                    room.GetComponent<Room>().upDoor = true;
                    newRoom.GetComponent<Room>().bottomDoor = true;
                }
            }
        }
    }

    void PurgeMap()
    {
        foreach (GameObject room in map)
        {
            Destroy(room);
        }

        for (int i = roomList.Count - 1;  i >= 0; i--)
        {
            roomList.Remove(roomList[i]);
        }
    }
}
