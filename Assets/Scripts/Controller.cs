using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, y, 0);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
