// using UnityEngine;

// public class Plate : MonoBehaviour
// {
//     public bool isActive = false;

//     public void Activate()
//     {
//         isActive = true;
//         GetComponent<Renderer>().material.color = Color.yellow; 
//     }
// }
using UnityEngine;

   public class Plate : MonoBehaviour
   {
       // Ваши другие переменные и методы

       public bool IsPlayerOrRockOnPlate(LayerMask playerMask, LayerMask rockMask)
       {
           // Проверяем, есть ли игрок или камень на плите
           Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 0.1f, playerMask);
           Collider2D rockCollider = Physics2D.OverlapCircle(transform.position, 0.1f, rockMask);

           // Отладочный вывод для проверки
           if (playerCollider != null)
           {
               Debug.Log("Player is on the plate");
           }
           if (rockCollider != null)
           {
               Debug.Log("Rock is on the plate");
           }

           return playerCollider != null || rockCollider != null;
       }

       // Методы Activate и Deactivate
       public void Activate()
       {
           // Ваша логика активации плиты
           Debug.Log("Plate activated");
       }

       public void Deactivate()
       {
           // Ваша логика деактивации плиты
           Debug.Log("Plate deactivated");
       }
   }
   