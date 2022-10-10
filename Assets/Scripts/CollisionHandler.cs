using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] public float levelLoadDelay = 2f;
    [SerializeField] public AudioClip crashSound;
    [SerializeField] public AudioClip finishSound;

    [SerializeField] public ParticleSystem crashParticles;
    [SerializeField] public ParticleSystem finishParticles;

    KeyCode loadNextLevelKey = KeyCode.L;
    KeyCode toggleCollisionsKey = KeyCode.C;

    private AudioSource audioSource;
    private bool isTransitioning = false;
    private bool collisionsDisabled = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(loadNextLevelKey))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(toggleCollisionsKey))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionTag = collision.gameObject.tag;
        if (isTransitioning || collisionsDisabled) {  return; }

        switch (collisionTag)
        {
            case "Friendly":
                CollisionFriendly();
                break;
            case "Finish":
                isTransitioning = true;
                StartSuccessSequence();
                break;
            default:
                isTransitioning = true;
                StartCrashSequence();              
                break;
        }
    }




    void StartCrashSequence()
    {
        // TODO: Add particle effect for crash
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);        
    }
    private void CollisionFriendly()
    {
        Debug.Log("This thing is Friendly");
    }

    private void StartSuccessSequence()
    {
        finishParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
       
    }

    public void LoadNextLevel()
    {
        var numScenes = SceneManager.sceneCountInBuildSettings;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < numScenes)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        isTransitioning = false;
    }
    private void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    
    }
}
