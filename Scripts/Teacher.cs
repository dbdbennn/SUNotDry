using UnityEngine;
using UnityEngine.SceneManagement;

public class Teacher : MonoBehaviour
{
    // Teacher 속도
    private float speed = 3f;
    private float dspeed = 2.5f;
    private bool isMoving = true;

    private SpriteRenderer sprite;
    private string animatorName;

    private Animator animator;

    public void Init(string animatorName)
    {
        this.animatorName = animatorName;
    }

    private void Start()
    {
        // sprite는 있어야함.
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 1;
        // sprite 이미지는 애니메이션 적용하며 없어지기에 그냥 임의로 작성해둠.
        Sprite sp = Resources.Load<Sprite>("Sprites/t_eunjoo");
        sprite.sprite = sp;

        animator = GetComponent<Animator>(); // 이미 Animator 컴포넌트가 추가되어 있으므로 GetComponent로 가져옵니다.
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }
        animator.runtimeAnimatorController = GetAnimatorController(animatorName);
    }

    // 애니메이션 적용 함수
    private RuntimeAnimatorController GetAnimatorController(string animatorName)
    {
        string controllerPath = "Animations/Controllers/" + animatorName;
        return Resources.Load<RuntimeAnimatorController>(controllerPath);
    }

    void Update()
    {
        if (isMoving)
        {
            Scene scene = SceneManager.GetActiveScene();

            if(scene.name == "GameStart"){
                dspeed = 2.5f;
                speed = 3f;
            }
            else if(scene.name == "Stage2"){
                dspeed = 3f;
                speed = 3.5f;
            }
            else if(scene.name == "Stage3"){
                dspeed = 4f;
                speed = 5f;
            }
            

            // 캐릭터 앞으로 이동
            transform.Translate(Vector3.down * dspeed * Time.deltaTime); // 아래쪽으로 dspeed만큼

            // 캐릭터 좌우 이동
            transform.Translate(Vector3.right * speed * Time.deltaTime); // 오른쪽으로 speed만큼
            if (transform.position.x >= 3.5f) // 오른쪽 끝에 도달
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); // 좌표를 180도 회전시켜 오브젝트의 좌우가 바뀌도록
            }
            else if (transform.position.x <= -3.5f) // 왼쪽 끝에 도달
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            if (transform.position.y <= -10f) // 화면에서 보이지 않으면 정지
            {
                isMoving = false;
            }
        }
    }

    private System.Collections.IEnumerator PlayAnimation()
    {
        int frameIndex = 0;
        while (true)
        {
            animator.Play("Animation" + frameIndex); // 애니메이션 이름을 숫자로 변경해야 합니다. 예: Animation0, Animation1, Animation2, ...
            frameIndex = (frameIndex + 1) % 3; // 애니메이션 프레임 수에 맞게 변경해야 합니다.
            yield return new WaitForSeconds(0.1f); // 프레임 속도에 맞게 변경해야 합니다.
        }
    }
}
