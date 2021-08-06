namespace WeAreFootball.Common
{
    public static class ModelValidation
    {
        public static class Comment
        {
            public const int ContentMaxValue = 255;
            public const int ContentMinValue = 3;
        }

        public static class ContactForm
        {
            public const int FullNameMaxLeght = 35;
            public const int FullNameMinLeght = 3;
            public const int ContentMaxLeght = 255;
            public const int ContentMinLeght = 5;
        }

        public static class League
        {
            public const int NameMaxLenght = 20;
            public const int NameMinLenght = 3;
            public const int CountryNameLenght = 15;
            public const int CountryMinNameLenght = 3;
        }

        public static class News
        {
            public const int TitleMaxLenght = 40;
            public const int TitleMinLenght = 5;
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
            public const int StadiumName = 30;
            public const int StadiumMinName = 3;
        }

        public static class Vote
        {
            public const int VoteMinValue = 1;
            public const int VoteMaxValue = 5;
        }
    }
}
