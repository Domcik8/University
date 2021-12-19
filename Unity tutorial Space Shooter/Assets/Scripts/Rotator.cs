using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float tumble;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 1.0f) * tumble;
    }
}
