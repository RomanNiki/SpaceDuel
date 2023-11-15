using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI.Controls
{
    public class ControlsView : MonoBehaviour
    {
        [SerializeField] private RectTransform _blueControlsContainer;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private RectTransform _redControlsContainer;
        [SerializeField] private ControlsImageHolder _imageHolderPrefab;


        public void Init(ControlModel controlModel)
        {
            CreateImageHolder(controlModel.BluePlayerControl, _blueControlsContainer);
            CreateImageHolder(controlModel.RedPlayerControl, _redControlsContainer);
            _description.text = controlModel.Description;
        }

        private void CreateImageHolder(IEnumerable<Sprite> sprites, Transform container)
        {
            foreach (var sprite in sprites)
            {
                Instantiate(_imageHolderPrefab, container).Init(sprite);
            }
        }
    }
}