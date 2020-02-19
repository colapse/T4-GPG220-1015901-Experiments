using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoBox : MonoBehaviour
{
    private BoxCollider b;
    private Vector3 size;

    public Color32 color = new Color32(33,33,33,150);
    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<BoxCollider>();
        size = transform.localScale;
        size.Scale(b?.size ?? Vector3.one);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (b == null)
            return;

        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, size);
    }
}
