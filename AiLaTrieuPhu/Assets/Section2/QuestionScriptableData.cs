﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Section2
{
	[CreateAssetMenu(fileName ="QuestionData")]
	public class QuestionScriptableData : ScriptableObject
	{
		public string question;
		public string answerA;
		public string answerB;
		public string answerC;
		public string answerD;
		public string CorrectAnswer;
	}
}

