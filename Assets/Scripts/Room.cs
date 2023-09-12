using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isOpen = false;

    public bool upDoor = false;
    public bool rightDoor = false;
    public bool bottomDoor = false;
    public bool leftDoor = false;

    public int row;
    public int col;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red;
    }

    private void Update()
    {
        if (isOpen && sr.color != Color.green)
        {
            sr.color = Color.green;
        }
    }


}
