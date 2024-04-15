using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public float bossTimer = 300;
    public GameObject bossPrefab;
    public Transform spawnPoint;
    static public float timer;
    static public int minutes;
    static public int seconds;
    public static bool bossSpawned;


    public AudioSource bossSound;
    public AudioSource basicSound;

    private bool isFadingOut = false;
    private float fadeOutDuration = 1f;

    void Start()
    {
        timer = bossTimer;
        basicSound.Play();
    }

    void Update()
    {

        if (UiManager.isGamePaused || UiManager.isGameEnded)
        {
            bossSound.Pause();
            basicSound.Pause();
        }
        else
        {
            bossSound.UnPause();
            basicSound.UnPause();
        }
        timer -= Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60);
        seconds = Mathf.FloorToInt(timer % 60);
        if ((timer <= 0 && !bossSpawned) || (UiManager.soulsNumber >= UiManager.soulsNeeded && !bossSpawned))
        {
            StartCoroutine(FadeOutBasicSound());
            SpawnBoss();
            bossSpawned = true;
          
        }
    }

    IEnumerator FadeOutBasicSound()
    {
        isFadingOut = true;
        float startVolume = basicSound.volume;
        float timer = 0;

        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            basicSound.volume = Mathf.Lerp(startVolume, 0, timer / fadeOutDuration);
            yield return null;
        }

        basicSound.Stop();
        isFadingOut = false;
    }

    void SpawnBoss()
    {
        bossSound.Play();
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
    }
}
