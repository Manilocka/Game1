// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PlateManager : MonoBehaviour
// {
//     private int activePlatesCount = 0;

//     public void ActivatePlate()
//     {
//         activePlatesCount++;
//         CheckForSceneChange();
//     }

//     public void DeactivatePlate()
//     {
//         activePlatesCount--;
//     }

//     private void CheckForSceneChange()
//     {
//         if (activePlatesCount >= 3) // Если активировано 3 плиты
//         {
//             SceneManager.LoadScene("level2"); // Замените "NextScene" на имя вашей следующей сцены
//         }
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // Добавьте это, чтобы использовать HashSet.

public class PlateManager : MonoBehaviour
{
    private HashSet<int> activePlatesIndices = new HashSet<int>(); // Используем HashSet для уникальных индексов

    public void ActivatePlate(int index)
    {
        activePlatesIndices.Add(index); // Добавляем индекс в набор активированных плит
        CheckForSceneChange();
    }

    public void DeactivatePlate(int index)
    {
        activePlatesIndices.Remove(index); // Убираем индекс из активированных плит
    }

    private void CheckForSceneChange()
    {
        if (activePlatesIndices.Count >= 3) // Если активировано 3 или более плиты
        {
            SceneManager.LoadScene("level2"); // Замените на имя вашей следующей сцены
        }
    }
}