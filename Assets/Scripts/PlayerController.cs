using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform Muzzle;
    public GameManager1 Manager;

    [SerializeField]
    private float _Speed;

    /// <summary>
    /// Maps this controller to a specific set of controls
    /// </summary>
    [Range(1, 2)]
    [SerializeField]
    private int _PlayerNum = 1;

    private string _InputMoveAxis, _InputShootAxis;

    private Transform _PointerTransform;
    private BallScript _CapturedBall;

    [SerializeField]
    private float _CaptureDuration;
    private float _CaptureTimer;

    [SerializeField]
    private float _CaptureCooldown;
    private float _CooldownTimer;

    [SerializeField]
    private float _MaxTravel = 4f;

    private float _PrevShootAxis;

    private GameObject _CannonObject;

    private BarrelAnimator _Animator;

    // Use this for initialization
    void Start() {
        _InputMoveAxis = "Player" + _PlayerNum.ToString();
        _InputShootAxis = "PlayerShoot" + _PlayerNum.ToString();

        _CannonObject = GetComponentInChildren<CannonCollisionInvoker>().gameObject;
        _PointerTransform = GetComponentInChildren<PointerMotor>().transform;

        _Animator = GetComponent<BarrelAnimator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis(_InputMoveAxis) != 0)
        {
            transform.position += transform.up * _Speed * Time.deltaTime * Input.GetAxis(_InputMoveAxis);
            float clampedY = Mathf.Clamp(transform.position.y, -_MaxTravel, _MaxTravel);
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        }
        if (Input.GetAxis(_InputShootAxis) != 0 && _PrevShootAxis == 0)
        {
            Shoot();
        }

        if (_CaptureTimer > 0)
        {
            _CaptureTimer -= Time.deltaTime;

            if (_CaptureTimer <= 0)
            {
                _CooldownTimer = _CaptureCooldown;
                _Animator.CloseBarrel();
            }
        }

        if (_CooldownTimer > 0)
        {
            _CooldownTimer -= Time.deltaTime;
        }

        _PrevShootAxis = Input.GetAxis(_InputShootAxis);
    }

    //This is called by the child object on collision
    public void OnCaptureCollision(GameObject other)
    {
        //if the object is a ball
        if (other.GetComponent<BallScript>() != null)
        {
            if (_CaptureTimer > 0)
            {
                CaptureBall(other);
            }
            else
            {
                Manager.PlayerDies(_PlayerNum);
            }
        }
    }

    public void CaptureBall(GameObject other)
    {
        Debug.Log("Capture");
        this._CapturedBall = other.GetComponent<global::BallScript>();
        _CapturedBall.SetVelocity(Vector2.zero);
        _CapturedBall.transform.position = _CannonObject.transform.position;
        _CapturedBall.GetComponent<Collider>().enabled = false;
        _CapturedBall.transform.parent = transform;
    }

    private void Shoot()
    {
        if (_CapturedBall != null)
        {
            _CapturedBall.velocityScale *= 2;
            _CapturedBall.GetComponent<BallScript>().SetVelocity(Muzzle.transform.right * _CapturedBall.velocityScale);
            _CapturedBall.transform.position += Muzzle.transform.right;
            _CapturedBall.GetComponent<Collider>().enabled = true;
            _CapturedBall.transform.parent = null;
            _CapturedBall = null;
        }
        else
        {
            //If the player hasn't captured
            if (_CaptureTimer <= 0 && _CooldownTimer <= 0)
            {
                _CaptureTimer = _CaptureDuration;
                _Animator.OpenBarrel();
            }
        }
    }
}
