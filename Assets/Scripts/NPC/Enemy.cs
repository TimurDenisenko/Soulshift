using System.Collections.Generic;
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
        [SerializeField] private SerializedDictionary<Item, int> lootTable;

        private Dictionary<Item, int> lootTableDict;

        private Player.Player player;
        private Animator playerAnimator;
        private Inventory playerInventory;

        private void Awake()
        {
            lootTableDict = lootTable.ToDictionary();
            player = GameObject.FindWithTag("Player").GetComponent<Player.Player>();
            playerAnimator = player.GetComponent<Animator>();
            playerInventory = player.GetComponent<Inventory>();
        }
        void Start()
        {
            CurrentHealth = maxHealth;
        }
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "AttackObject":
                    if (other.gameObject.GetComponentInParent<Player.Player>() is null)
                        return;
                    if (!playerAnimator.GetBool("Hit1") && !playerAnimator.GetBool("Hit2"))
                        return;
                    CurrentHealth -= player.AttackPower / Defense;
                    if (CurrentHealth < 1)
                        Death(other.gameObject);
                    return;
                default:
                    return;
            }
        }
        private void Death(GameObject player)
        {
            Destroy(gameObject);
            int rand = Random.Range(0, 101);
            foreach (KeyValuePair<Item, int> item in lootTableDict)
            {
                if (item.Value >= rand)
                    playerInventory.AddItem(item.Key);
            }
        }
    }
}
