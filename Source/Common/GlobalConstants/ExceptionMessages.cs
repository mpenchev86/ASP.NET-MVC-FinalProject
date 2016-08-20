namespace MvcProject.Common.GlobalConstants
{
    public class ExceptionMessages
    {
        // Templates
        public const string CustomExceptionMessage = "Custom Error: {0}";

        // Data access exceptions
        public const string DbContextArgumentException = "An instance of DbContext is required to use this repository.";

        // File System access exceptions
        public const string FileNameHasInvalidCharacters = "File name contains invalid characters.";
        public const string FileNameNullOrEmpty = "File name cannot be null or empty.";
        public const string FileNameTooLong = "File name is too long.";
        public const string FilePathHasInvalidCharacters = "File path contains invalid characters.";
        public const string FilePathNullOrEmpty = "File path cannot be empty or contain invalid characters.";
        public const string FilePathTooLong = "File path cannot be longer than.";
    }
}
