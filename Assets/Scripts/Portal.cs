using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Portal : MonoBehaviour
{
    public string goingSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        SceneManager.LoadScene(goingSceneName);
    }

}
