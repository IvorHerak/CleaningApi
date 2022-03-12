using CleaningManagement.BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CleaningManagement.Api.Infrastructure
{
    public class ErrorDetails
    {
        public ErrorDetails(BusinessException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Errors = new Dictionary<string, string>();
            foreach (var kvp in exception.Errors)
            {
                var newKey = char.ToLowerInvariant(kvp.Key[0]) + kvp.Key.Substring(1);
                Errors.Add(newKey, kvp.Value);
            }
            Message = "One or more errors occured.";
        }

        public ErrorDetails()
        {
            Message = "One or more errors occured.";
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, string> Errors { get; set; }
    }
}
