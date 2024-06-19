namespace starting_project.Models
{
    public abstract class QuestionDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string QuestionText { get; set; }

        protected QuestionDto() { }

        protected QuestionDto(string id, string type, string questionText)
        {
            Id = id;
            Type = type;
            QuestionText = questionText;
        }
    }

    public class ParagraphQuestionDto : QuestionDto
    {
        public ParagraphQuestionDto() : base() { }

        public ParagraphQuestionDto(string id, string questionText)
            : base(id, "Paragraph", questionText) { }
    }

    public class YesNoQuestionDto : QuestionDto
    {
        public YesNoQuestionDto() : base() { }

        public YesNoQuestionDto(string id, string questionText)
            : base(id, "YesNo", questionText) { }
    }

    public class DropdownQuestionDto : QuestionDto
    {
        public List<string> Options { get; set; }

        public DropdownQuestionDto() : base() { }

        public DropdownQuestionDto(string id, string questionText, List<string> options)
            : base(id, "Dropdown", questionText)
        {
            Options = options;
        }
    }

    public class MultipleChoiceQuestionDto : QuestionDto
    {
        public List<string> Options { get; set; }

        public MultipleChoiceQuestionDto() : base() { }

        public MultipleChoiceQuestionDto(string id, string questionText, List<string> options)
            : base(id, "MultipleChoice", questionText)
        {
            Options = options;
        }
    }

    public class DateQuestionDto : QuestionDto
    {
        public DateQuestionDto() : base() { }

        public DateQuestionDto(string id, string questionText)
            : base(id, "Date", questionText) { }
    }

    public class NumberQuestionDto : QuestionDto
    {
        public NumberQuestionDto() : base() { }

        public NumberQuestionDto(string id, string questionText)
            : base(id, "Number", questionText) { }
    }
}
