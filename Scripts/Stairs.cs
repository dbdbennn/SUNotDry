using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour {
    private void Start() {
        StartCoroutine(TransitionCoroutine());
    }

    private System.Collections.IEnumerator TransitionCoroutine() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameClear");
    }
}
