using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay; // delay between being shot and being destroyed
    public float dropDestroyDelay; // delay between hitting the dropper and being deleted
    public float heartOffset; //offset between the sheep and where the heart  will spawn
    public GameObject heartPrefab;
    
    private bool hitByHay; // sets to true when the sheep was hit
    private Collider myCollider; // reference to sheep's collider component
    private Rigidbody myRigidbody; // reference to sheep's rigidbody
    private SheepSpawner sheepSpawner;

    private bool hasDropped;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

        hitByHay = false;
        hasDropped = false;
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

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        Destroy(gameObject, gotHayDestroyDelay);

        // Adds TweenScale component to the game object that uses this script
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        // Remove oneself from the spawner list
        sheepSpawner.RemoveSheepFromList(gameObject);
        SoundManager.Instance.PlaySheepHitClip();

        GameStateManager.Instance.SavedSheep();

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
        else if (other.CompareTag("DropSheep") && !hasDropped)
        {
            Drop();
        }
    }

    private void Drop()
    {
        hasDropped = true; 

        // to get affected by gravity
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        Destroy(gameObject, dropDestroyDelay);

        SoundManager.Instance.PlaySheepDroppedClip();
        // Remove oneself from the spawner list
        sheepSpawner.RemoveSheepFromList(gameObject);

        GameStateManager.Instance.DroppedSheep();

    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
