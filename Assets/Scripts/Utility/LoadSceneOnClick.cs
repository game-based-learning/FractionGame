using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadSceneOnClick : MonoBehaviour
{
    public int  sceneNameToLoad; 

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
