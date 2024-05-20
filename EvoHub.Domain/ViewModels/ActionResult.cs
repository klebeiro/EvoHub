namespace EvoHub.Domain
{
    public class ActionResult<TModel> where TModel: class, new()
    {
        public ActionResult() { }

        public bool IsValid { get; set; }
        
        public string Message { get; set; }
        
        public TModel Result { get; set; }
    }
}
