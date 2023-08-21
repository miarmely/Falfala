using Entities.ErrorModels;

namespace Entities.Exceptions
{
    public sealed class FormatErrorException : Exception
	{
		public FormatErrorException(string shortErrorCode
			, string longErrorCode
			, string message) 
			: base(new ErrorDetails()
			{
				StatusCode = 400,
				ErrorCode = shortErrorCode,
				ErrorDescription = longErrorCode,
				Message = message
			}
			.ToString())
		{ }
	}
}
