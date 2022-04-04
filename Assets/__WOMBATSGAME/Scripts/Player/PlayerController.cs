using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [Header("Class References")] [SerializeField]
    public GameControls gameControlsClass;
    
    [Header("Player Car Stuff")]
    public PathFollower playerPF;
    public float xOffSet;
    public GameObject PlayercarVisual;

    
    public float Acc, Dec, targetSpeed;

    

    [Header("Car Movement Variables")] 
    [SerializeField]
    public bool isAnimPlayed;
    [SerializeField] 
    private Transform leftCarTransform,rightCarTransform,centreCarTransform;
    private Vector3 Velocity = Vector3.zero;
    [SerializeField]
    private float smoothTime,smoothTimeCamera;
    public int currentPosition;
    public float cameraOffsetxOffset;
    public float movementDuration,rotationDuration;
    
    
    private void Start()
    {
        targetSpeed = LevelManager.Instance.normalSpeed;
        PlayercarVisual = LevelManager.Instance.currentPlayerCarModel;
        StartCoroutine("IniCarPush");
        
        
    }

    IEnumerator IniCarPush()
    {
        PlayercarVisual.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(false); 
        playerPF.speed = 0.5f;
        PlayercarVisual.transform.localPosition = new Vector3(PlayercarVisual.transform.localPosition.x,0.01f,0f);
        yield return new WaitForSeconds(0.5f);
        PlayercarVisual.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true); 
        playerPF.speed = 0;
        
    }

    private void Update()
    {
        
        if (LevelManager.Instance.isGameStarted)
        {
            if (GameManager.Instance.canControlCar)
            {
                if (gameControlsClass.gestureState == GameControls.GestureState.Break)                                // Apply Breaks
                {
                    LevelManager.Instance.slowWind.SetActive(true);
                    LevelManager.Instance.FastWind.SetActive(false);
                    
                    //Decelerate
                    float target = 0;                                                                                
                    float delta = playerPF.speed - target;    
                    delta *= Time.deltaTime * Dec;
                    
                    if (playerPF.speed <= 1)
                    {
                        playerPF.speed = 0;
                    }
                    else
                    {
                        playerPF.speed -= delta;
                    }
                    
                    //Other Effects
                    PlayercarVisual.transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).gameObject.SetActive(true);            //CAR LIGHTS
                }

                if (gameControlsClass.gestureState == GameControls.GestureState.Release)                            //Release Breaks
                {
                    LevelManager.Instance.slowWind.SetActive(false);
                    LevelManager.Instance.FastWind.SetActive(true);
                    
                    //Accelerate
                    float target = targetSpeed;
                    float delta = target - playerPF.speed;
                    delta *= Time.deltaTime * Acc;
                    playerPF.speed += delta;
                    
                    //Other Effects
                    PlayercarVisual.transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).gameObject.SetActive(false);            //CAR LIGHTS

                }
                
            }

        }
        
        else if(!LevelManager.Instance.isGameStarted)
        {
            playerPF.speed = 0;
        }

    }

    
    #region PlayerGestureMovements

    

    public void MoveLeft()
    {
        switch (currentPosition)
        {
            case 0:
                PlayercarVisual.transform.DOLocalMove(leftCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                
                DOTween.To(() => LevelManager.Instance.cmCameraOffset.m_Offset.x, 
                        x => LevelManager.Instance.cmCameraOffset.m_Offset.x = x, -cameraOffsetxOffset, 0.5f)
                    .OnUpdate(() => {
                        
                    });
                
                
                currentPosition = -1;
                break;
            
            case 1:
                PlayercarVisual.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => LevelManager.Instance.cmCameraOffset.m_Offset.x, 
                        x => LevelManager.Instance.cmCameraOffset.m_Offset.x = x, 0, 0.5f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 0;
                break;
        }
        
    }
    
    public void MoveRight()
    {
        switch (currentPosition)
        {
            case 0:
                PlayercarVisual.transform.DOLocalMove(rightCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => LevelManager.Instance.cmCameraOffset.m_Offset.x, 
                        x => LevelManager.Instance.cmCameraOffset.m_Offset.x = x, cameraOffsetxOffset, 0.5f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 1;
                break;
            case -1:
                PlayercarVisual.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => LevelManager.Instance.cmCameraOffset.m_Offset.x, 
                        x => LevelManager.Instance.cmCameraOffset.m_Offset.x = x, 0, 0.5f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 0;
                break;
        }
        
    }
    
    public void RightSwipe()
    {
        if (currentPosition == 0)
        {
            //currentPosition = 1;
            
            PlayercarVisual.GetComponent<Animator>().SetBool("isRight",true);
            Invoke("AnimPLayedTrue",0.1f);
           
        }
        
        if (currentPosition == -1)
        {
            //currentPosition = 0;
            
            PlayercarVisual.GetComponent<Animator>().SetBool("isRight",true);
            Invoke("AnimPLayedTrue",0.1f);
           
        }
    }

    

    public void LeftSwipe()
    {
        if (currentPosition == 0)
        {
            //currentPosition = -1;
            
            PlayercarVisual.GetComponent<Animator>().SetBool("isLeft",true);
            Invoke("AnimPLayedTrue",0.1f);
        }
        
        if (currentPosition == 1)
        {
           // currentPosition = 0;
            
            PlayercarVisual.GetComponent<Animator>().SetBool("isLeft",true);
            Invoke("AnimPLayedTrue",0.1f);
        }
    }

    void AnimPLayedFalse()
    {
        if (isAnimPlayed)
        {
            isAnimPlayed = false;
            PlayercarVisual.GetComponent<Animator>().SetBool("isLeft",false);
            PlayercarVisual.GetComponent<Animator>().SetBool("isRight",false);
        }
       
    }
   
    void AnimPLayedTrue()
    {
        if (!isAnimPlayed)
        {
            isAnimPlayed = true;
           
        }
      
    }

    #endregion
    
}
