namespace OnionArchitecture.Bl.Dto
{
    public class EmployeeDto : BaseDto
    {
        public int BossId { get; set; }

        public BossDto Boss { get; set; }
    }
}
