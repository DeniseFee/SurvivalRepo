using UnityEngine;

public class BuildBehavior : MonoBehaviour
{
    [SerializeField] private GameObject[] buildCursorPrefabs;
    [SerializeField] private GameObject[] buildableObjectPrefabs;

    public GameObject BuildCursor;
    [SerializeField] private GameObject buildableObject;

    private RaycastHit hit;
    private int selectedBuildable = 1;

    private float shownBuildableY;

    private bool shownBuildableIsShown = false;

    void Update()
    {
        HandleRaycast();
    }

    void HandleRaycast()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (shownBuildableIsShown)
            {
                BuildCursor.transform.position = new Vector3(hit.point.x, shownBuildableY, hit.point.z);
                BuildCursor.transform.eulerAngles = new Vector3(BuildCursor.transform.eulerAngles.x, transform.eulerAngles.y, BuildCursor.transform.eulerAngles.z);
            }
        }
    }

    void HandleSwitchingBuildables()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            if (selectedBuildable < buildableObjectPrefabs.Length - 1)
            {
                selectedBuildable += 1;
            }
        }
        else if (d < 0f)
        {
            if (selectedBuildable > 0)
            {
                selectedBuildable -= 1;
            }
        }
    }

    void ChangeBuildCursor()
    {
        Destroy(BuildCursor);

        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        BuildCursor = Instantiate(buildCursorPrefabs[selectedBuildable], hit.point, rotation);
        buildableObject = buildableObjectPrefabs[selectedBuildable];
    }
}
