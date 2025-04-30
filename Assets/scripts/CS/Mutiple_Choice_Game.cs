using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuizGame : MonoBehaviour
{
    public struct Question
    {
        public string text;
        public string correctAnswer;
        public List<string> wrongAnswers;
    }

    [Header("UI References")]
    public TextMeshProUGUI questionText;           // -> Question
    public TextMeshProUGUI  feedbackText;           // -> OutPut
    public TextMeshProUGUI  scoreText;              // -> Score

    public Button buttonA;              // -> Button_A
    public Button buttonB;              // -> Button_B
    public Button buttonC;              // -> Button_C
    public Button buttonD;              // -> Button_D

    [Header("Questions")]
    public List<Question> questions;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private List<string> currentShuffledAnswers;

    void Start()
    {
        if (questions == null || questions.Count == 0)
            LoadDummyQuestions();
        ShowQuestion();
    }

    void LoadDummyQuestions()
{
    questions = new List<Question>
    {
        new Question
        {
            text = "What is the capital of France?",
            correctAnswer = "Paris",
            wrongAnswers = new List<string> { "Berlin", "London", "Rome" }
        },
        new Question
        {
            text = "What is 2 + 2?",
            correctAnswer = "4",
            wrongAnswers = new List<string> { "3", "5", "22" }
        },
        new Question
        {
            text = "Which color is made by mixing red and blue?",
            correctAnswer = "Purple",
            wrongAnswers = new List<string> { "Green", "Orange", "Pink" }
        }
    };
}


    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            questionText.text = "Quiz Complete!";
            feedbackText.text = $"Final Score: {score}/{questions.Count}";
            scoreText.text = "";
            DisableAllButtons();
            return;
        }

        Question q = questions[currentQuestionIndex];
        questionText.text = q.text;

        // Prepare answers
        currentShuffledAnswers = new List<string>(q.wrongAnswers);
        currentShuffledAnswers.Add(q.correctAnswer);
        Shuffle(currentShuffledAnswers);

        // Assign to buttons
        List<Button> buttons = new List<Button> { buttonA, buttonB, buttonC, buttonD };

        for (int i = 0; i < buttons.Count; i++)
        {
            var btn = buttons[i];
            var answer = currentShuffledAnswers[i];
            
            
        
            btn.GetComponentInChildren<TextMeshProUGUI>().text = answer;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnAnswerSelected(answer));
            btn.gameObject.SetActive(true);
        }

        feedbackText.text = "";
        scoreText.text = $"Score: {score}";
    }

    void OnAnswerSelected(string selectedAnswer)
    {
        Question q = questions[currentQuestionIndex];

        if (selectedAnswer == q.correctAnswer)
        {
            feedbackText.text = "Correct!";
            score++;
        }
        else
        {
            feedbackText.text = $"Wrong! Correct answer: {q.correctAnswer}";
        }

        currentQuestionIndex++;
        Invoke("ShowQuestion", 1.5f);
    }

    void DisableAllButtons()
    {
        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);
        buttonC.gameObject.SetActive(false);
        buttonD.gameObject.SetActive(false);
    }

    void Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}

