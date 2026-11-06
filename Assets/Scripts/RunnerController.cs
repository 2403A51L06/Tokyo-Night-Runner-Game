using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    public float laneDistance = 2.5f;
    public float laneChangeSpeed = 12f;
    public float forwardSpeed = 9f;
    public float jumpHeight = 2.2f;
    public float gravity = -22f;
    public float slideDuration = 0.75f;

    private CharacterController controller;
    private int lane = 1;
    private float verticalVelocity;
    private float slideTimer;
    private float normalHeight;
    private Vector3 normalCenter;
    private bool isDead;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        normalHeight = controller.height;
        normalCenter = controller.center;
    }

    void Update()
    {
        if (isDead || GameManager.Instance == null || !GameManager.Instance.IsPlaying) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) lane = Mathf.Max(0, lane - 1);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) lane = Mathf.Min(2, lane + 1);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            StartSlide();

        if (slideTimer > 0)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                controller.height = normalHeight;
                controller.center = normalCenter;
            }
        }

        if (controller.isGrounded && verticalVelocity < 0) verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        float targetX = (lane - 1) * laneDistance;
        float x = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        Vector3 move = new Vector3(x - transform.position.x, verticalVelocity * Time.deltaTime, forwardSpeed * Time.deltaTime);
        controller.Move(move);
    }

    void StartSlide()
    {
        slideTimer = slideDuration;
        controller.height = normalHeight * 0.5f;
        controller.center = new Vector3(normalCenter.x, normalCenter.y * 0.5f, normalCenter.z);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        GameManager.Instance.EndGame();
    }
}
