using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    public GameObject Trunk;
    public GameObject Root;

    private Rigidbody trunkRigidBody;

    private float pushIntensity = 10f;

    void Start()
    {
        trunkRigidBody = Trunk.GetComponent<Rigidbody>();
    }

    public void CutTree()
    {
        trunkRigidBody.isKinematic = false;
        trunkRigidBody.AddForce(-transform.forward * pushIntensity); //TODO: maak dit afhankelijk van waar de speler staat
    }
}
