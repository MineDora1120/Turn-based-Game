using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Stage1");
    }
}
