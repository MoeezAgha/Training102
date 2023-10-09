using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training102.DAL
{
    public class User : IdentityUser<int>
    {
        // Navigation property for TrainingAssignToUser
        public List<TrainingAssignToUser> AssignedTrainings { get; set; }

        // Navigation property for QuizCompletion
        public List<QuizCompletion> QuizCompletions { get; set; }
    }

    // Training entity represents a training course.
    public class Training
    {
        [Key] // Define TrainingId as the primary key
        public int TrainingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Quiz> Quizzes { get; set; } // Collection of quizzes in the training

        public int CreatedByUserId { get; set; } // User who created the training

        [ForeignKey("CreatedByUserId")] // Define CreatedByUserId as a foreign key referencing the User entity
        public User CreatedByUser { get; set; } // Navigation property to the creator
                                                // Other training-related properties
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }

    // Quiz entity represents a quiz that belongs to a training.
    public class Quiz
    {
        [Key] // Define QuizId as the primary key
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; } // Collection of questions in the quiz

        public int CreatedByUserId { get; set; } // User who created the quiz

        [ForeignKey("CreatedByUserId")] // Define CreatedByUserId as a foreign key referencing the User entity
        public User CreatedByUser { get; set; } // Navigation property to the creator
                                                // Other quiz-related properties
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    // Question entity represents a question in a quiz.
    public class Question
    {
        [Key] // Define QuestionId as the primary key
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<Answer> Choices { get; set; } // Collection of answer choices for the question

        [ForeignKey("CorrectAnswer")]
        public int? CorrectAnswerId { get; set; } // Foreign key to the correct answer

        public Answer CorrectAnswer { get; set; } // The correct answer for the question
        public bool IsActive { get; set; }         // Other question-related properties
        public bool IsDeleted { get; set; }
    }

    // Answer entity represents an answer option for a question.
    public class Answer
    {
        [Key] // Define AnswerId as the primary key
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }

    // TrainingAssignToUser entity represents the assignment of a training to a user.
    public class TrainingAssignToUser
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Training")]
        public int TrainingId { get; set; }

        public Training Training { get; set; }

        public DateTime AssignmentDate { get; set; }
        // Other assignment-related properties
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }

    // QuizCompletion entity represents the completion of a quiz by a user.
    public class QuizCompletion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public double PercentageCorrect { get; set; } // Percentage of correct answers
        public bool IsPass { get; set; } // Pass or Fail status
        public DateTime CompletionDate { get; set; } // Date of quiz completion
                                                     // Other audit-related properties
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
    //// TrainingAssignToUser entity represents the assignment of a training to a user.
    //public class TrainingAssignToUser
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [ForeignKey("User")] // Define UserId as a foreign key referencing the User entity
    //    public int UserId { get; set; }

    //    public User User { get; set; }

    //    [ForeignKey("Training")] // Define TrainingId as a foreign key referencing the Training entity
    //    public int TrainingId { get; set; }

    //    public Training Training { get; set; }
    //    public DateTime AssignmentDate { get; set; }
    //    // Other assignment-related properties
    //}

    //// QuizCompletion entity represents the completion of a quiz by a user.
    //public class QuizCompletion
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [ForeignKey("User")] // Define UserId as a foreign key referencing the User entity
    //    public int UserId { get; set; }

    //    public User User { get; set; }

    //    [ForeignKey("Quiz")] // Define QuizId as a foreign key referencing the Quiz entity
    //    public int QuizId { get; set; }

    //    public Quiz Quiz { get; set; }
    //    public double PercentageCorrect { get; set; } // Percentage of correct answers
    //    public bool IsPass { get; set; } // Pass or Fail status
    //    public DateTime CompletionDate { get; set; } // Date of quiz completion
    //    // Other audit-related properties
    //}
}
