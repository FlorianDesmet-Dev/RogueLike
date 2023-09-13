using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isStartRoom = false;
    public bool isOpen = false;

    public bool upDoor = false;
    public bool rightDoor = false;
    public bool bottomDoor = false;
    public bool leftDoor = false;

    [SerializeField] private GameObject[] doors;

    public int row;
    public int col;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.gray;

        if (isOpen && sr.color != Color.white)
        {
            sr.color = Color.white;
        }

        if (isStartRoom && isOpen && sr.color != Color.red)
        {
            sr.color = Color.red;
        }

        if (isOpen)
        {
            if (upDoor)
            {
                Instantiate(doors[0], new Vector3(transform.position.x, transform.position.y + 4, 0), Quaternion.identity);
            }
            if (rightDoor)
            {
                Instantiate(doors[1], new Vector3(transform.position.x + 8, transform.position.y, 0), Quaternion.identity);
            }
            if (bottomDoor)
            {
                Instantiate(doors[2], new Vector3(transform.position.x, transform.position.y - 4, 0), Quaternion.identity);
            }
            if (leftDoor)
            {
                Instantiate(doors[3], new Vector3(transform.position.x - 8, transform.position.y, 0), Quaternion.identity);
            }
        }
    }
}
