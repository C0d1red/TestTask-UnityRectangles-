using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(RectangleLink))]
public class RectangleController : MonoBehaviour
{
    [SerializeField]
    private float doubleClickDelay = 0.2f;
    [SerializeField]
    private float draggingSpeed = 10f;

    // Time when first click was produced
    private float firstClickTime;

    private bool isCoroutineAllowed;
    private int clickCounter;
    private bool isDragging;

    private Rigidbody2D rigidbody;
    private RectangleLink rectangleLink;

    private void Start()
    {
        rectangleLink = GetComponent<RectangleLink>();
        rigidbody = GetComponent<Rigidbody2D>();
        ResetValues();
    }


    private void OnMouseDown()
    {
        clickCounter++;

        // If one click was produced and coroutine is allowed
        if (clickCounter == 1 && isCoroutineAllowed)
        {
            // Store time when first click was produced and start coroutine fot cjecking double click
            firstClickTime = Time.time;
            StartCoroutine(CheckingDoubleClick());
        }
    }


    private void OnMouseDrag()
    {
        isDragging = true;
        
        // Freeze rotation for static behavior of rectangle
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Getting mouse position
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        // Using rigidbody.velocity for detection of collision
        rigidbody.velocity = (currentPos - (Vector2)transform.position) * draggingSpeed;
    }


    private void OnMouseUp()
    {
        // Return all constraints to default
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        isDragging = false;
    }


    private IEnumerator CheckingDoubleClick()
    {
        // Stop future repitings
        isCoroutineAllowed = false;
        while (Time.time < firstClickTime + doubleClickDelay)
        {
            if (clickCounter == 2)
            {
                // Doble click was produced
                // Destroy rectangle
                Destroy(gameObject);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        // Reset all walues to the start
        ResetValues();
    }


    private void ResetValues()
    {
        isCoroutineAllowed = true;
        firstClickTime = 0;
        clickCounter = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // isDragging: only one (dragging) rectangle create link
        if (isDragging)
        {
            // Switch link (add or remove)
            rectangleLink.SwitchLink(collision.gameObject);
        }
    }


    private void OnDestroy()
    {
        rectangleLink.DestroyAllLines();
    }

}
