using UnityEngine;

// public class Plate : MonoBehaviour
// {
//     public bool isActive = false;

//     public void Activate()
//     {
//         isActive = true;
//         GetComponent<Renderer>().material.color = Color.yellow; 
//     }
// }




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
        // plateManager.ActivatePlate();
        plateManager.ActivatePlate(index); // Передаем индекс
        Debug.Log("Rock is on the plate with index: " + index);
        // Debug.Log("Rock is on the plate");
    }

    public void Deactivate()
    {
        isActive = false;
        GetComponent<Renderer>().material.color = Color.white; 
        // plateManager.DeactivatePlate(); 
        plateManager.DeactivatePlate(index); // Передаем индекс
    }
}



// using UnityEngine;

// public class Plate : MonoBehaviour
// {
//     public bool isActive = false;
//     private PlateManager plateManager; // Ссылка на PlateManager
//     public int index; 

//     private void Start()
//     {
//         plateManager = FindObjectOfType<PlateManager>(); // Находим PlateManager на сцене
//     }

//     public void Activate()
//     {
//         if (!isActive) // Проверяем, активирована ли плита
//         {
//             isActive = true;
//             GetComponent<Renderer>().material.color = Color.yellow; 
//             plateManager.ActivatePlate(); // Уведомляем PlateManager об активации
//             Debug.Log("Rock is on the plate");
//         }
//     }

//     public void Deactivate()
//     {
//         if (isActive) // Проверяем, была ли плита активирована
//         {
//             isActive = false;
//             GetComponent<Renderer>().material.color = Color.white; 
//             plateManager.DeactivatePlate(); // Уведомляем PlateManager о деактивации
//         }
//     }
// }






















// using UnityEngine;

//    public class Plate : MonoBehaviour
//    {
//        // Ваши другие переменные и методы

//        public bool IsPlayerOrRockOnPlate(LayerMask playerMask, LayerMask rockMask)
//        {
//            // Проверяем, есть ли игрок или камень на плите
//            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 0.1f, playerMask);
//            Collider2D rockCollider = Physics2D.OverlapCircle(transform.position, 0.1f, rockMask);

//            // Отладочный вывод для проверки
//            if (playerCollider != null)
//            {
//                Debug.Log("Player is on the plate");
//            }
//            if (rockCollider != null)
//            {
//                Debug.Log("Rock is on the plate");
//            }

//            return playerCollider != null || rockCollider != null;
//        }

//        // Методы Activate и Deactivate
//        public void Activate()
//        {    GetComponent<Renderer>().material.color = Color.yellow; 
//            // Ваша логика активации плиты
//            Debug.Log("Plate activated");
//        }

//        public void Deactivate()
//        {
//            // Ваша логика деактивации плиты
//            Debug.Log("Plate deactivated");
//        }
//    }
