using UnityEngine;

public class Plate : MonoBehaviour
{
    public bool isActive = false;

    public void Activate()
    {
        if (!isActive) // Проверяем, активирована ли плита ранее
        {
            isActive = true;
            GetComponent<Renderer>().material.color = Color.yellow; // Меняем цвет на желтый
            
        }
    }
}