using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    [SerializeField] private CameraHandler parent;

    void Start()
    {
        parent = transform.parent.GetComponent<CameraHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.OnChildTriggerEnter(gameObject.name, collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        parent.OnChildTriggerStay(gameObject.name, collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.OnChildTriggerExit(gameObject.name, collision);
    }
}
