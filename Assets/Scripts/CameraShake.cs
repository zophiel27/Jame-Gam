using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera mCam;
    private float mShakeTime = 0.2f;
    private float mShakeIntensity = 1f;
    private float timer;
    private CinemachineBasicMultiChannelPerlin mBasicMultiChannelPerlin;

    private void Awake()
    {
        mCam = GetComponent<CinemachineVirtualCamera>();
        mBasicMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StopShake();
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0)
                StopShake();
        }
    }
    public void ShakeCamera() {
        mBasicMultiChannelPerlin.m_AmplitudeGain = mShakeIntensity;
        timer = mShakeTime;
    }
    public void StopShake() {
        mBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        timer = 0;
    } 
}
