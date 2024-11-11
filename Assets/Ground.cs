using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    public Tilemap tilemap;
    public float speed = 2f;
    private Vector3 originalPosition;
    private float moveDistance;

    void Start()
    {
        originalPosition = tilemap.transform.position;

        BoundsInt bounds = tilemap.cellBounds;
        int widthInCells = bounds.xMax - bounds.xMin;
        Debug.Log("Tilemap x : " + tilemap.size.x);


        moveDistance = (widthInCells) / 4;

    }

    // Update is called once per frame
    void Update()
    {
        if (tilemap.transform.position.x > originalPosition.x - moveDistance)
        {
            tilemap.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            tilemap.transform.position = originalPosition;
        }

        // Reset position back to original
    }
}
