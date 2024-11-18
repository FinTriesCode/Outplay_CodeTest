using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //init data
    private int _checkpointIndex = 0;
    
    [SerializeField]
    private float _moveSpeed; //move speed -> could be made const however I have opted to give the designer the ability to change speed.

    [SerializeField]
    Vector3[] _checkpoints; //list of vector 3's -> to store desired check points.

    //effects & manager
    [SerializeField]
    private GameObject _winEffect;
    [SerializeField]
    private GameObject _loseEffect;

    //audio clips
        //NOTE: Beware your ears, win clip is very loud and will not seem to scale with in-engine volume control.
    public AudioClip _audioWinClip;
    public AudioClip _audioLoseClip;

    private void Awake()
    {
        //pos and format player tag.
        transform.position = new Vector3(0f, 0f, 0f);
        this.tag = "Player";
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        //if player has successfully reached all checkpoints, win.
        if(_checkpointIndex == _checkpoints.Length)
            WinGame();
        else
        {
            //move player towards current checkpoint
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_checkpoints[_checkpointIndex].x, _checkpoints[_checkpointIndex].y, _checkpoints[_checkpointIndex].z), _moveSpeed * Time.deltaTime);

            //if checkpoint is reached, update to next checkpoint.
            if (transform.position == new Vector3(_checkpoints[_checkpointIndex].x, _checkpoints[_checkpointIndex].y, _checkpoints[_checkpointIndex].z))
            {
                Debug.Log("Checkpoint " + _checkpointIndex + " reached, now targeting " + (_checkpointIndex + 1));
                _checkpointIndex += 1;
            }
        }

    }

    private void WinGame()
    {
        Debug.Log("All checkpoints reached, you win!");
        //play audio
        AudioSource.PlayClipAtPoint(_audioWinClip, Camera.main.transform.position, 0.1f);
        //play VFX
        _winEffect = Instantiate(_winEffect, this.gameObject.transform.position, Quaternion.identity);
    }

    private void LoseGame()
    {
        Debug.Log("You Lose!");
        //play audio
        AudioSource.PlayClipAtPoint(_audioLoseClip, Camera.main.transform.position, 0.1f);
        //play vfx
        _winEffect = Instantiate(_loseEffect, this.gameObject.transform.position, Quaternion.identity);

        StartCoroutine(delay());
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //player -> obstacle collisions
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            LoseGame();
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
    }
}
