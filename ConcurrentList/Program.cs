using MainProgram;

var testCase = TestCase.List_Unsafe;

switch(testCase)
{
    case TestCase.List_Unsafe:
        RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerUnsafe_ListUnsafe, Helpers.Producers(1), Helpers.Consumers(1), Constants.ItemsToProduce);
        break;

    case TestCase.ListWithLock_UnSafe:
        RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerUnsafe_ListLock, Helpers.Producers(1), Helpers.Consumers(1), Constants.ItemsToProduce);
        //RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerUnsafe_ListLock, Helpers.Producers(2), Helpers.Consumers(1), Constants.ItemsToProduce);
        //RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerUnsafe_ListLock, Helpers.Producers(1), Helpers.Consumers(2), Constants.ItemsToProduce);
        break;

    case TestCase.ListWithLock_Safe:
        RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerSafe_ListLock, Helpers.Producers(2), Helpers.Consumers(1), Constants.ItemsToProduce);
        //RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerSafe_ListLock, Helpers.Producers(1), Helpers.Consumers(2), Constants.ItemsToProduce);
        //RunnerGeneric.Start(RunnerGeneric.RunnerType.RunnerSafe_ListLock, Helpers.Producers(8), Helpers.Consumers(15), Constants.ItemsToProduce);        
        break;

    default:
        throw new ArgumentOutOfRangeException($"run: {nameof(testCase)} UNSUPPORTED");
}
