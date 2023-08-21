using Entities.ErrorModels;


namespace Entities.Exceptions
{
	public sealed class VerificationErrorException : Exception
	{
		public VerificationErrorException(string shortErrorCode
			, string longErrorCode
			, string message)
			: base(new ErrorDetails()
			{
				StatusCode = 404,
				ErrorCode = shortErrorCode,
				ErrorDescription = longErrorCode,
				Message = message
			}
			.ToString())
		{ }
	}
}