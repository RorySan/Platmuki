using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 1f;
    void Update()
    {
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * scrollSpeed);
    }
}
