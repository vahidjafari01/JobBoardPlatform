namespace JobBoardPlatform.Presentation.Dtos
{
    public record UpdateCompanyDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Location { get; set; }
        public Guid CityId { get; set; }
    }
}
