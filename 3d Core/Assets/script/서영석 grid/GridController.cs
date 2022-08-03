using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace pj.UI {
    public class GridController: MonoBehaviour {
        private class DeadZoneHandler {
            const float DEFAULT_DEAD_ZONE = 0.2f;
            private bool isInputConcerned;
            private float inputDeadZone;
            public DeadZoneHandler() {
                SetDeadZone(DEFAULT_DEAD_ZONE);
            }
            public DeadZoneHandler(float deadZone) {
                SetDeadZone(deadZone);
            }
            private void SetDeadZone(float deadZone) {
                inputDeadZone = deadZone;
            }
            public int GetMoveDirection(float input) {
                int moveDireciton = 0;
                float inputAbs = Mathf.Abs(input);
                float inputSign = Mathf.Sign(input);
                if (inputAbs < inputDeadZone) {
                    isInputConcerned = false;
                    moveDireciton = 0;
                } else if (!isInputConcerned) {
                    isInputConcerned = true;
                    moveDireciton = (int)inputSign;
                } else {
                    //Do nothing.
                }
                return moveDireciton;
            }
        }
        [SerializeField] private Vector2Int gridSize = new Vector2Int();
        private Vector2Int currentSelectedGrid = new Vector2Int();
        private DeadZoneHandler xAxisDeadZoneHandler = new DeadZoneHandler();
        private DeadZoneHandler yAxisDeadZoneHandler = new DeadZoneHandler();
        private GameObject currentSelectedChild;
        private void Start() {
            GameObject firstSelected = GetChildOfGridPoint(currentSelectedGrid);
            Select(firstSelected);
        }
        void OnMove(InputValue input) {
            Vector2Int moveDirection = new Vector2Int();
            Vector2 inputVector = input.Get<Vector2>();
            inputVector.y = -inputVector.y;
            moveDirection.x = xAxisDeadZoneHandler.GetMoveDirection(inputVector.x);
            moveDirection.y = yAxisDeadZoneHandler.GetMoveDirection(inputVector.y);
            ChangeGridPositionBy(moveDirection);
        }//Called by unity player input class
        private void ChangeGridPositionBy(Vector2Int direction) {
            currentSelectedGrid += direction;
            currentSelectedGrid += gridSize;
            currentSelectedGrid.x %= gridSize.x;
            currentSelectedGrid.y %= gridSize.y;
            GameObject selectedGameObject = GetChildOfGridPoint(currentSelectedGrid);
            Select(selectedGameObject);
        }
        private void Select(GameObject selectedChild) {
            if(currentSelectedChild != null) {
                GridElement deselectedGridElement = currentSelectedChild.GetComponent<GridElement>();
                deselectedGridElement.Deselected();
            }
            currentSelectedChild = selectedChild;
            GridElement selectedGridElement = currentSelectedChild.GetComponent<GridElement>();
            selectedGridElement.Selected();

        }
        private GameObject GetChildOfGridPoint(Vector2Int gridPoint) {
            return transform.GetChild(GridToInt(currentSelectedGrid)).gameObject;
        }
        private int GridToInt(Vector2Int gridPoint) {
            return gridPoint.x + gridPoint.y * gridSize.x;
        }
        private Vector2Int IntToGrid(int elementNumber) {
            Vector2Int returnVector = new Vector2Int(elementNumber % gridSize.x, elementNumber / gridSize.x);
            return returnVector;
        }

    }


}