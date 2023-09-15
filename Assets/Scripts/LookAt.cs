using UnityEngine;

public class LookAt : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mousePos - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, dir);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
