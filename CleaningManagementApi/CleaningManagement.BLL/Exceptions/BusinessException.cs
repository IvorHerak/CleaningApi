using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace CleaningManagement.BLL.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(ValidationResult validationResult)
        {
            Errors = new Dictionary<string, string>();
            foreach (var error in validationResult.Errors)
            {
                if (Errors.ContainsKey(error.PropertyName))
                {
                    Errors[error.PropertyName] += " " + error.ErrorMessage;
                }
                else
                {
                    Errors.Add(error.PropertyName, error.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Dictionary which stores the list of errors for a property path
        /// </summary>
        public Dictionary<string, string> Errors { get; set; }
    }
}
