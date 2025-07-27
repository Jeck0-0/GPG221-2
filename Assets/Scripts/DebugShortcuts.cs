using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugShortcuts : MonoBehaviour
{
    void Update()
    {
        //restart scene [CTRL + SHIFT + R]
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
            FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).ForEach(x => x.weapon = null);
        
    }
}
