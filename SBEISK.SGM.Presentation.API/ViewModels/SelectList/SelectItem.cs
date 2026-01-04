namespace SBEISK.SGM.Presentation.API.ViewModels.SelectList
{
    public class SelectItem<TId, TText>
    {
        public SelectItem(TId id, TText text)
        {
            Id = id;
            Text = text;
        }

        public TId Id { get; set; }
        public TText Text { get; set; }
    }
}