namespace JobBoardPlatform.Presentation.Dtos
{
    public record AppStatusCommand
    {
        public string Status{ get; set; }

        public AppStatusCommand(string status)
        {
            Status = status;
        }
    }
}
