using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

namespace UIToolkit
{
    public class CanvasController : MonoBehaviour
    {
        private int numRemember;
        [SerializeField]
        private int rowGridSize;
        [SerializeField]
        private int colGridSize;
        [SerializeField]
        private GameObject[] panel;
        private Vector2 gridPosition = new Vector2(0, 0);
        bool isSelected = false;


        void Start()
        {
            numRemember = 1;
            //panel = GameObject.FindGameObjectsWithTag("Panel");
        }

        private void OnMove(InputValue input)
        {
            //Debug.Log(input.Get<Vector2>());
            if (input.Get<Vector2>() == new Vector2(1, 0))
            {
                int finalNum = rowGridSize * colGridSize;
                if (numRemember == finalNum)
                    numRemember = 1;
                else
                    numRemember++;
                Debug.Log(numRemember);
                Debug.Log(panel[numRemember - 1]);
                clickIDCard();
            }
            if (input.Get<Vector2>() == new Vector2(-1, 0))
            {
                if (numRemember == 1)
                    numRemember = rowGridSize * colGridSize;
                else
                    numRemember--;
                Debug.Log(numRemember);
            }
            if (input.Get<Vector2>() == new Vector2(0, 1))
            {
                if (numRemember <= rowGridSize)
                {
                    numRemember += rowGridSize * (colGridSize - 1);
                }
                else
                    numRemember -= rowGridSize;
                Debug.Log(numRemember);
            }
            if (input.Get<Vector2>() == new Vector2(0, -1))
            {
                if (numRemember > rowGridSize * (colGridSize - 1))
                    numRemember -= rowGridSize * (colGridSize - 1);
                else
                    numRemember += rowGridSize;
                Debug.Log(numRemember);
            }
        }
        public void clickIDCard()
        {
            RectTransform rectTran = panel[numRemember - 1].GetComponent<RectTransform>();
            if (isSelected == false)
            {
                rectTran.localScale = new Vector2(1.3f, 1.3f);
                isSelected = true;
            }
            else
            {
                Debug.Log(isSelected);
                rectTran.localScale = new Vector2(1, 1);
                isSelected = false;
            }

        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}
