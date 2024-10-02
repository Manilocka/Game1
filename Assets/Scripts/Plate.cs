using UnityEngine;


public class Plate : MonoBehaviour
{
    public bool isActive = false;
    private PlateManager plateManager;
    public int index; 

    private void Start()
    {
        plateManager = FindObjectOfType<PlateManager>(); 
    }

    public void Activate()
    {
        isActive = true;
        GetComponent<Renderer>().material.color = Color.yellow; 
        plateManager.ActivatePlate(index); 
        Debug.Log("Rock is on the plate with index: " + index);
    }

    public void Deactivate()
    {
        isActive = false;
        GetComponent<Renderer>().material.color = Color.white; 
        plateManager.DeactivatePlate(index);
    }
}

