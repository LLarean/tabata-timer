using NUnit.Framework;

namespace Tests.EditMode
{
    public class ProgressBarModelTests
    {
        [TestCase(5)]
        public void Ctor_TransmittedMaximumDuration_MaximumDurationIsEqualTransmittedMaximumDuration(int maximumDuration)
        {
            ProgressBarModel progressBarModel = new ProgressBarModel(maximumDuration);
            
            Assert.IsTrue(progressBarModel.MaximumDuration == maximumDuration);
        }
        
        [TestCase(5)]
        public void Ctor_TransmittedMaximumDuration_CurrentDurationIsEqualTransmittedMaximumDuration(int maximumDuration)
        {
            ProgressBarModel progressBarModel = new ProgressBarModel(maximumDuration);
            
            Assert.IsTrue(progressBarModel.CurrentDuration == maximumDuration);
        }
    }
}
