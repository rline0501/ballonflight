using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class IntroCameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 pos;

    [SceneName]
    public string nextLevel;

    IEnumerator Start()
    {
        pos = transform.position;

        //yield return new WaitForSeconds(audio.clip.length + 1);

        SceneManager.LoadScene(nextLevel);

        yield return null;
    }

    void Update()
    {
        //float newPosition = Mathf.SmoothStep(pos.x, target.position.x, Time.timeSinceLevelLoad / audio.clip.length);

        //transform.position = new Vector3(newPosition, pos.y, pos.z);
    }
}
