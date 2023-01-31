using TMPro;
using UnityEngine;

namespace Views
{
    public class PrepareScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;

        public void SetText(string text)
        {
            _tmpText.text = text;
        }
    }
}