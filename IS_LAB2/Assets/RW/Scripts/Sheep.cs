using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay; // delay between being shot and being destroyed
    private bool hitByHay; // sets to true when the sheep was hit

    public float dropDestroyDelay; // delay between hitting the dropper and being deleted
    private Collider myCollider; // reference to sheep's collider component
    private Rigidbody myRigidbody; // reference to sheep's rigidbody

    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        hitByHay = true; 
        runSpeed = 0; 

        Destroy(gameObject, gotHayDestroyDelay);
        
        // Remove oneself from the spawner list
        sheepSpawner.RemoveSheepFromList(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If it collides with hay and was not previously hit
        if (other.CompareTag("Hay") && !hitByHay) 
        {

            // Destroy the hay bullet
            Destroy(other.gameObject);
            HitByHay(); 
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void Drop()
    {
        // to get affected by gravity
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        Destroy(gameObject, dropDestroyDelay); 

        // Remove oneself from the spawner list
        sheepSpawner.RemoveSheepFromList(gameObject);
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
