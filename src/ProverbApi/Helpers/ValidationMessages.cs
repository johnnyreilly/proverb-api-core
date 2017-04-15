using System.Collections.Generic;
using System.Linq;

namespace Proverb.Api.Core.Helpers
{
    public class ValidationMessage 
    {
       public string Name { get; } 
       public IEnumerable<string> Messages { get; }
       public ValidationMessage(string name, IEnumerable<string> messages)
       {
           Name = name;
           Messages = messages;
       }

       public ValidationMessage(string name, string message)
       {
           Name = name;
           Messages = new [] { message };
       } 
    }

    public class ValidationMessages
    {
        private readonly Dictionary<string, IEnumerable<string>> _errors = new Dictionary<string, IEnumerable<string>>();

        public ValidationMessages() 
        {
        }

        public ValidationMessages(Dictionary<string, IEnumerable<string>> errors)
        {
            _errors = errors;
        }

        public ValidationMessages(params ValidationMessage[] errors)
        {
            _errors = errors.ToDictionary(e => e.Name, value => value.Messages);
        }

        public void AddError(string field, string error)
        {
            Errors.Add(field, new[] { error });
        }

        public Dictionary<string, IEnumerable<string>> Errors 
        { 
            get 
            { 
                return _errors; 
            } 
        }

        public string ErrorsAsString() 
        { 
            return string.Join(", ", Errors.Values); 
        } 

        public bool HasErrors() 
        { 
            return Errors.Keys.Any(); 
        }
    }
}
