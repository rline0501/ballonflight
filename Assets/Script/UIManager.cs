using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    //txtScore�Q�[���I�u�W�F�N�g�̎���Text�R���|�[�l���g���C���X�y�N�^�[����A�T�C������
    private Text txtScore;

    [SerializeField]
    private Text txtInfo;

    [SerializeField]
    private CanvasGroup canvasGroupInfo;

    [SerializeField]
    private ResultPopUp resultPopUpPrefab;

    [SerializeField]
    private Transform canvasTran;

   ///<summary>
   ///�X�R�A�\�����X�V
   /// </summary>
   ///<param name="score"></param>param>
   public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }

    ///<summary>
    ///�Q�[���I�[�o�[�\��
    /// </summary>
    public void DisplayGameOverInfo()
    {
        //InfoBackGround�Q�[���I�u�W�F�N�g�̎���anvasGroup�R���|�[�l���g��Alpha�̒l��
        //�P�b�����ĂP�ɕύX���āA�w�i�ƕ�������ʂɌ�����悤�ɂ���B
        canvasGroupInfo.DOFade(1.0f, 1.0f);

        //��������A�j���[�V�����ɂ����ĕ\��
        txtInfo.DOText("Game Over�c", 1.0f);

    }


    public void GenerateResultPopUp(int score)
    {
        //ResultPopUp�𐶐�
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

        //ResultPopUp�̐ݒ���s��
        resultPopUp.SetUpResultPopUp(score);
    }

}
