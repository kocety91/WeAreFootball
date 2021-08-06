namespace WeAreFootball.Common
{
    public static class ModelValidation
    {
        public const string NameLengthError = "Name must be between {2} and {1} symbols";
        public const string EmptyFieldLengthError = "Please enter the field.";

        public static class Comment
        {
            public const int ContentMaxValue = 255;
            public const int ContentMinValue = 3;

            public const string ContentDisplay = "Content";
        }

        public static class ContactForm
        {
            public const int FullNameMaxLeght = 35;
            public const int FullNameMinLeght = 3;
            public const int ContentMaxLeght = 255;
            public const int ContentMinLeght = 5;

            public const string FullNameDisplay = "Full Name";
            public const string EmailDisplay = "Email";
            public const string ContentDisplay = "Content";

        }

        public static class League
        {
            public const int NameMaxLenght = 20;
            public const int NameMinLenght = 3;
            public const int CountryNameLenght = 15;
            public const int CountryMinNameLenght = 3;

            public const string NameDisplay = "Name";
            public const string CountryDisplay = "Country";
        }

        public static class News
        {
            public const int TitleMaxLenght = 40;
            public const int TitleMinLenght = 5;

            public const string TitleDisplay = "Title";
            public const string ContentDisplay = "Content";
            public const string LeaguesDisplay = "Leagues";
            public const string TagDisplay = "Tags";
        }

        public static class Tag
        {
            public const int NameMaxLenght = 20;
            public const int NameMinLenght = 3;
        }

        public static class Team
        {
            public const int NameMaxLenght = 25;
            public const int NameMinLenght = 3;
            public const int CityNameLenght = 15;
            public const int CityMinNameLenght = 3;
            public const int StadiumMaxName = 30;
            public const int StadiumMinName = 3;

            public const string NameDisplay = "Name";
            public const string CityDisplay = "City";
            public const string StadiumNameDisplay = "StadiumName";
            public const string LeaguesDisplay = "Leagues";

        }

        public static class Vote
        {
            public const int VoteMinValue = 1;
            public const int VoteMaxValue = 5;
        }
    }
}
