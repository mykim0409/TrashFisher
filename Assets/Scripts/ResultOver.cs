using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
public class ResultOver : MonoBehaviour {

    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit() {
        Application.Quit();
    }
}
