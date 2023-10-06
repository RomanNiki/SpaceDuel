using UnityEngine;

namespace Engine.Services.Loading
{
    [ExecuteAlways]
    public class BlockLoadingSlider : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float _sliderValue;

        [SerializeField] private BlockLoadingContainer[] _blocks;
        private BlockLoadingContainer _currentBlock;
        public float Value { get; private set; }

        private void OnValidate()
        {
            InitBlocks();
            ChangeValue(_sliderValue);
        }

        private void Awake()
        {
            InitBlocks();
        }

        private void InitBlocks()
        {
            _currentBlock = _blocks[0];
            for (var i = 1; i < _blocks.Length; i++)
            {
                _currentBlock.SetNextBlock(_blocks[i]);
                _currentBlock = _blocks[i];
            }

            _currentBlock = _blocks[0];
        }

        public void ChangeValue(float value)
        {
            Value = value;
            _currentBlock.SetFill(value*_blocks.Length);
        }
    }
}