using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    //MoveObject�h���������ՂƂ��擾�����ۂɑ�����邽�߂̏���
    private MoveObject moveObject;

    void Start()
    {
        //���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̎��AMoveObject�X�N���v�g��T���Ď擾���AmoveObject�ϐ��ɑ��
        moveObject = GetComponent<MoveObject>();
    }

    ///<summary>
    ///�󒆏��Ɉړ����x��^����
    ///</summary>

    public void SetInitialSpeed()
    {
        ///�A�T�C�����Ă���Q�[���I�u�W�F�N�g�̎���MoveObject�X�N���v�g��moveSpeed�ϐ��ɃA�N�Z�X���āA�E�ӂ̒l��������
        moveObject.moveSpeed = 0.005f;

    }
}