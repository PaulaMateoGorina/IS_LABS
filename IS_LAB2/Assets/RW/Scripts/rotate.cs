using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public Vector3 rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
