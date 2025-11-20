using UnityEngine;

namespace Game_6
{
    public class AnimationManager : MonoBehaviour
    {
        public void EndAnimation()
        {
            transform.parent.GetComponent<Ingredient>().EndAnimation();
        }
    }
}