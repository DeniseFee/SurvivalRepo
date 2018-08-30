using UnityEngine;

public class BuildTest : MonoBehaviour
{
    [SerializeField] private GameObject[] BuildablePrefabs;
    [SerializeField] private GameObject[] BuiltPrefabs;

    [SerializeField] private GameObject BuiltObject;
    public GameObject BuildableCursor;

    public float HeightModifier;
    public bool isOnPoles;

    private float shownBuildableY;
    private int buildableInt;
    private bool buildModeOn = false;
    private bool shownBuildableIsShown = false;
    private RaycastHit hit;

    private void Start()
    {
        buildableInt = 0;
    }

    void Update()
    {
        HandleRaycast();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            buildModeOn = !buildModeOn;
        }

        if (buildModeOn)
        {
            BuiltObject = BuiltPrefabs[buildableInt];
            if (!shownBuildableIsShown)
            {
                SwitchBuildable();
                shownBuildableIsShown = true;
            }

            HandleBuildingInput();
            HandleSwitchingBuildables();
        }
        else if (!buildModeOn)
        {
            Destroy(BuildableCursor);
            BuiltObject = null;
            shownBuildableIsShown = false;
        }
    }

    void HandleRaycast()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (shownBuildableIsShown)
            {
                BuildableCursor.transform.position = new Vector3(hit.point.x, shownBuildableY, hit.point.z);
                BuildableCursor.transform.eulerAngles = new Vector3(BuildableCursor.transform.eulerAngles.x, transform.eulerAngles.y, BuildableCursor.transform.eulerAngles.z);
            }
        }
    }

    void HandleBuildingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BuiltObject, BuildableCursor.transform.position, BuildableCursor.transform.rotation);
        }
    }

    void HandleSwitchingBuildables()
    {
        if (!isOnPoles) // TODO: polish this shizzle
        {
            HeightModifier = BuildableCursor.transform.localScale.y / 2;
            shownBuildableY = hit.point.y + HeightModifier;
        }
        else if (isOnPoles)
        {
            //shownBuildableY = 
        }

        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            if (buildableInt < BuiltPrefabs.Length - 1)
            {
                buildableInt += 1;
                SwitchBuildable();
            }
        }
        else if (d < 0f)
        {
            if (buildableInt > 0)
            {
                buildableInt -= 1;
                SwitchBuildable();
            }
        }
    }

    void SwitchBuildable()
    {
        Destroy(BuildableCursor);

        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        BuildableCursor = Instantiate(BuildablePrefabs[buildableInt], hit.point, rotation);
        BuiltObject = BuiltPrefabs[buildableInt];
    }
}
