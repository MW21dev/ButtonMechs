
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int playerMaxHp;
    public int playerCurrentHp;
    public int playerMaxActions;
    public int playerCurrentActions;
    public int playerDamage;
    public int playerCurrentMoney;
    public int drawCount = 1;
    public bool isDead;

    public static PlayerStats Instance;

    public GameObject explosion;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke("SetHealth", 0.2f);

       
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.F9))
        {
            GetHit(1);
        }
    }



    public void SetHealth()
    {
        playerCurrentHp = playerMaxHp;
        UIScript.Instance.ChangeHp(playerCurrentHp);

    }

    public void GetHit(int recievedDmg)
    {
        playerCurrentHp -= recievedDmg;
        var explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionPrefab, 0.2f);
        UIScript.Instance.ChangeHp(playerCurrentHp);

        SoundManager.Instance.PlayUISound(4);


        if (playerCurrentHp == 0)
        {
            Invoke("Dead", 0.3f);
        }

        

    }

    public void UseAction(int actionCost)
    {
        playerCurrentActions -= actionCost;
    }

    public void SetMaxActions(int maxActions)
    {
        playerCurrentActions = maxActions;
    }
    public void Dead()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayUISound(7);
        var a = GameObject.FindAnyObjectByType<DeadAnimation>();
        a.LaunchTurnOn();
    }
}
