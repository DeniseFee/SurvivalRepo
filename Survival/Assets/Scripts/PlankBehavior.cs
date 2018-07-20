using System.Collections.Generic;
using UnityEngine;

public class PlankBehavior : MonoBehaviour
{
    [SerializeField] List<Transform> poles;
    [SerializeField] float distanceBetweenPoles;
    [SerializeField] float plankLength;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Built" && poles.Count < 2)
        {
            poles.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (poles.Contains(other.transform))
        {
            poles.Remove(other.transform);
        }
    }

    private void Update()
    {
        if (poles.Count == 2)
        {
            distanceBetweenPoles = Vector3.Distance(poles[0].position, poles[1].position);
            plankLength = distanceBetweenPoles;
        }
        else
        {
            plankLength = 5;
        }

        transform.localScale = new Vector3(plankLength, transform.localScale.y, transform.localScale.z); // TODO: maak built plank ook breder
    }
}
