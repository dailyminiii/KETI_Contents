using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace pj.UI {
    public class GridElement: MonoBehaviour {
        [SerializeField] UnityEvent selected;
        [SerializeField] UnityEvent deselected;
        public void Selected() {
            selected.Invoke();
        }
        public void Deselected() {
            deselected.Invoke();
        }
    }

}
