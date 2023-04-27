using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{

    public float timeToDestroy; //time until the object is destroyed

    // Start is called before the first frame update
    void Start()
    {
        // Makes the object destroy after that time
        Destroy(gameObject, timeToDestroy);
    }

}
