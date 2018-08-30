using UnityEngine;

public class DestroyAllTheTrees : MonoBehaviour
{
    private RaycastHit hit;

    void Start()
    {

    }

    void Update()
    {
        HandleRaycast();
    }

    void HandleRaycast()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

        if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Tree")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.transform.parent.GetComponent<TreeBehavior>().CutTree();
                }
            }
        }
    }
}
