// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Dtos
{
  public enum Priority : byte
  {
    Low = 0, Medium = 1, High = 2,
  }

  public enum Status : byte
  {
    ToDo = 0, InProgress = 1, Done = 2, Close = 3,
  }

  public sealed class GetTasksResponseDto
  {
    public TaskDto[]? Tasks { get; set; }

    public int Total { get; set; }

    public int PageNo { get; set; }

    public int PageCount { get; set; }

    public sealed class TaskDto
    {
      public int TaskId { get; set; }

      public string? Title { get; set; }

      public UserDto? Assigned { get; set; }

      public DateTime Deadline { get; set; }

      public Priority Priority { get; set; }

      public Status Status { get; set; }
    }

    public sealed class UserDto
    {
      public int UserId { get; set; }

      public string? Name { get; set; }
    }
  }
}
