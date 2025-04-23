using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public float maxComboDelay = 1f;
    public static int hitNumber = 0;
    private Animator animator;
    private float lastClickedTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(3).normalizedTime > 0.7f)
        {
            if (animator.GetCurrentAnimatorStateInfo(3).IsName("First attack"))
            {
                animator.SetBool("Hit1", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(3).IsName("Second attack"))
            {
                animator.SetBool("Hit2", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(3).IsName("Final attack"))
            {
                animator.SetBool("Hit3", false);
                hitNumber = 0;
            }
        }
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            hitNumber = 0;
        }
    }

    public void Attack()
    {
        lastClickedTime = Time.time;
        hitNumber = Mathf.Clamp(++hitNumber, 0, 3);
        if (hitNumber == 1)
        {
            animator.SetBool("Hit1", true);
            animator.SetBool("Hit3", false);
            return;
        }
        if (hitNumber == 2 && animator.GetCurrentAnimatorStateInfo(3).normalizedTime > 0.3f)
        {
            animator.SetBool("Hit1", false);
            animator.SetBool("Hit2", true);
            return;
        }
        if (hitNumber == 3 && animator.GetCurrentAnimatorStateInfo(3).normalizedTime > 0.3f)
        {
            animator.SetBool("Hit2", false);
            animator.SetBool("Hit3", true);
            hitNumber = 0;
            return;
        }
    }
}
