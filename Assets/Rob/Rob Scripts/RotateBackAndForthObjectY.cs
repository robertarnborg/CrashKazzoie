using UnityEngine;

public class RotateBackAndForthObjectY : MonoBehaviour
{

    public float speed;
    public float maxRotation = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * speed), 0f);
    }


}
