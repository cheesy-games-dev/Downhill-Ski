using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player LocalPlayer { get; private set; }
    public static Action<Player> OnPlayerSpawned;
    public Rigidbody Rigidbody;
    public Rigidbody FootBall;
    public PlayerStats Stats;
    public PlayerController Controller;
    public bool Live = true;

    private void Awake()
    {
        FootBall.isKinematic = true;
        LocalPlayer = this;
        OnPlayerSpawned?.Invoke(this);
    }
    public void SetController(PlayerController controller)
    {
        Controller = controller;
    }

    public static void StartLocalPlayer()
    {
        LocalPlayer?.OnStartPlayer();
    }

    public void OnStartPlayer()
    {
        FootBall.isKinematic = false;
        FootBall?.AddForce(Vector3.forward * 300);
        if (!TryGetComponent(out Controller)) SetController(gameObject.AddComponent<PlayerControllerInput>());
    }

    private void FixedUpdate()
    {
        Controller?.InputUpdate();
        FootBall?.AddForce(Stats.ConstantForce);
    }
    public void Joystick(float horizontal)
    {
        horizontal = Mathf.Clamp(horizontal, -1, 1);
        float speed = horizontal * Stats.SpeedMultiplier;
        Stats.ConstantForce.x = speed;
        //FootBall.AddForce(Vector3.right*speed);
    }
    public void Jump()
    {
        if(Physics.Raycast(FootBall.position, Vector3.down, 1)) FootBall.AddForce(Stats.JumpForce);
    }

    public void ChangeCamera()
    {
    }

    public void Die() {
        Live = false;
        FootBall?.AddExplosionForce(6, transform.position, 0.5f);
        Time.timeScale = 1f;
        Rigidbody.freezeRotation = false;
        Rigidbody?.AddTorque(Vector3.one * 6);
        Destroy(FootBall?.GetComponent<Joint>());
        GameManager.Instance?.Invoke(nameof(GameManager.Instance.EndGame), 3);
        Debug.Log("Dead");
    }
}

[Serializable]
public struct PlayerStats
{
    public Vector3 ConstantForce;
    public Vector3 JumpForce;
    public float SpeedMultiplier;
}