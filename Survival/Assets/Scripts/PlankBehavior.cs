using System.Collections.Generic;
using UnityEngine;

public class PlankBehavior : MonoBehaviour
{
    [SerializeField] private List<Transform> poles;
    [SerializeField] private float distanceBetweenPoles;
    [SerializeField] private GameObject invisibleWallPrefab;
    [SerializeField] private GameObject invisibleWallBetweenPoles;
    [SerializeField] private float plankLength;
    [SerializeField] private BuildTest buildTest;
    [SerializeField] private GameObject shownBuildable;

    private bool invisibleWallIsActive = false;

    private void Start()
    {
        buildTest = FindObjectOfType<BuildTest>();
    }

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
            if (!invisibleWallIsActive)
            {
                CreateInvisibleWall();
            }
        }
        else
        {
            DestroyInvisibleWall();
            plankLength = 5;
        }

        transform.localScale = new Vector3(plankLength, transform.localScale.y, transform.localScale.z); // TODO: maak built plank ook breder/minder breed
    }

    private void CreateInvisibleWall()
    {
        shownBuildable = buildTest.BuildableCursor;

        var x = (poles[0].position.x + poles[1].position.x) / 2;
        var z = (poles[0].position.z + poles[1].position.z) / 2;

        invisibleWallBetweenPoles = Instantiate(invisibleWallPrefab);
        invisibleWallBetweenPoles.transform.position = new Vector3(x, 0, z);
        invisibleWallBetweenPoles.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90, 90);

        invisibleWallIsActive = true;
    }

    private void DestroyInvisibleWall()
    {
        Destroy(invisibleWallBetweenPoles);
        invisibleWallIsActive = false;
    }
}
