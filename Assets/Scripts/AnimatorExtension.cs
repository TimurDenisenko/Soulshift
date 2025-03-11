using UnityEngine;

public static class AnimatorExtension
{
    public static string state;
    public static void PlayAnimation(this Animator a, string animationName, bool isNeedState = false, int layer = 0, float fade = 0.2f)
    {
        animationName = (isNeedState ? state : "") + animationName;
        int animationHash = Animator.StringToHash(animationName);
        a.CrossFade(animationHash, fade, layer);
    }
    public static void StopAnimation(this Animator a, bool isNeedState = false, int layer = 0)
    {
        a.CrossFade((isNeedState ? state : "") + "Idle", 0.2f, layer);
    }
}