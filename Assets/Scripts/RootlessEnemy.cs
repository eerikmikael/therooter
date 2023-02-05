using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RootlessEnemy : MonoBehaviour
{
    [SerializeField]
    private float rootPoints = 2;

    private List<Texture> skins;
    
    private Rigidbody myBody;

    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    public void Hit(float damage)
    {
        rootPoints -= damage;

        if(rootPoints <= 0)
        {
            GotRooted();
        }
    }

    private void GotRooted()
    {
        StopMovementAndCreateCrater();
        LevelManager.Instance.AddLevelProgress(1);
    }

    private void StopMovementAndCreateCrater()
    {
        Vector3 position = transform.position;
        myBody.isKinematic = true;
        position = new Vector3(position.x, 0, position.z);
        transform.DOMove(position, 0.05f);

        GameObject crater = Instantiate(GameManager.Instance.GetCrater(), GameManager.Instance.transform, true);
        crater.transform.Rotate(0,0, Random.Range(0, 180f));
        crater.transform.position = new Vector3(position.x, 0, position.z);

        GameObject audioSourceObject = AudioManager.Instance.GetNextAudioSourceGameObject();
        audioSourceObject.transform.parent = transform;
        audioSourceObject.transform.position = position;

        AudioManager.Instance.SetAudioMixerChannel();
    }
}
