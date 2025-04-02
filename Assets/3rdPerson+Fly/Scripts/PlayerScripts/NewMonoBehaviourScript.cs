using UnityEngine;

public abstract class GenericBehaviour : MonoBehaviour
{
    protected int speedFloat;
    protected BasicBehaviour behaviourManager;
    protected int behaviourCode;
    protected bool canSprint;

    void Awake()
    {
        behaviourManager = GetComponent<BasicBehaviour>();
        speedFloat = Animator.StringToHash("Speed");
        canSprint = true;
        behaviourCode = this.GetType().GetHashCode();
    }

    public virtual void LocalFixedUpdate() { }
    public virtual void LocalLateUpdate() { }
    public virtual void OnOverride() { }

    public int GetBehaviourCode()
    {
        return behaviourCode;
    }

    public bool AllowSprint()
    {
        return canSprint;
    }
}
