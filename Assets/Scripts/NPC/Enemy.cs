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
    }
}
