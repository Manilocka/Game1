
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class PlateManager : MonoBehaviour
{
    private HashSet<int> activePlatesIndices = new HashSet<int>();

    public void ActivatePlate(int index)
    {
        activePlatesIndices.Add(index); 
        CheckForSceneChange();
    }

    public void DeactivatePlate(int index)
    {
        activePlatesIndices.Remove(index); 
    }

    private void CheckForSceneChange()
    {
        if (activePlatesIndices.Count >= 3) 
        {
            SceneManager.LoadScene("level2");
        }
    }
}