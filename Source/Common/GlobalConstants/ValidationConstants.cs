namespace MvcProject.GlobalConstants
{
    public class ValidationConstants
    {
        // ApplicationUser entity
        public const string ErrorMessagePasswordConfirm = "The password and confirmation password do not match.";
        public const string ErrorMessagePasswordConfirmNew = "The new password and confirmation password do not match.";
        public const string ErrorMessagePasswordMinLength = "The {0} must be at least {2} characters long.";
        public const string ErrorMessageUsernameLength = "The {0} must be between {2} and {1} characters long.";

        // Category entity
        public const int CategoryNameMaxLenght = 50;

        // Comment entity
        public const int CommentContentMinLength = 20;
        public const int CommentContentMaxLength = 500;

        // Description entity
        public const int DescriptionContentMaxLength = 2000;

        // Image entity
        public const int ImageOriginalFileNameMaxLength = 255;
        public const int ImageFileExtensionMaxLength = 4;
        public const int ImageUrlPathMaxLength = 255;

        // Product entity
        public const int ProductTitleMaxLength = 150;
        public const int ShortDescriptionMaxLength = 300;

        // Property entity

        // Tag entity
        public const int TagNameMaxLength = 20;

        // Vote entity
        public const int VoteValueMin = 1;
        public const int VoteValueMax = 10;
    }
}
