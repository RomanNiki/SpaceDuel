using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engine.UI.Controls
{
    public class ControlsUIController : MonoBehaviour
    {
        [SerializeField] private ControlModel[] _controls;
        [SerializeField] private ControlsView _controlsViewPrefab;
        private readonly List<ControlsView> _instantiatedControls = new();
        
        private void OnEnable()
        {
            foreach (var controlData in _controls)
            {
                var item = Instantiate(_controlsViewPrefab, transform);
                item.Init(controlData);
                _instantiatedControls.Add(item);
            }
        }

        private void OnDisable()
        {
            foreach (var control in _instantiatedControls)
            {
                Destroy(control);
            }
        }
    }

    [Serializable]
    public class ControlModel
    {
        public List<Sprite> BluePlayerControl;
        public string Description;
        public List<Sprite> RedPlayerControl;
    }
}