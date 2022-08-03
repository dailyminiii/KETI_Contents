using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace pj.UI
{
    public class ImageModifier : MonoBehaviour
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color deselectedColor;
        [SerializeField] private float changeDuration;
        private RectTransform rect;
        private Image image;
        private void Awake()
        {
            image = GetComponent<Image>();
            ChangeColorToDeselected();
            rect = GetComponent<RectTransform>();
            ChangeSizeToDeselected();
        }
        public void ChangeColorToSelected()
        {
            StartCoroutine(ChangeColor(selectedColor));
        }
        public void ChangeColorToDeselected()
        {
            StartCoroutine(ChangeColor(deselectedColor));

        }
        IEnumerator ChangeColor(Color color)
        {
            yield return new WaitForSeconds(changeDuration);
            image.color = color;
        }
        public void ChangeSizeToSelected()
        {
            StartCoroutine(ChangeSizeSel());
        }
        public void ChangeSizeToDeselected()
        {
            StartCoroutine(ChangeSizeDesel());
        }
        IEnumerator ChangeSizeSel()
        {
            yield return new WaitForSeconds(changeDuration);
            rect.localScale = new Vector2(0.7f, 0.7f);
        }
        IEnumerator ChangeSizeDesel()
        {
            yield return new WaitForSeconds(changeDuration);
            rect.localScale = new Vector2(0.5f, 0.5f);
        }
    }


}
