namespace ToDoApp.Helpers.Validations.Rules
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value.ToString();

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
