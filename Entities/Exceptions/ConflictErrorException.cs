using Entities.ErrorModels;

namespace Entities.Exceptions
{
    public sealed class ConflictErrorException : Exception
	{
		public ConflictErrorException(string shortErrorCode
			, string longErrorCode
			, string message)
			: base(new ErrorDetails()
			{
				StatusCode = 409,
				ErrorCode = shortErrorCode,
				ErrorDescription = longErrorCode,
				Message = message
			}
			.ToString())
		{ }
	}
}
