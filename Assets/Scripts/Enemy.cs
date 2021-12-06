using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    private GameManager m_Game;
    public GameObject fireBall;
    private GameObject fireBallContainer;

    public int Life { get; private set; }

    public event EventHandler<DamageEvent> DamageToPlayer;
    public event EventHandler<DamageEvent> EnemyDie;

    private float rotationSpeed;
    private float delay;
    private float ballSpeed;

    public float Velocity { get; private set; } = 1;
    public int Iteration { get; private set; } = 1;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        fireBallContainer = m_Game.EnemyManager.fireBallsContainer;
        Life = m_Game.LevelsManager.levels[m_Game.LevelsManager.actualLevel].enemiesLife;
    }

    void Start()
    {
        AssignValue();
        StartCoroutine(SpawnFireball());
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed);
        AssignValue();
    }

    public void SetValues(float velocity, int iteration)
    {
        Velocity = velocity;
        Iteration = iteration;
    }

    private void AssignValue()
    {
        rotationSpeed = 1f * Velocity;
        delay = 0.20f / Velocity;
        ballSpeed = 35 * (Velocity / 3);
    }

    public void Damage(int damage)
    {
        m_Game.AudioManager.playShortSound("enemyDamage");

        Life = Life - damage;

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Color baseColor = sprite.color;

        sprite.color = new Color(1f, 0.3f, 0.3f);

        StartCoroutine(ResetColor(sprite, baseColor));

        if (Life <= 0)
        {
            Destroy(gameObject);
            EnemyDie?.Invoke(this, new DamageEvent(gameObject, 0));
        }
    }

    IEnumerator ResetColor(SpriteRenderer sprite, Color baseColor)
    {
        yield return new WaitForSeconds(0.2f);

        sprite.color = baseColor;
    }

    IEnumerator SpawnFireball()
    {
        newFireball();
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnFireball());
    }

    private void newFireball()
    {
        Vector2 euler2d = new Vector2(transform.up.x, transform.up.y);
        float angle = Vector2.SignedAngle(euler2d, Vector2.up);

        float offsetAngle = 1f / Iteration;

        for (int i = Iteration; i > 0; i--)
        {
            float angleFinal = offsetAngle * (float) i * 360f;

            Vector3 vcttg = new Vector3(transform.up.x, transform.up.y, transform.position.z);
            float angle2 = Mathf.Atan2(vcttg.y, vcttg.x) * Mathf.Rad2Deg;
            Quaternion quat = Quaternion.AngleAxis( angleFinal + angle2 -90, Vector3.forward);

            Tuple<Rigidbody2D, GameObject> tupleFireball = CreateFireball(quat);

            float forceX = ballSpeed * tupleFireball.Item2.transform.up.x;
            float forceY = ballSpeed * tupleFireball.Item2.transform.up.y;

            tupleFireball.Item1.AddForce(new Vector2( forceX, forceY));
        }
    }

    private Tuple<Rigidbody2D, GameObject> CreateFireball(Quaternion quat)
    {
        GameObject fireballObject = Instantiate(fireBall, transform.position, new Quaternion(0, 0, quat.z, quat.w), fireBallContainer.transform);

        Fireball newFireBall = fireballObject.GetComponent<Fireball>();
        newFireBall.DamageToPlayer += OnDamageToPlayer;

        Rigidbody2D fbBody = fireballObject.GetComponent<Rigidbody2D>();

        return Tuple.Create(fbBody, fireballObject);

    }

    private void OnDamageToPlayer(object sender, DamageEvent e)
    {
        DamageToPlayer?.Invoke(this, e);
    }
}