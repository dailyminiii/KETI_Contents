using System.Collections;
using UnityEngine;

public class MenuObject : MonoBehaviour
{

    [SerializeField]
    float scrollSpeed = 5f; // 스크롤 속도
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(LeftSwipeDelay());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(RightSwipeDelay());
        }
    }

    IEnumerator LeftSwipeDelay()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.left * scrollSpeed);

    }
    IEnumerator RightSwipeDelay()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.right * scrollSpeed);

    }
    // MenuObject[] HorizontalSwipeMenu;

}
