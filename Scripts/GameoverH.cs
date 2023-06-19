using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverH : MonoBehaviour {
    private void Start() {
        StartCoroutine(TransitionCoroutine());
    }

    private System.Collections.IEnumerator TransitionCoroutine() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Gameover");
    }
}
