using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    public Transform headTransform;
    public Camera mainCamera; // Assign your main camera in the inspector if not using Camera.main
    public bool flipCharacter = true; // Set to true if your character should flip when looking left

    private Vector3 initialScale;
    private void Start()
    {
        // Initialize the scale based on the object's starting scale
        initialScale = headTransform.localScale;

        // If the main camera is not assigned, use the default camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    void Update()
    {
        RotateHeadToMouse();
    }
    private void RotateHeadToMouse()
    {
        // Step 1: Get the mouse position in world space
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Step 2: Calculate direction vector from head to mouse position
        Vector3 directionToMouse = mouseWorldPosition - headTransform.position;

        // Step 3: Remove the Z component if working in a 2D plane
        directionToMouse.z = 0;

        // Step 4: Calculate the angle to rotate the head
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Step 5: Rotate the head to face the mouse position
        headTransform.rotation = Quaternion.Euler(0, 0, angle);

        // Step 6: Handle character flipping if needed
        // if (flipCharacter)
        // {
        //     if (directionToMouse.x < 0)
        //     {
        //         // Mouse is on the left side of the character, flip to the left
        //         headTransform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        //     }
        //     else
        //     {
        //         // Mouse is on the right side, face right
        //         headTransform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        //     }
        // }
    }
}
