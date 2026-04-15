
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static int killCount { get; private set; }
    public static int points { get; private set; }
    public static GameManager Instance;
    private float waitForSceneChange = 0f;
    private float maxWaitingTime = 2f;

    private bool playerDead = false;
    public enum Scenes
    {
        GameScene,
        MainMenueScene,
        GameOverScene
    }

    private void Awake()
    {


        Instance = this;
        killCount = 0;
        points = 0;
        playerDead = false;
        waitForSceneChange = 0f;


    }
    private void Start()
    {
         
        EventBus.Suscribe(EventBus.events.OnEnemyDeath, GetKillCount);
        EventBus.Suscribe(EventBus.events.FriendlyFireKill, MorePoints);
        EventBus.Suscribe(EventBus.events.OnDeath, OnPlayer_Death);

    }

    private void Update()
    {
        if (playerDead)
        {

            waitForSceneChange += Time.deltaTime;
            if (waitForSceneChange >= maxWaitingTime)
            {

                SceneManager.LoadScene("GameOverScene");
            }

        }
    }

    private void OnPlayer_Death(GameObject playerObject)
    {
        if (playerObject == player.Instance.gameObject)
        {
            playerDead = true;
        }

    }

    private void MorePoints(object sender)
    {
        points += 50;
        Debug.Log("Stargeric Point");
        Debug.Log("Points:" + points);
    }

    private void GetKillCount(object sender)
    {
        killCount += 1;
        points += 10;

        Debug.Log("Points: " + points);
        Debug.Log("KillCount: " + killCount);
    }

    private void OnDisable()
{
    EventBus.UnSuscribe(EventBus.events.OnEnemyDeath, GetKillCount);
    EventBus.UnSuscribe(EventBus.events.FriendlyFireKill, MorePoints);
    EventBus.UnSuscribe(EventBus.events.OnDeath, OnPlayer_Death);
}


}
