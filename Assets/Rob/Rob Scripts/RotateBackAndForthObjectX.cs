using UnityEngine;

public class RotateBackAndForthObjectX : MonoBehaviour
{
    
    public float speed;
    public float maxRotation = 45f;

    // Update is called once per frame
    void Update()
    {
            transform.localRotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed), 0f , 0f);
    }

    
}
