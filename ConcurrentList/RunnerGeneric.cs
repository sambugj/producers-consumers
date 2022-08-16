namespace MainProgram
{
    internal class RunnerGeneric
    {
        public enum RunnerType
        {
            RunnerUnsafe_ListUnsafe,
            RunnerUnsafe_ListLock,
            RunnerSafe_ListLock,
        }

        public static void Start(RunnerType runnerType, int producers, int consumers, int itemsToProduce)
        {
            switch (runnerType)
            {
                case RunnerType.RunnerUnsafe_ListUnsafe:
                    RunnerUnsafe_ListUnsafe.Start(producers, consumers, itemsToProduce);
                    break;
                case RunnerType.RunnerUnsafe_ListLock:
                    RunnerUnsafe_ListWithLock.Start(producers, consumers, itemsToProduce);
                    break;
                case RunnerType.RunnerSafe_ListLock:
                    RunnerSafe_ListWithLock.Start(producers, consumers, itemsToProduce);
                    break;
            }
        }
    }
}
