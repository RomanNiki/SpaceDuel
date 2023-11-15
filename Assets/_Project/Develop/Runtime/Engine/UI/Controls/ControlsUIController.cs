using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI.Controls
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
}