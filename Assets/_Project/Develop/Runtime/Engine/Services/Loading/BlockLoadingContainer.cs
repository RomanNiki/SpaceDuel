using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.Services.Loading
{
    public class BlockLoadingContainer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private BlockLoadingContainer _nextBlock;

        public void SetNextBlock(BlockLoadingContainer blockLoadingContainer) => _nextBlock = blockLoadingContainer;

        public void SetFill(float amount)
        {
            _image.fillAmount = amount;
            amount--;
            if (_nextBlock != null)
            {
                _nextBlock.SetFill(amount);
            }
        }
    }
}