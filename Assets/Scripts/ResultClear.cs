using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultClear : MonoBehaviour {
    
    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void next() {
        string n = SceneManager.GetActiveScene().name;
        char a = n.ToCharArray()[n.Length-1];
        SceneManager.LoadScene("play0"+ (int.Parse(a.ToString()) + 1));
    }
}
