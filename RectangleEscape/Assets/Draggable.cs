using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Draggable : MonoBehaviour
{
    public bool isVertical, moving;

    private Rigidbody2D rb;
    private float startPosX, startPosY;
    public float speed = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        rb.velocity = new Vector2(0,0);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (isVertical)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, new Vector3(this.gameObject.transform.position.x, mousePos.y - startPosY, this.gameObject.transform.position.z), Time.deltaTime * speed));
            } else
            {
                rb.MovePosition(Vector3.Lerp(transform.position, new Vector3(mousePos.x - startPosX, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Time.deltaTime * speed));
            }

            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
