using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code from youtube tutorial How to Pick Up + Hold Objects in Unity (FPS) https://www.youtube.com/watch?v=pPcYr3tL3Sc
// from there I should be able to throw and projectory path based on this

public class PickUpBall : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 500f; //force at which the object is thrown at; I might use this for the ball projection 

    public float pickUpRange = 5f; //how far the player can pickup the object from

    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
