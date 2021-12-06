using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_body;
    private Animator m_animator;
    private SpriteRenderer m_sprite;

    [Range(0, 10)]
    public float speed = 1f;

    void Awake() {
        m_body = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        float transX = Input.GetAxisRaw("Horizontal");
        float transY = Input.GetAxisRaw("Vertical");

        Vector2 translation = new Vector2(transX, transY);

        m_body.velocity = translation * speed;

        if(transX != 0)
        {
            m_sprite.flipX = transX < 0;
        }

        m_animator.SetFloat("transX", transX);
        m_animator.SetFloat("transY", transY);
    }
}
