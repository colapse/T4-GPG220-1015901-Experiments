using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GizmoBox : MonoBehaviour
{
    public BoxCollider b;
    private Vector3 size;

    public Color32 color = new Color32(33,33,33,150);

    private void OnEnable()
    {
        if(b == null)
            b = GetComponent<BoxCollider>();
        size = transform.lossyScale;
        size.Scale(b?.size ?? Vector3.one);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, size);
    }
}
