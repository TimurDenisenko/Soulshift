using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.NPC
{
    public class NPCAnimationEvent : MonoBehaviour
    {
        public GameObject InteractionObject;
        public GameObject InteractionObjectAnimation;
        public float InteractionObjectSpeed;
        private float initialZCoordinate;

        void Update()
        {
            if (InteractionObject.activeSelf)
                ChangeZCoordinate(InteractionObjectSpeed, true);
            else
                ChangeZCoordinate(initialZCoordinate);
        }

        public void StartSingleMagicAttack()
        {
            InteractionObject.SetActive(true);
            InteractionObjectAnimation.SetActive(true);
            initialZCoordinate = InteractionObject.transform.localPosition.z;
        }

        public void EndSingleMagicAttack()
        {
            InteractionObject.SetActive(false);
            InteractionObjectAnimation.SetActive(false);
        }
        
        private void ChangeZCoordinate(float z, bool isAddition = false)
        {
            Vector3 pos = InteractionObject.transform.localPosition;
            if (isAddition)
                pos.z += z;
            else
                pos.z = z;
            InteractionObject.transform.localPosition = pos;
        }
    }
}
