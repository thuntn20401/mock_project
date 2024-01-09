using System;
using System.Collections.Generic;

namespace InsternShip.Data.Models;

public class QuestionModel
{
    public Guid QuestionId { get; set; }

    public string QuestionString { get; set; } = null!;

    public Guid CategoryQuestionId { get; set; }
}
