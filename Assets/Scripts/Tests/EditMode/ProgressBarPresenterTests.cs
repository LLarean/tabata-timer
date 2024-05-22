using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class ProgressBarPresenterTests
    {
        [Test]
        public void Ctor_TransmittedParametersToConstructor_ThereAreNoErrors()
        {
            ProgressBarModel progressBarModel = new ProgressBarModel();
            IProgressBarView progressBarView = Substitute.For<IProgressBarView>();

            Assert.That(() => new ProgressBarPresenter(progressBarModel, progressBarView), Throws.Nothing);
        }
        
        [TestCase(-1)]
        public void StartAnimation_TransmittedDurationIsNegative_CurrentDurationIsZero(int duration)
        {
            ProgressBarModel progressBarModel = new ProgressBarModel();
            IProgressBarView progressBarView = Substitute.For<IProgressBarView>();
            ProgressBarPresenter progressBarPresenter = new ProgressBarPresenter(progressBarModel, progressBarView);

            progressBarPresenter.StartAnimation(duration);
            Assert.IsTrue(progressBarModel.CurrentDuration == 0);
        }

        [TestCase(1)]
        public void StartAnimation_TransmittedDuration_CurrentDurationIsEqualTransmittedDuration(int duration)
        {
            ProgressBarModel progressBarModel = new ProgressBarModel();
            IProgressBarView progressBarView = Substitute.For<IProgressBarView>();
            ProgressBarPresenter progressBarPresenter = new ProgressBarPresenter(progressBarModel, progressBarView);

            progressBarPresenter.StartAnimation(duration);
            Assert.IsTrue(progressBarModel.CurrentDuration == duration);
        }
        
        [TestCase(-1)]
        public void ChangeMaximumDuration_TransmittedNegativeDuration_MaximumDurationIsZero(int duration)
        {
            ProgressBarModel progressBarModel = new ProgressBarModel();
            IProgressBarView progressBarView = Substitute.For<IProgressBarView>();
            ProgressBarPresenter progressBarPresenter = new ProgressBarPresenter(progressBarModel, progressBarView);

            progressBarPresenter.ChangeMaximumDuration(duration);
            Assert.IsTrue(progressBarModel.MaximumDuration == 0);
        }

        [TestCase(5)]
        public void ChangeMaximumDuration_TransmittedDuration_MaximumDurationIsEqualTransmittedDuration(int duration)
        {
            ProgressBarModel progressBarModel = new ProgressBarModel();
            IProgressBarView progressBarView = Substitute.For<IProgressBarView>();
            ProgressBarPresenter progressBarPresenter = new ProgressBarPresenter(progressBarModel, progressBarView);

            progressBarPresenter.ChangeMaximumDuration(duration);
            Assert.IsTrue(progressBarModel.MaximumDuration == duration);
        }
    }
}