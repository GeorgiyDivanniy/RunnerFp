using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Получить ссылку на компонент Animator
        animator = GetComponent<Animator>();

        // Проверить, была ли успешно найдена ссылка на компонент Animator
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));
    }
}
