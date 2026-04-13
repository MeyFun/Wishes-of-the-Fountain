using UnityEngine;

public class DeletePrefs : MonoBehaviour
{
    public void deletePrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrest очищен");
    }
}
