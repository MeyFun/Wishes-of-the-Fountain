using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Выход из игры...");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}