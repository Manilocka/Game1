using UnityEngine;

public class Plate : MonoBehaviour
{
    public bool isActive = false;

    public void Activate()
    {
        isActive = true;
        GetComponent<Renderer>().material.color = Color.yellow; 
    }
}