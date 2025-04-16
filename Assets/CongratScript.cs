using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CongratScript : MonoBehaviour
{
    public TextMesh text;
    public ParticleSystem[] sparksParticles = new ParticleSystem[8];
    private List<string> textDisplay = new List<string>() { "Congratulation!", "All Errors Fixed!" };
    private float rotatingSpeed;
    private float round = 360.0f;
    private float showText0Sec = 6.0f;
    private float showText1Sec = 4.0f;
    private float timeToRotation = 0.0f;
    private float particleInterval = 0.5f;
    private bool particleForward = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchText()); //Changes text every specified seconds. ex)showText0Sec, showText1Sec
        StartCoroutine(PlayParticles()); //Repeat the process of turning on the particles
    }
    // Update is called once per frame
    void Update()
    {
        RotationY(); //Rotates text at a specified speed
    }
    void RotationY() {
        float rotationDegree = (timeToRotation += Time.deltaTime) * rotatingSpeed;
        transform.rotation = Quaternion.Euler(0, -rotationDegree, 0);
        if (rotationDegree >= round) timeToRotation = 0;
    }
    IEnumerator SwitchText() {
        while (true)
        {
            //rotatingSpeed is set to change after the text has rotated once
            rotatingSpeed = round / showText0Sec;
            text.text = textDisplay[0];
            yield return new WaitForSeconds(showText0Sec);
            rotatingSpeed = round / showText1Sec;
            text.text = textDisplay[1];
            yield return new WaitForSeconds(showText1Sec);
        }
    }
    IEnumerator PlayParticles()
    {
        while (true)
        {
            // If 'particleForward' is true, the particle turns on as it moves forward, if false, it turns on as it moves back
            if (particleForward)
            {
                for (global::System.Int32 i = 0; i < sparksParticles.Length; i++)
                {
                    sparksParticles[i].Play();
                    yield return new WaitForSeconds(particleInterval);
                    sparksParticles[i].Stop();
                }
                particleForward = !particleForward;
            }
            else
            {
                for (global::System.Int32 i = sparksParticles.Length-1; i >= 0; i--)
                {
                    sparksParticles[i].Play();
                    yield return new WaitForSeconds(particleInterval);
                    sparksParticles[i].Stop();
                }
                particleForward = !particleForward;
            }
        }
    }
}