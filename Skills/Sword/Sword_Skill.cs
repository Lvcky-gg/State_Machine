using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;
    private Vector2 finalDir;
    [Header("Aim Dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchDir.x, AimDirection().normalized.y * launchDir.y);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(spaceBetweenDots * i);

            }
        }
    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();
        newSwordScript.SetupSword(finalDir, swordGravity, player);
        player.AssignNewSword(newSword);
        DotsActive(false);
    }
    public Vector2 AimDirection() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }
    private Vector2 DotsPosition(float t) =>
    (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchDir.x,
            AimDirection().normalized.y * launchDir.y) * t + .2f * (Physics2D.gravity * swordGravity) * (t * t);

}
