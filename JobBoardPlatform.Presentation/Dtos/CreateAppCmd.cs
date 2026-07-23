namespace JobBoardPlatform.Presentation.Dtos
{
    public record CreateAppCmd
    {
        public CreateAppCmd(Guid userId, Guid jobAdID, string? note)
        {
            UserId = userId;
            this.jobAdID = jobAdID;
            Note = note;
        }

        public Guid UserId { get; set; }
        public Guid jobAdID { get; set; }
        public string? Note { get; set; }
    }
}
