using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{
    public void StartDesktopGame()
    {
        SceneManager.LoadScene("BasketBalllDesktop");
    }

    public void StartVRGame()
    {
        SceneManager.LoadScene("VR_BasketBalll");
    }
}
