namespace MVCommand.Views
{
    public interface IViewDataRetriever<ModelType>
    {
        ModelType GetModelFromViewData();
        ModelType GetNullableModelFromViewData();
    }
}