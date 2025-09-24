using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player LocalPlayer { get; private set; }
    public static Action<Player> OnPlayerSpawned;
    public Rigidbody Rigidbody;
    public Rigidbody FootBall;
    public PlayerStats Stats;

    public bool Live = true;

    private void Awake()
    {
        FootBall.isKinematic = true;
        LocalPlayer = this;
        OnPlayerSpawned?.Invoke(this);
    }

    public static void StartLocalPlayer()
    {
        LocalPlayer.StartPlayer();
    }

    protected void StartPlayer()
    {
        FootBall.isKinematic = false;
        FootBall.AddForce(Vector3.forward * 300);
    }

    private void FixedUpdate()
    {
        FootBall.AddTorque(Stats.ConstantForce);
    }
    public void Joystick(float horizontal)
    {
        horizontal = Mathf.Clamp(horizontal, -1, 1);
        horizontal *= 20;
        Stats.ConstantForce.z = horizontal;
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
        FootBall.AddExplosionForce(6, transform.position, 0.5f);
        Time.timeScale = 1f;
        GameManager.Instance.Invoke(nameof(GameManager.Instance.RestartScene), 3);
        Debug.Log("Dead");
    }
}

[System.Serializable]
public struct PlayerStats {
    public Vector3 ConstantForce;
    public Vector3 JumpForce;
}