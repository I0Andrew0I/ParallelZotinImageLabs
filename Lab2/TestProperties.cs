namespace Lab2
{
    public class TestProperties
    {
        public TestProperties()
        {
        }

        public TestProperties(int testCount, int threadCount)
        {
            TestCount = testCount;
            ThreadCount = threadCount;
        }

        public int TestCount { get; set; }
        public int ThreadCount { get; set; }

        public void Deconstruct(out int testCount, out int threadCount)
        {
            testCount = TestCount;
            threadCount = ThreadCount;
        }
    }
}