using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private Vector2 contentStartPos;

    public float dragSpeed = 1.0f;

    private void OnMouseDrag()
    {
        Vector2 dragDelta = Input.mousePosition;
        Vector2 newPos = contentStartPos + new Vector2(dragDelta.x * dragSpeed, 0);

        contentStartPos = newPos;
    }
}
