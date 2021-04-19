using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Section2
{
	//luu du lieu question
	[Serializable]
	public class QuestionData
	{
		public string question;
		public string answerA;
		public string answerB;
		public string answerC;
		public string answerD;
		public string CorrectAnswer;
	}

	public enum GameState{
		Home,Gameplay,GameOver
	}
	public class GameManager : MonoBehaviour
	{
		[SerializeField]private TextMeshProUGUI m_txtQuestion;
		[SerializeField]private TextMeshProUGUI m_txtAnswerA;
		[SerializeField]private TextMeshProUGUI m_txtAnswerB;
		[SerializeField]private TextMeshProUGUI m_txtAnswerC;
		[SerializeField]private TextMeshProUGUI m_txtAnswerD;

		[SerializeField]private Image m_ImgAnswerA;
		[SerializeField]private Image m_ImgAnswerB;
		[SerializeField]private Image m_ImgAnswerC;
		[SerializeField]private Image m_ImgAnswerD;

		[SerializeField]private Sprite buttonGreen;
		[SerializeField]private Sprite buttonYellow;
		[SerializeField]private Sprite buttonBlack;

		//[SerializeField]private QuestionData[] m_QuestionData;//tao mang
		[SerializeField]private QuestionScriptableData[] m_QuestionData;

		[SerializeField]private GameObject m_Homepanel, m_GamePlayPanel, m_GameoverPanel;
		private GameState m_gamestate;//trang thai State Machine
		private int m_questionIndex;
		private int m_live = 3;

		[SerializeField]private AudioSource m_audioSource;
		[SerializeField]private AudioClip m_musicMain;
		[SerializeField]private AudioClip m_WrongAnswer;
		[SerializeField]private AudioClip m_CorrectAnswer;

		// Start is called before the first frame update
		void Start()
		{
			SetgameState(GameState.Home);
			m_questionIndex = 0;
			InitQuestion(0);
		}

		public void btnAnswer_Pressed(string pSelectedAnswer)
		{
			bool traloidung = false;
			if (m_QuestionData[m_questionIndex].CorrectAnswer == pSelectedAnswer)
			{
				traloidung = true;
				m_audioSource.PlayOneShot(m_CorrectAnswer);
				Debug.Log("cau tra dung");
			}
			else
			{
				m_live--;
				if (m_live == 0)
				{
					SetgameState(GameState.GameOver);
					m_audioSource.Stop();
				}
				traloidung = false;
				m_audioSource.PlayOneShot(m_WrongAnswer);
				Debug.Log("cau tra sai");
			}

			switch (pSelectedAnswer)
			{
				case "A":
					//đổi sprite button
					m_ImgAnswerA.sprite = traloidung ? buttonGreen : buttonYellow;

					//đổi màu
					//m_ImgAnswerA.color = traloidung ? Color.green : Color.red;
					break;
				case "B":
					//đổi sprite button
					m_ImgAnswerB.sprite = traloidung ? buttonGreen : buttonYellow;

					//đổi màu
					//m_ImgAnswerB.color = traloidung ? Color.green : Color.red;
					break;
				case "C":
					//đổi sprite button
					m_ImgAnswerC.sprite = traloidung ? buttonGreen : buttonYellow;

					//đổi màu
					//m_ImgAnswerC.color = traloidung ? Color.green : Color.red;
					break;
				case "D":
					//đổi sprite button
					m_ImgAnswerD.sprite = traloidung ? buttonGreen : buttonYellow;

					//đổi màu
					//m_ImgAnswerD.color = traloidung ? Color.green : Color.red;
					break;
			}

			if (traloidung)
			{
				if (m_questionIndex >= m_QuestionData.Length)
				{
					Debug.Log("Bạn đã chiến thắng");
					return;
				}
				Invoke("nextQuestion", 3f);
			}	
		}

		private void nextQuestion()
		{
			m_questionIndex++;
			InitQuestion(m_questionIndex);
		}
		private void InitQuestion(int pindex)
		{
			if (pindex < 0 || pindex >= m_QuestionData.Length)
				return;
			//m_ImgAnswerA.color = Color.white;
			//m_ImgAnswerB.color = Color.white;
			//m_ImgAnswerC.color = Color.white;
			//m_ImgAnswerD.color = Color.white;

			m_ImgAnswerA.sprite = buttonBlack;
			m_ImgAnswerB.sprite = buttonBlack;
			m_ImgAnswerC.sprite = buttonBlack;
			m_ImgAnswerD.sprite = buttonBlack;

			m_txtQuestion.text = m_QuestionData[pindex].question;
			m_txtAnswerA.text = "A. " + m_QuestionData[pindex].answerA;
			m_txtAnswerB.text = "B. " + m_QuestionData[pindex].answerB;
			m_txtAnswerC.text = "C. " + m_QuestionData[pindex].answerC;
			m_txtAnswerD.text = "D. " + m_QuestionData[pindex].answerD;

		}

		public void SetgameState(GameState state)
		{
			m_gamestate = state;
			m_live = 3;
			m_Homepanel.SetActive(m_gamestate == GameState.Home);
			m_GamePlayPanel.SetActive(m_gamestate == GameState.Gameplay);
			m_GameoverPanel.SetActive(m_gamestate == GameState.GameOver);
		}


		public void BtnPlay_Pressed()
		{
			m_live = 3;
			m_audioSource.clip = m_musicMain;
			m_audioSource.Play();
			SetgameState(GameState.Gameplay);
			InitQuestion(0);
		}
		public void BtnHome_Pressed()
		{
			SetgameState(GameState.Home);
		}
	}
}
