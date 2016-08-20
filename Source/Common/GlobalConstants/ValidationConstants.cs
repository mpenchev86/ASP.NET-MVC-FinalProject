namespace MvcProject.Common.GlobalConstants
{
    public class ValidationConstants
    {
        // ApplicationUser entity
        public const string ErrorMessagePasswordConfirm = "The password and confirmation password do not match.";
        public const string ErrorMessagePasswordConfirmNew = "The new password and confirmation password do not match.";
        public const string ErrorMessagePasswordMinLength = "The {0} must be at least {2} characters long.";
        public const string ErrorMessageUsernameLength = "The {0} must be between {2} and {1} characters long.";
        public const int ApplicationUserNameMinLength = 5;
        public const int ApplicationUserNameMaxLength = 20;
        public const int ApplicationUserPasswordMinLength = 6;
        public const int ApplicationUserPasswordMaxLength = 100;

        // Category entity
        public const int CategoryNameMaxLenght = 50;

        // Comment entity
        public const int CommentContentMinLength = 20;
        public const int CommentContentMaxLength = 500;

        // Description entity
        public const int DescriptionContentMaxLength = 2000;

        // Image entity
        public const int ImageFullyQaulifiedFileNameMaxLength = 260;
        public const int ImageOriginalFileNameMaxLength = 255;
        public const int ImageFileExtensionMaxLength = 5;
        public const int ImageUrlPathMaxLength = 248;

        // Product entity
        public const int ProductTitleMaxLength = 150;
        public const int ProductShortDescriptionMaxLength = 300;
        public const int ProductAllTimeItemsSoldMin = 0;
        public const int ProductAllTimeItemsSoldMax = int.MaxValue;
        public const double ProductAllTimeAverageRatingMin = 0;
        public const double ProductAllTimeAverageRatingMax = 5;
        public const int ProductQuantityInStockMin = 0;
        public const int ProductQuantityInStockMax = int.MaxValue;
        public const decimal ProductUnitPriceMin = decimal.Zero;
        public const string ProductUnitPriceMinString = "0";
        public const decimal ProductUnitPriceMax = decimal.MaxValue;
        public const string ProductUnitPriceMaxString = "9999999";
        public const decimal ProductShippingPriceMin = decimal.Zero;
        public const string ProductShippingPriceMinString = "0";
        public const decimal ProductShippingPriceMax = decimal.MaxValue;
        public const string ProductShippingPriceMaxString = "9999999";

        // Property entity
        public const int PropertyNameMaxLength = 100;
        public const int PropertyValueMaxLength = 100;

        // Tag entity
        public const int TagNameMaxLength = 20;

        // Vote entity
        public const int VoteValueMin = 1;
        public const int VoteValueMax = 5;
    }
}
