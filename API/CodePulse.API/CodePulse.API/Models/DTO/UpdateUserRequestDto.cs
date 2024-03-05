namespace CodePulse.API.Models.DTO {
    public class UpdateUserRequestDto {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
