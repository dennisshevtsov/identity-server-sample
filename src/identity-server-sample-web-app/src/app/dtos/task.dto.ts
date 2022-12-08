import { UserDto } from './user.dto';

export class TaskDto {
  public constructor(
    public taskId: number,
    public title: string,
    public assigned: UserDto,
    public deadline: string,
    public priority: number,
    public status: number,
  ) {}
}
