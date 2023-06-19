using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Studentmove : MonoBehaviour {
    private bool collisionOccurred = false; // 충돌 여부를 나타내는 변수

    private float speed = 15f;
    private float moveRange = 4f;

    // 캐릭터 충돌 범위를 설정할 변수
    public float collisionBoxWidth = 1f;
    public float collisionBoxHeight = 1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Respawn") {
            Time.timeScale = 0f;
            StartCoroutine(GameoverTransition());
        }
    }

    private IEnumerator GameoverTransition() {
        SceneManager.LoadScene("GameoverH");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3f);
    }


    private void Start() {
    }

    private void Update() {
        Time.timeScale = 1f;
        float playerMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            playerMove = -speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            playerMove = speed * Time.deltaTime;
        }

        float targetXPosition = transform.position.x + playerMove;
        targetXPosition = Mathf.Clamp(targetXPosition, -moveRange, moveRange);

        transform.position = new Vector3(targetXPosition, transform.position.y, transform.position.z);
    }
}
