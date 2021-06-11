using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalChecker : MonoBehaviour
{
    //�ړ����x
    public float moveSpeed = 0.01f;

    //��~�n�_�B��ʂ̉E�[�ŃX�g�b�v������B
    private float stopPos = 6.5f;

    //�S�[���̏d������h�~�p�B��x�S�[�����肵����true�ɂ��āA�S�[������͂P�񂾂������s��Ȃ��悤�ɂ���B
    private bool isGoal;

    void Update()
    {
        //��~�n�_�ɓ�������܂ňړ�����
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" && isGoal == false)
        {
            //�Q��ڈȍ~�̓S�[��������s��Ȃ��悤�ɂ��邽�߂ɁAtrue�ɕύX����
            isGoal = true;

            Debug.Log("�Q�[���N���A");

            //PlayerController�̏����擾
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();

            //PlayerController�̎���UIManager�̕ϐ��𗘗p���āAGenerateResultPopUp���\�b�h���Ăяo��
            playerController.uiManager.GenerateResultPopUp(playerController.coinPoint);
        }


    }
}
