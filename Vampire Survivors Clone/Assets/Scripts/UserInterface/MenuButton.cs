using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartFirstLevel() {
        SceneManager.LoadSceneAsync("MainLevel");
    }
}
