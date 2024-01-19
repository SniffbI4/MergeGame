using UnityEngine;

namespace Scripts.GamePlay
{
    public class MergeElement : MonoBehaviour
    {
        public int ID { get; private set; }
        
        public int Level;
        public Rigidbody2D RigidbodyComponent;
        public SpriteRenderer SpriteRendererComponent;

        private void Awake()
        {
            ID = GetInstanceID();
        }
    }
}