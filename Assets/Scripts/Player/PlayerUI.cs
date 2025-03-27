using Assets.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerUI : MonoBehaviour
    {
        public Image hpMask;
        private Player player;
        private void Start()
        {
            player = GetComponent<Player>();
        }
        private void Update()
        {
            Console.WriteLine(hpMask.fillAmount);
            hpMask.fillAmount = player.CurrentHealth / player.MaxHealth;
        }
    }
}