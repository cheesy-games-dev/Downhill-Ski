using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player LocalPlayer;
    public Animator animator;
    public Camera myCamera;
    public TMP_Text scoreText;
    public Transform cameraPlace;
    public float lerpTime;
    public Rigidbody rb;
    public float playerSpeed;
    public float turnSpeed;
    public LayerMask groundLayerMask;
    public float score = 0;
    public Camera fpsCamera;
    public Transform headPos;

    public Rigidbody[] rigidbodiesEnableOnDeath;

    private string tempBool = "null";
    [NonSerialized] public float tempPlayerSpeed;
    [NonSerialized] public float maxVel = 50f;

    public bool alive = true;

    private void Awake() {
        LocalPlayer = this;
        Time.timeScale = 1f;
        score = 0;
        tempPlayerSpeed = playerSpeed;
        myCamera = GetComponentInChildren<Camera>();
        rb = GetComponentInChildren<Rigidbody>();
        alive = true;
        rigidbodiesEnableOnDeath = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodiesEnableOnDeath) {
            if (!rigidbody.Equals(rb)) {
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                rigidbody.GetComponent<Collider>().enabled = false;
            }       
        }      
        if (tempBool == "bean") {
            myCamera.enabled = false;
            return;
        }
        myCamera.enabled = true;
        //myCamera.transform.parent = null;
        rb.transform.parent = null;
        fpsCamera.transform.position = headPos.position;
        fpsCamera.transform.rotation = headPos.rotation;
    }

    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext callbackContext) {
        moveInput = callbackContext.ReadValue<Vector2>();
    }
    private bool fpsPlayerMode = false;
    private void Update()
    {   
        if (tempBool == "bean") {
            myCamera.enabled = false;
            return;
        }
        if (Keyboard.current.fKey.wasPressedThisFrame) {
            fpsPlayerMode = !fpsPlayerMode;
        }
        fpsCamera.enabled = fpsPlayerMode;
        myCamera.enabled = !fpsPlayerMode;
        if (!alive)
            return;  
        fpsCamera.transform.parent = headPos;
        myCamera.transform.position = Vector3.Slerp(myCamera.transform.position, cameraPlace.position, lerpTime * Time.deltaTime);
        myCamera.transform.eulerAngles = Vector3.Slerp(myCamera.transform.eulerAngles, cameraPlace.eulerAngles, lerpTime * Time.deltaTime);
        transform.position = rb.transform.position;
        rb.AddForce(transform.forward * playerSpeed);
        rb.AddForce(Vector3.right * moveInput.x * turnSpeed * 10f);
        animator.SetFloat("xPos", Mathf.Lerp(animator.GetFloat("xPos"), moveInput.x, 5 * Time.deltaTime));
        rb.freezeRotation = true;
        rb.maxLinearVelocity = maxVel;
        RaycastHit hit;
        if (Physics.Raycast(rb.position, -rb.transform.up, out hit, 0.5f, groundLayerMask)) {
            cameraPlace.localPosition = new Vector3(0, 3, -4f);
            cameraPlace.localEulerAngles = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.FromToRotation(rb.transform.up, hit.normal); // * rb.rotation;
            playerSpeed = 1f;
            score += Time.deltaTime;
        }
        else {
            cameraPlace.localPosition = new Vector3(0, 2.5f, -2.78f);
            cameraPlace.localEulerAngles = new Vector3(15f, 0, 0);
            playerSpeed = tempPlayerSpeed;
        }
        scoreText.text = "Score\n" + Mathf.RoundToInt(score).ToString();
    }

    public void ChangeCamera() {
        fpsPlayerMode = !fpsPlayerMode;
    }

    public void Die() {
        alive = false;
        animator.enabled = false;
        foreach (Rigidbody rigidbody in rigidbodiesEnableOnDeath) {
            if (!rigidbody.Equals(rb)) {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                rigidbody.GetComponent<Collider>().material = new PhysicsMaterial("tempOrSomething");
                rigidbody.GetComponent<Collider>().material.frictionCombine = PhysicsMaterialCombine.Minimum;
                rigidbody.GetComponent<Collider>().material.staticFriction = 0.1f;
                rigidbody.GetComponent<Collider>().material.dynamicFriction = 0.1f;
                rigidbody.GetComponent<Collider>().enabled = true;
                rigidbody.AddForce(Vector3.up + Vector3.forward * 5);
            }
        }
        Invoke(nameof(RestartScene), 3f);
        Time.timeScale = 1f;
        Debug.Log("Dead");
    }

    private void RestartScene() {
        if (score >= PlayerPrefs.GetInt("HighScore") || !PlayerPrefs.HasKey("HighScore")) {
            PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(score));
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame() {
        this.rb.isKinematic = false;
        rb.AddForce(Vector3.forward * 100);
    }
}
