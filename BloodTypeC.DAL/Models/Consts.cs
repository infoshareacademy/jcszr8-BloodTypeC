namespace BloodTypeC.DAL.Models
{
    public static class Consts
    {
        public const string mailSenderFrom = "beeropedia@int.pl";

        public const string mailTemplate =
            "<h1 style=\"font-family:'Comic Sans MS'; font-size:large; font-variant:small-caps; line-height:20px; text-align:center; color:#ffffff; background-color:#FDC500; padding:20px;\">{MAILHEADLINE}</h1><p style=\"background-color: #27292A\">{MAILBODY}</p><p style =\"font-family:Helvetica, sans-serif; font-size:x-small; line-height:20px; text-align:center; color:#ffffff; background-color:#4d4d4d; padding:20px;\">{MAILFOOTER}</p>";

        public const int nameMinLength = 2;
        public const string nameMinLengthString = "2";
        public const int nameMaxLength = 40;
        public const string nameMaxLengthString = "40";
        public const double maxScore = 10;
        public const string maxScoreString = "10";
        public const double maxAbv = 95;
        public const string maxAbvString = "95";
        public const int breweryMaxLength = 40;
        public const string breweryMaxLengthString = "40";
    }
}
