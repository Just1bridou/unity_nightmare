using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class IntroductionManager : MonoBehaviour
{
    private GameManager m_Game;

    public LevelsData introductionLevel;
    public GameObject character;

    private GameObject ActualLevel;
    private GameObject? ActualCharacter = null;

    private List<Vector3> positions = new List<Vector3>();

    private int pos = 0;
    private bool StartAnimationBool = false;

    private void Awake()
    {
        m_Game = GameManager.Instance;

        positions.Add(new Vector3(0.47f, 0.28f, 0));
        positions.Add(new Vector3(-0.41f, 0.28f, 0));
        positions.Add(new Vector3(-0.41f, 0.1f, 0));
        positions.Add(new Vector3(-0.83f, 0.1f, 0));
    }

    public void startIntroduction()
    {
        m_Game.ClearContainer(m_Game.LevelsManager.levelContainer);

        ActualLevel = Instantiate(introductionLevel.gridMap.gameObject, m_Game.LevelsManager.levelContainer.transform);

        m_Game.AudioManager.playSound("musicHouse");

        InstantiatePlayer();
        StartCoroutine(StartAnimation());
    }

    public void Update()
    {
        if(ActualCharacter != null && StartAnimationBool)
        {
            Vector3 res = ActualCharacter.transform.position = Vector3.MoveTowards(ActualCharacter.transform.position, positions[pos], 0.5f * Time.deltaTime);

            if(res == positions[pos] && pos < positions.Count - 1)
            {
                pos++;
            }

            if(pos == positions.Count - 1 && res == positions[pos])
            {
                StartAnimationBool = false;
                StartCoroutine(TurnOffLight());
            }
        }
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(2);
        StartAnimationBool = true;
    }

    IEnumerator TurnOffLight()
    {
        yield return new WaitForSeconds(2);
        m_Game.AudioManager.playShortSound("switchEffect");
        Light roomLight = GetLight("Light").GetComponent<Light>();
        roomLight.enabled = false;
        m_Game.TransitionManager.transitionIn((res) => {
            m_Game.StartGame();
        });
    }

    private bool move(Vector2 from, Vector2 to)
    {
        return true;
    }

    private void InstantiatePlayer()
    {
        ActualCharacter = Instantiate(character.gameObject, m_Game.LevelsManager.levelContainer.transform);
        ActualCharacter.transform.position = new Vector3(0.47f, 0.39f, 0);
    }

    private GameObject GetLight(string tag)
    {
        Transform t = ActualLevel.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.gameObject;
            }
        }

        return null;
    }
}
