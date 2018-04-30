namespace HaroldAdviser.BL
{
    public class Result
    {
        public readonly bool Success;

        public readonly string Error;

        private Result()
        {
            Success = true;
        }

        public Result(string err)
        {
            Success = false;
            Error = err;
        }

        public static Result Ok = new Result();
    }
}