using UnityEngine;

public class QuitOnClick : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
            // If running in the Unity Editor, stop playing
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If built as an application, quit the application
            Application.Quit();
        #endif
    }
}
