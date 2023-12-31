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

        // Create Start Room
        int row = Random.Range(0, ROWS - 1);
        int col = Random.Range(0, COLS - 1);

        GameObject startRoom = map[row, col];
        startRoom.name = "Start room";
        startRoom.GetComponent<Room>().isStartRoom = true;
        startRoom.GetComponent<Room>().isOpen = true;
        Debug.Log("StartRoom[" + row + ", " + col + "]");

        roomList.Add(startRoom);

        // Generate Rooms
        int NB_ROOM = 10;

        while (roomList.Count < NB_ROOM)
        {
            int nRoom = Random.Range(0, roomList.Count - 1);
            int nRow = roomList[nRoom].GetComponent<Room>().row;
            int nCol = roomList[nRoom].GetComponent<Room>().col;

            GameObject room = roomList[nRoom];
            GameObject newRoom = null;

            int dir = Random.Range(0, 3);

            bool addRoom = false;
            if (dir == 0 && nRow > 0)
            {
                newRoom = map[nRow - 1, nCol];
                
                if (!newRoom.GetComponent<Room>().isOpen)
                {
                    room.GetComponent<Room>().upDoor = true;
                    newRoom.GetComponent<Room>().bottomDoor = true;
                    addRoom = true;
                }
            }
            else if(dir == 1 && nCol < COLS - 1)
            {
                newRoom = map[nRow, nCol + 1];

                if (!newRoom.GetComponent<Room>().isOpen)
                {
                    room.GetComponent<Room>().rightDoor = true;
                    newRoom.GetComponent<Room>().leftDoor = true;
                    addRoom = true;
                }
            }
            else if (dir == 2 && nRow < ROWS - 1)
            {
                newRoom = map[nRow + 1, nCol];

                if (!newRoom.GetComponent<Room>().isOpen)
                {
                    room.GetComponent<Room>().bottomDoor = true;
                    newRoom.GetComponent<Room>().upDoor = true;
                    addRoom = true;
                }
            }
            else if (dir == 3 && nCol > 0)
            {
                newRoom = map[nRow, nCol - 1];

                if (!newRoom.GetComponent<Room>().isOpen)
                {
                    room.GetComponent<Room>().leftDoor = true;
                    newRoom.GetComponent<Room>().rightDoor = true;
                    addRoom = true;
                }
            }

            if (addRoom)
            {
                newRoom.GetComponent<Room>().isOpen = true;
                roomList.Add(newRoom);
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
