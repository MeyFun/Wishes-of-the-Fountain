using UnityEngine;

public class DeletePrefs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void deletePrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrest очищен");
    }
}
