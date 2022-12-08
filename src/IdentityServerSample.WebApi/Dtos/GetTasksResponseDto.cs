

namespace IdentityServerSample.WebApi.Dtos
{
  public sealed class GetTasksResponseDto
  {
    public int TaskId { get; set; }

    public string Title { get; set; }

    public UserDto MyProperty { get; set; }
  }
}
