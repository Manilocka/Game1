using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool isActive = false;

    public void Activate()
    {
        isActive = true;
        GetComponent<Renderer>().material.color = Color.yellow; 
    }
   
}