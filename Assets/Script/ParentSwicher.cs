using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwicher : MonoBehaviour
{
    //Tag�ɐݒ肵�Ă��镶�������
    private string player = "Player";

    //���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̃R���C�_�[��
    //���̃Q�[���I�u�W�F�N�g�̃R���C�_�[���ڐG���Ă���Ԃ����ƐڐG������s�����\�b�h
    private void OnCollisionStay2D(Collision2D col)
    {
        //�ڐG���肪���������col�ϐ��ɃR���C�_�[�̏�񂪑�������B
        //���̃R���C�_�[�����Q�[���I�u�W�F�N�g��Tag��player�ϐ��̒l("Player")�Ɠ���������Ȃ�
        if(col.gameObject.tag == player)
        {
            //�ڐG���Ă���Q�[���I�u�W�F�N�g�i�L�����j���A
            //���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�i���j�̎q�I�u�W�F�N�g�ɂ���
            col.transform.SetParent(transform);
        }
    }

    //���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̃R���C�_�[�ƁA
    //���̃Q�[���I�u�W�F�N�g�̃R���C�_�[�Ƃ����ꂽ�ۂɔ�����s�����\�b�h
    private void OnCollisionExit2D(Collision2D col)
    {
        //�R���C�_�[�����ꂽ���肪���������col�ϐ��ɃR���C�_�[�̏�񂪑�������B
        //���̃R���C�_�[�����Q�[���I�u�W�F�N�g��Tag��player�ϐ��̒l("Player")�Ɠ���������Ȃ�
        if(col.gameObject.tag == player)
        {
            //�ڐG��Ԃł͂Ȃ��Ȃ����i���ꂽ�j�Q�[���I�u�W�F�N�g�i�L�����j�ƁA���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�i���j�̐e�q�֌W����������
            col.transform.SetParent(null);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
