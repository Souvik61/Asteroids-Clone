using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // Application.Quit();
        }
    }

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
