using UnityEngine;

namespace Assets.Scripts.NPC
{
    public class Enemy : MonoBehaviour, IEntity
    {
        public float MaxHealth => maxHealth;
        public float CurrentHealth { get; private set; }
        public float Defense => defense;
        public float AttackPower => attackPower;

        [SerializeField] private float maxHealth;
        [SerializeField] private float defense;
        [SerializeField] private float attackPower;
        void Start()
        {
            CurrentHealth = maxHealth;
        }
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "AttackObject":
                    Animator playerAnimator = other.gameObject.GetComponentInParent<Animator>();
                    if (!playerAnimator.GetBool("Hit1") && !playerAnimator.GetBool("Hit2"))
                        return;
                    CurrentHealth -= other.gameObject.GetComponentInParent<Player.Player>().AttackPower / Defense;
                    if (CurrentHealth < 1)
                        Destroy(gameObject);
                    return;
                default:
                    return;
            }
        }
    }
}
