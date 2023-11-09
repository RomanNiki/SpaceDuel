using UnityEngine;
using UnityEngine.UI;

namespace Engine.UI.Controls
{
    [RequireComponent(typeof(Image))]
    public class ControlsImageHolder : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Init(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}