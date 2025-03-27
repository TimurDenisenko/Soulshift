using Assets.Scripts.NPC;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IEntity
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

        private void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "AttackObject":
                    CurrentHealth -= collision.gameObject.GetComponentInParent<Enemy>().AttackPower / Defense;
                    return;
                default:
                    return;
            }
        }
    }
}
