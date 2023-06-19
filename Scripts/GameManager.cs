using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Teacher[] teachers; // 생성된 캐릭터 배열

    public GameObject teacherPrefab;
    public GameObject gameOverDisplayPrefab;

    private bool gameEnded = false;
    private GameObject gameOverDisplay;

    public Sprite[] teacherSprites; // 캐릭터의 스프라이트 프레임 배열
    public float frameRate = 0.1f; // 애니메이션의 프레임 속도

    private int teacherCount = 2;

    private void Start() {
        string[] animeNames = { "t_eunjoo_0", "t_hyang_0", "t_jiwoo_0", "t_kyujung_0", "t_gahyun_0" };

        teachers = new Teacher[teacherCount]; // 생성된 캐릭터 배열 초기화

        for (int num = 0; num < teacherCount; num++)
        {
            int randomNum = Random.Range(1, 6); // 1 이상 4 미만의 랜덤한 숫자 생성
            GameObject myInstance = Instantiate(teacherPrefab);
            Teacher t = myInstance.GetComponent<Teacher>();
            t.Init(animeNames[randomNum - 1]);
            t.tag = "Respawn";

            // 랜덤하게 Teacher 포지션 지정 (x값, y값)
            float randomX = UnityEngine.Random.Range(-3.5f, 3.5f);
            float randomY = UnityEngine.Random.Range(5, 9);
            randomY *= num + 1;
            randomY -= num;
            myInstance.transform.position = new Vector3(randomX, randomY, 0);

            teachers[num] = myInstance.GetComponent<Teacher>(); // 생성된 캐릭터를 배열에 저장
        }

    }


    private void Update() 
    {   //선생님들 모두 피하면 다음 스테이지로 이동
        Scene scene = SceneManager.GetActiveScene();
        if(teachers[teacherCount-1].transform.position.y <= -5.8f){
            if(scene.name == "GameStart"){
                SceneManager.LoadScene("Stage2");
            }
            else if(scene.name == "Stage2"){
                SceneManager.LoadScene("Stage3");
            }
            else if(scene.name == "Stage3"){
                // SceneManager.LoadScene("Stairs");
                StartCoroutine(TransitionToGameover());
            }
        }

        if (!gameEnded)
        {
            if (IsCollisionDetected())
            {
                EndGame();
            }
        }
    }

    private IEnumerator TransitionToGameover()
    {
        SceneManager.LoadScene("Stairs");
        yield return new WaitForSeconds(3f);
    }

    private bool IsCollisionDetected()
    {
        // 충돌 감지 로직
        return false;
    }

    public void EndGame()
    {
        gameEnded = true;
        Debug.Log("게임 종료");
    }

    private System.Collections.IEnumerator PlayAnimation(SpriteRenderer spriteRenderer)
    {
        int frameIndex = 0;
        while (true)
        {
            spriteRenderer.sprite = teacherSprites[frameIndex];
            frameIndex = (frameIndex + 1) % teacherSprites.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
    
}


