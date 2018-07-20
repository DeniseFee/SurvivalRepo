using UnityEngine;

public class BuildTest : MonoBehaviour
{
    [SerializeField] private GameObject[] ShownBuildables;
    [SerializeField] private GameObject ShownBuildable;
    [SerializeField] private GameObject[] ThingsToBuild;
    [SerializeField] private GameObject ThingToBuild;

    public float HeightModifier;

    [SerializeField] private int buildableInt;
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
            ThingToBuild = ThingsToBuild[buildableInt];
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
            Destroy(ShownBuildable);
            ThingToBuild = null;
            shownBuildableIsShown = false;
        }
    }

    void HandleRaycast()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (shownBuildableIsShown)
            {
                ShownBuildable.transform.position = new Vector3(hit.point.x, hit.point.y + HeightModifier, hit.point.z);
                ShownBuildable.transform.eulerAngles = new Vector3(ShownBuildable.transform.eulerAngles.x, transform.eulerAngles.y, ShownBuildable.transform.eulerAngles.z);
            }
        }
    }

    void HandleBuildingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(ThingToBuild, ShownBuildable.transform.position, ShownBuildable.transform.rotation);
        }
    }

    void HandleSwitchingBuildables()
    {
        HeightModifier = ShownBuildable.transform.localScale.y / 2;

        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            if (buildableInt < ThingsToBuild.Length - 1)
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
        Destroy(ShownBuildable);

        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0); // TODO: correct angles
        ShownBuildable = Instantiate(ShownBuildables[buildableInt], hit.point, rotation);
        ThingToBuild = ThingsToBuild[buildableInt];
    }
}
