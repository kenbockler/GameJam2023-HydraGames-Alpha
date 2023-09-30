using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementsTopDown : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Transform DragCheck;
    public LayerMask Objects;
    private bool isDraggable;
    private bool isDragging = false; // Флаг, указывающий, тащит ли игрок объект
    public GameObject objectToDrag; // Объект, который игрок тащит
    private Vector3 offset; // Смещение между позицией игрока и позицией объекта
    private bool isFacingRight = true;
    private bool isFacingTop = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moving X axis
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(dirX * 7f, rigidBody.velocity.y);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, dirY * 7f);
        Flip();

        // Draging
        // Проверяем, нажата ли кнопка мыши (или касание на мобильных устройствах)
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Button pressed");
            CheckForDraggableObject();
        }

        // Если игрок уже начал таскать объект, обновляем его позицию
        if (isDragging)
        {
            Vector3 newPosition = transform.position + offset;
            objectToDrag.transform.position = new Vector3(newPosition.x, newPosition.y, objectToDrag.transform.position.z);
        }

        // Проверяем, была ли отпущена кнопка мыши (или касание)
        if (Input.GetButtonUp("Fire1") && isDragging)
        {
            StopDragging();
        }
        
    }

    private void Flip()
    {
        if (!isDragging)
        {
            if (rigidBody.velocity.x < 0f)
            {
                isFacingTop = false;
                isFacingRight = false;
                Quaternion localRotation = transform.localRotation;
                transform.rotation = Quaternion.Euler(localRotation.x, localRotation.y, 180);
            }
            else if(rigidBody.velocity.x > 0f)
            {
                isFacingTop = false;
                isFacingRight = true;
                Quaternion localRotation = transform.localRotation;
                transform.rotation = Quaternion.Euler(localRotation.x, localRotation.y, 0);
            }

            if (rigidBody.velocity.y < 0f)
            {
                isFacingRight = false;
                isFacingTop = false;
                Quaternion localRotation = transform.localRotation;
                transform.rotation = Quaternion.Euler(localRotation.x, localRotation.y, 270);
            }
            if (rigidBody.velocity.y > 0f)
            {
                isFacingRight = false;
                isFacingTop = true;
                Quaternion localRotation = transform.localRotation;
                transform.rotation = Quaternion.Euler(localRotation.x, localRotation.y, 90);
            }
        }
    }

    // Метод для начала таскания объекта
    private void StartDragging(GameObject obj)
    {
        isDragging = true;
        objectToDrag = obj;
        offset = objectToDrag.transform.position - transform.position;
    }

    // Метод для окончания таскания объекта
    private void StopDragging()
    {
        isDragging = false;
        objectToDrag = null;
    }

    // Проверка на соприкосновение с объектами с тегом "drag"
    private void CheckForDraggableObject()
    {
        Vector2 localScale = transform.localScale;
        RaycastHit2D hit;

        if (isFacingRight)
        {
            Debug.LogWarning("Right");
            hit = Physics2D.Raycast(transform.position, Vector2.right * localScale.x, 2, LayerMask.GetMask("Objects"));
        }
        else if (!isFacingRight)
        {
            Debug.LogWarning("Left");
            hit = Physics2D.Raycast(transform.position, Vector2.left * localScale.x, 2, LayerMask.GetMask("Objects"));

        }
        else if (isFacingTop)
        {
            Debug.LogWarning("Up");
            hit = Physics2D.Raycast(transform.position, Vector2.up * localScale.y, 5, LayerMask.GetMask("Objects"));
        }
        else if (!isFacingTop)
        {
            Debug.LogWarning("Down");
            hit = Physics2D.Raycast(transform.position, Vector2.down * localScale.y, 5, LayerMask.GetMask("Objects"));
        }
        else {
            hit = Physics2D.Raycast(transform.position, Vector2.right * localScale.x, 2, LayerMask.GetMask("Objects"));
        }

        if (hit.collider != null && hit.collider.CompareTag("drag"))
        {
            StartDragging(hit.collider.gameObject);
        }
    }
}
