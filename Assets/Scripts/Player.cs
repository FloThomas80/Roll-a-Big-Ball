using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    //private Joystick joystick;
    private int ScoreValue = 0;

    private float movX;
    private float movY;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScenarioData _scenario;
    [SerializeField] private GameObject _wallPrefab;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : "+ScoreValue;
        //joystick = FindObjectOfType<Joystick>();
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movX = movementVector.x;
        movY = movementVector.y;
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movX *-1,0f, movY * -1);

        if (movX != 0f || movY != 0f)
        {
            _rigidbody.AddForce(movement * _speed); //* Time.deltaTime);
        } 


        //if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        //{
        //    _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical"));
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Destroy(other.gameObject);
            UpdateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        ScoreValue++;
        //PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        //_scoreText.text = PlayerPrefs.GetString("Score"); // increment et affichage du score

        Instantiate(_wallPrefab, _scenario.FirstWalls[ScoreValue].position, Quaternion.Euler(_scenario.FirstWalls[ScoreValue].orientation)); // on pop un mur suivant l'ordre donné dans le scriptable object

        if (PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        }
        _scoreText.text = PlayerPrefs.GetString("Score"); // increment et affichage du score


        if (ScoreValue >= 8 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerPrefs.SetInt("ScoreValue", ScoreValue);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
